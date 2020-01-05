using PCLStorage;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DevEnvAzure.Models
{
    public class Attachment
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }

        //used for offline items - sharepoint itemId
        public string SaveToURL { get; set; }

        public Attachment()
        {

        }

        public Attachment(string path)
        {
            FilePath = path;
            FileName = Path.GetFileName(path);
        }

        public async Task<bool> Exists()
        {
            IFolder folder = await FileSystem.Current.LocalStorage.GetFolderAsync(Path.GetDirectoryName(FilePath));
            var isFileExists = await folder.CheckExistsAsync(Path.GetFileName(FilePath));

            return isFileExists == ExistenceCheckResult.FileExists;
        }

        public async Task<Stream> GetStream()
        {
            if (await Exists())
            {
                var file = await FileSystem.Current.GetFileFromPathAsync(FilePath);
                if (file != null)
                    return await file.OpenAsync(FileAccess.Read);
            }

            return null;
        }

        public async Task<HttpResponseMessage> PostAttachment()
        {
            string url = SaveToURL;
            if (string.IsNullOrEmpty(url)) return null;

            Stream stream = await GetStream();
            if (stream == null) return null;

            var client = await OAuthHelper.GetHTTPClientAsync();
            var response = await client.PostAsync(url, new StreamContent(stream));
            return response;
        }

        //public async Task<HttpResponseMessage> GetItem()
        //{
        //    if (string.IsNullOrEmpty(ItemURL)) return null;

        //    var client = await OAuthHelper.GetHTTPClient();
        //    var response = await client.GetAsync(ItemURL);
        //    return response;
        //}
    }
}
