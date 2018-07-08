using PCLStorage;
using System.IO;
using System.Threading.Tasks;

namespace DevEnvAzure.Models
{
    public class Attachment
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }

        public Attachment()
        {

        }

        public Attachment(string path)
        {
            FilePath = path;
            FileName = Path.GetFileName(path);
        }

        public async Task<Stream> GetStream()
        {
            if (string.IsNullOrEmpty(FilePath)) return null;

            IFolder folder = await FileSystem.Current.LocalStorage.GetFolderAsync(Path.GetDirectoryName(FilePath));
            var isFileExists = await folder.CheckExistsAsync(Path.GetFileName(FilePath));

            if (isFileExists == ExistenceCheckResult.FileExists)
            {
                var file = await FileSystem.Current.GetFileFromPathAsync(FilePath);
                return await file.OpenAsync(FileAccess.Read);
            }

            return null;
        }
    }
}
