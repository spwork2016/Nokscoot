using DevEnvAzure.DataContracts;
using DevEnvAzure.Interfaces;
using Newtonsoft.Json;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentLibrary : ContentPage
    {
        const string SPDocumentLibraryURL = "https://sptechnophiles.sharepoint.com/_api/web/GetFolderByServerRelativeUrl('{0}')/{1}";
        const string SPDocumentFileURL = "https://sptechnophiles.sharepoint.com/_api/web/GetFolderByServerRelativeUrl('{0}')/Files('{1}')";
        const string SPDocumentAnonymousLink = "https://sptechnophiles.sharepoint.com/_api/SP.Web.CreateAnonymousLink";

        public string FolderPath { get; set; }
        public string PrevPath { get; set; }
        public bool IsBackButtonVisible { get; set; }

        public DocumentLibrary()
        {
            FolderPath = "/SampleDocuments";
            BindingContext = this;
            InitializeComponent();
        }

        public DocumentLibrary(string folderPath = "/SampleDocuments")
        {
            FolderPath = folderPath;
            BindingContext = this;
            InitializeComponent();
        }

        private async void BtnNavBack_Clicked(object sender, EventArgs e)
        {
            if (PrevPath != "")
                await DataBind(PrevPath);
        }

        private async Task<bool> DataBind(string fPath)
        {
            ActivityIndicator spinner = this.FindByName<ActivityIndicator>("activityIndicator");

            Device.BeginInvokeOnMainThread(() =>
            {
                IsBusy = true;
            });

            List<DataContracts.Result> dataSource = new List<Result>();
            var folders = await GetDocuments(fPath, false);
            if (folders != null)
            {
                foreach (var item in folders)
                {
                    item.Icon = "folder.png";
                }
                dataSource.AddRange(folders);
            }

            //Retrive files
            var files = await GetDocuments(fPath, true);
            if (files != null)
            {
                foreach (var item in files)
                {
                    item.Icon = "file.png";
                }
                dataSource.AddRange(files);
            }

            documentList.ItemsSource = dataSource;
            FolderPath = fPath;
            btnNavBack.IsVisible = fPath != "/SampleDocuments";

            Device.BeginInvokeOnMainThread(() =>
            {
                IsBusy = false;
            });
            return true;
        }

        protected override async void OnAppearing()
        {
            await DataBind(FolderPath);
            documentList.ItemSelected += DocumentList_ItemSelected;
        }

        private async void DocumentList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (DataContracts.Result)e.SelectedItem;
            if (item.Icon == "file.png")
            {
                await GetFile(item);
            }
            else
            {
                PrevPath = FolderPath;
                await DataBind(item.ServerRelativeUrl);
            }

        }

        public async Task<List<DataContracts.Result>> GetDocuments(string folderPath, bool isFiles)
        {
            var client = OAuthHelper.GetHTTPClient();
            var response = await client.GetStringAsync(string.Format(SPDocumentLibraryURL, folderPath, isFiles ? "/Files" : "/Folders"));
            if (response != null)
            {
                var folders = JsonConvert.DeserializeObject<SPFolder>(response, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
                if (folders != null)
                {
                    return folders.d.results.ToList();
                }
            }

            return null;
        }

        public async Task GetFile(DataContracts.Result fileObj)
        {
            try
            {
                var client = OAuthHelper.GetHTTPClient();

                string root = ClientConfiguration.Default.ActiveDirectoryResource;
                string urlString = Uri.EscapeUriString(root.Remove(root.Length - 1, 1) + fileObj.ServerRelativeUrl);
                var myContent = "{'url':'" + urlString + "','isEditLink':false}";
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = client.PostAsync(SPDocumentAnonymousLink, byteContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    string str = await response.Content.ReadAsStringAsync();
                    DocumentAnonymousReponse res = JsonConvert.DeserializeObject<DocumentAnonymousReponse>(str);
                    Device.OpenUri(new Uri(res.d.CreateAnonymousLink));
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert("Unable to retrieve file Information");
            }
        }

        public class D
        {
            public string CreateAnonymousLink { get; set; }
        }

        public class DocumentAnonymousReponse
        {
            public D d { get; set; }
        }

    }
}