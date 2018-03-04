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
           

            TLSVersion = ""
        };

        public string TLSVersion { get; set; }
        public string UriString { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ActiveDirectoryResource { get; set; }
        public String ActiveDirectoryTenant { get; set; }
        public String ActiveDirectoryClientAppId { get; set; }
        //Client secret is for web app/api , the belwo is invalid - if you want web app access correct the secret and use GetAuthenticationHeader(useWebAppAuthentication = true)
        public string ActiveDirectoryClientAppSecret { get; set; }
        public string SPRootURL { get; set; }
        public string SPRootURLList { get; set; }
    }
}