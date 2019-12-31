using Android.App;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(DevEnvAzure.Droid.Authenticator))]
namespace DevEnvAzure.Droid
{
    class Authenticator : IAuthenticator
    {
        public async Task<AuthenticationResult> Authenticate(string tenantUrl, string graphResourceUri, string ApplicationID, string returnUri)
        {
            try
            {

                var authContext = new AuthenticationContext(tenantUrl);
                if (authContext.TokenCache.ReadItems().Any())
                    authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().FirstOrDefault().Authority);
                var authResult = await authContext.AcquireTokenAsync(graphResourceUri, ApplicationID, new Uri(returnUri), new PlatformParameters((Activity)Forms.Context));

                return authResult;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<AuthenticationResult> Authenticate(string tenantUrl, string returnUri, ClientCredential clientCredential)
        {
            try
            {

                var authContext = new AuthenticationContext(tenantUrl);
                if (authContext.TokenCache.ReadItems().Any())
                    authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().FirstOrDefault().Authority);
                var authResult = await authContext.AcquireTokenAsync((returnUri), clientCredential);

                return authResult;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<AuthenticationResult> ReAuthenticate(string tenantUrl, string graphResourceUri, string ApplicationID, string returnUri)
        {
            try
            {
                var authContext = new AuthenticationContext(tenantUrl);
                AuthenticationResult authResult = null;
                if (authContext.TokenCache.ReadItems().Any())
                {
                    authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().FirstOrDefault().Authority);
                    authResult = await authContext.AcquireTokenAsync(graphResourceUri, ApplicationID, new Uri(returnUri), new PlatformParameters((Activity)Forms.Context));
                }
                return authResult;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}