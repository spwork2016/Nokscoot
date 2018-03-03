using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms;

[assembly: Dependency(typeof(DevEnvAzure.UWP.Authenticator))]
namespace DevEnvAzure.UWP
{
    public class Authenticator : IAuthenticator
    {
        public async Task<AuthenticationResult> Authenticate(string tenantUrl, string graphResourceUri, string ApplicationID, string returnUri)
        {
            try
            {
                var authContext = new AuthenticationContext(tenantUrl);
                if (authContext.TokenCache.ReadItems().Any())
                    authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
                var authResult =
                    await
                        authContext.AcquireTokenAsync(graphResourceUri, ApplicationID, new Uri(returnUri),
                            new PlatformParameters(PromptBehavior.Auto, false));
                return authResult;
            }
            catch(Exception )
            {
                return null;
            }
        }
    }
}
