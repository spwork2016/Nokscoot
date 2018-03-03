using System;
namespace DevEnvAzure
{
    public partial class ClientConfiguration
    {
        public static ClientConfiguration Default { get { return ClientConfiguration.OneBox; } }
        public static ClientConfiguration OneBox = new ClientConfiguration()
        {
            UriString = "https://usnconeboxax1aos.cloud.onebox.dynamics.com/",
            ActiveDirectoryResource = "https://nok365.sharepoint.com/",
            //ActiveDirectoryTenant = "https://login.microsoftonline.com/9c32f22b-4146-43ec-b31c-b65699517707/oauth2/token",
            ActiveDirectoryTenant = "https://login.microsoftonline.com/542381e6-b9d2-4fe3-a20b-e575f656c08c/oauth2/token",
            ActiveDirectoryClientAppId = "d4c9dc64-803f-4dce-842c-380ce91f60d4", //sptechnophiles            // Insert here the application secret when authenticate with AAD by the application  
            //ActiveDirectoryClientAppId = "561ad9e0-db4b-4cdd-b050-16a08d9f8709",
            //Client secret is for web app/api , the belwo is invalid - if you want web app access correct the secret and use GetAuthenticationHeader(useWebAppAuthentication = true)
            ActiveDirectoryClientAppSecret = "mC1DXLlOCNIeXIaDQ0e305kVIGHs9e7Xc4oUHSP2vwY=",            // Change TLS version of HTTP request from the client here            // Ex: TLSVersion = "1.2"            // Leave it empty if want to use the default version          
            TLSVersion = "",
            SPRootURL = "https://nok365.sharepoint.com/_api/",
            SPRootURLList = "https://nok365.sharepoint.com/_api/web/lists/"
        };

        public string TLSVersion { get; set; }
        public string UriString { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ActiveDirectoryResource { get; set; }
        public String ActiveDirectoryTenant { get; set; }
        public String ActiveDirectoryClientAppId { get; set; }
        public string ActiveDirectoryClientAppSecret { get; set; }
        public string SPRootURL { get; set; }
        public string SPRootURLList { get; set; }
    }
}