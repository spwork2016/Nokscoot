using System;
using System.Linq;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using UIKit;
using Xamarin.Forms;
using System.Threading.Tasks;

[assembly: Dependency(typeof(DevEnvAzure.iOS.Authenticator))]
namespace DevEnvAzure.iOS
{
    class Authenticator : IAuthenticator
    {
        public async Task<AuthenticationResult> Authenticate(string tenantUrl, string resourceUri, string ApplicationID, string returnUri)
        {
            try
            {
                var authContext = new AuthenticationContext(tenantUrl);
                if (authContext.TokenCache.ReadItems().Any())
                    authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().FirstOrDefault().Authority);
                var authResult = await authContext.AcquireTokenAsync(resourceUri, ApplicationID, new Uri(returnUri),
                    new PlatformParameters(UIApplication.SharedApplication.KeyWindow.RootViewController));
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
                if (authContext.TokenCache.ReadItems().Any())
                    authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().FirstOrDefault().Authority);
                var authResult = await authContext.AcquireTokenAsync(graphResourceUri, ApplicationID, new Uri(returnUri),
                    new PlatformParameters(UIApplication.SharedApplication.KeyWindow.RootViewController));
                return authResult;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}