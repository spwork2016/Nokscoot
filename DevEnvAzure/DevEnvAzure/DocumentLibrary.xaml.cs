using DevEnvAzure.DataContracts;
using DevEnvAzure.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentLibrary : ContentPage
    {
        const string SPDocumentLibraryURL = "https://sptechnophiles.sharepoint.com/_api/web/GetFolderByServerRelativeUrl('{0}')/{1}";
        public DocumentLibrary()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            List<DataContracts.Result> dataSource = new List<DataContracts.Result>();
            var folders = await GetDocuments("/SampleDocuments", false);
            if (folders != null)
            {
                foreach (var item in folders)
                {
                    item.Icon = "folder.png";
                }
                dataSource.AddRange(folders);
            }

            var files = await GetDocuments("/SampleDocuments", true);
            if (files != null)
            {
                foreach (var item in files)
                {
                    item.Icon = "file.png";
                }
                dataSource.AddRange(files);
            }

            documentList.ItemsSource = dataSource;
            documentList.ItemSelected += DocumentList_ItemSelected;
        }

        private void DocumentList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //throw new NotImplementedException();
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
    }
}