using DevEnvAzure.DataContracts;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentLibrary : ContentPage
    {
        string SPDocumentLibraryURL = ClientConfiguration.Default.ActiveDirectoryResource + "SSQServices/DMS/_api/web/GetFolderByServerRelativeUrl('{0}')/{1}";
        string SPDocumentFileURL = ClientConfiguration.Default.ActiveDirectoryResource + "SSQServices/DMS/_api/web/GetFolderByServerRelativeUrl('{0}')/Files('{1}')";
        string SPDocumentAnonymousLink = ClientConfiguration.Default.ActiveDirectoryResource + "SSQServices/DMS/_api/SP.Web.CreateAnonymousLink";

        public string FolderPath { get; set; }
        public List<string> PrevPaths { get; set; }
        public bool IsBackButtonVisible { get; set; }
        const string RootFolder = "";

        public DocumentLibrary()
        {
            FolderPath = RootFolder;
            BindingContext = this;
            PrevPaths = new List<string>();
            InitializeComponent();
        }

        private async void BtnNavBack_Clicked(object sender, EventArgs e)
        {
            if (PrevPaths.Count > 0)
            {
                string prevPath = PrevPaths.Last();
                PrevPaths.RemoveAt(PrevPaths.Count - 1);
                await DataBind(prevPath);
            }
        }

        private async Task<bool> DataBind(string fPath)
        {
            if (!CrossConnectivity.Current.IsConnected) return false;

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
                    if (!string.IsNullOrEmpty(fPath) || (item.Name == "Safety Security Critical Information" || item.Name == "NCT Controlled Manuals Library"))
                    {
                        dataSource.Add(item);
                    }
                }
            }

            if (!string.IsNullOrEmpty(fPath))
            {
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
            }

            documentList.ItemsSource = dataSource;
            tBarItemBackBtn.Text = fPath != RootFolder ? "Back" : "";
            Device.BeginInvokeOnMainThread(() =>
            {
                FolderPath = fPath;

                string[] ps = fPath.Split('/');
                Title = ps[ps.Length - 1];

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
            PrevPaths.Add(FolderPath);
            var item = (DataContracts.Result)e.SelectedItem;
            if (item.Icon == "file.png")
            {
                IsBusy = true;
                await GetFile(item);
            }
            else
            {
                await DataBind(item.ServerRelativeUrl);
            }

        }

        public async Task<List<DataContracts.Result>> GetDocuments(string folderPath, bool isFiles)
        {
            try
            {
                var client = await OAuthHelper.GetHTTPClient();
                string url = string.Format(SPDocumentLibraryURL, folderPath, isFiles ? "Files" : "Folders");
                var response = await client.GetStringAsync(url);
                if (response != null)
                {
                    var folders = JsonConvert.DeserializeObject<SPFolder>(response, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
                    if (folders != null)
                    {
                        return folders.d.results.ToList();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public async Task GetFile(DataContracts.Result fileObj)
        {
            try
            {
                var client = await OAuthHelper.GetHTTPClient();

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
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        IsBusy = false;
                    });
                }
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsBusy = false;
                });

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