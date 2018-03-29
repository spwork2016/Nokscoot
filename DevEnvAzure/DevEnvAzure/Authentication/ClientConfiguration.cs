using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DevEnvAzure
{
    public partial class ClientConfiguration
    {
        public static ClientConfiguration Default { get { return ClientConfiguration.OneBox; } }
        public static ClientConfiguration OneBox = new ClientConfiguration()
        {
            ActiveDirectoryResource = "https://sptechnophiles.sharepoint.com/",
            ActiveDirectoryTenant = "https://login.microsoftonline.com/542381e6-b9d2-4fe3-a20b-e575f656c08c/oauth2/token",
            ActiveDirectoryClientAppId = "d4c9dc64-803f-4dce-842c-380ce91f60d4", //sptechnophiles            // Insert here the application secret when authenticate with AAD by the application  
            SPRootURL = "https://sptechnophiles.sharepoint.com/_api/",
            SPRootURLList = "https://sptechnophiles.sharepoint.com/_api/web/lists/",
            SPDocumentLibraryURL = "https://sptechnophiles.sharepoint.com/_api/web/GetFolderByServerRelativeUrl('/SampleDocuments')/Folders",
            GraphAPIRootURL = "https://graph.windows.net/",
            GraphAPIURL = "https://graph.windows.net/542381e6-b9d2-4fe3-a20b-e575f656c08c",

            //ActiveDirectoryResource = "https://nok365.sharepoint.com/",
            //ActiveDirectoryTenant = "https://login.microsoftonline.com/9c32f22b-4146-43ec-b31c-b65699517707/oauth2/token",
            //SPRootURL = "https://nok365.sharepoint.com/_api/",
            //SPRootURLList = "https://nok365.sharepoint.com/_api/web/lists/"
            //ActiveDirectoryClientAppId = "561ad9e0-db4b-4cdd-b050-16a08d9f8709",

            TLSVersion = ""
        };

        public string TLSVersion { get; set; }
        public string UriString { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ActiveDirectoryResource { get; set; }
        public String ActiveDirectoryTenant { get; set; }
        public String ActiveDirectoryClientAppId { get; set; }
        //Client secret is for web app/api , the below is invalid - if you want web app access correct the secret and use GetAuthenticationHeader(useWebAppAuthentication = true)
        public string ActiveDirectoryClientAppSecret { get; set; }
        public string SPRootURL { get; set; }
        public string SPRootURLList { get; set; }
        public string SPDocumentLibraryURL { get; set; }
        public string GraphAPIRootURL { get; set; }
        public string GraphAPIURL { get; set; }
    }
}