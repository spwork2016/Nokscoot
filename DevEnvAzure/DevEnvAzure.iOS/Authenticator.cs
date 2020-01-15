using System;
using System.Linq;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using UIKit;
using Xamarin.Forms;
using System.Threading.Tasks;
using Foundation;

[assembly: Dependency(typeof(DevEnvAzure.iOS.Authenticator))]
namespace DevEnvAzure.iOS
{
    class Authenticator : IAuthenticator
    {
        public async Task<AuthenticationResult> Authenticate(string authority, string graphResourceUri, string ApplicationID, string returnUri)
        {
            try
            {
                var authContext = new AuthenticationContext(authority);
                if (authContext.TokenCache.ReadItems().Any())
                    authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().FirstOrDefault().Authority);

                AuthenticationResult result = null;

                try
                {
                    result = await authContext.AcquireTokenAsync(graphResourceUri, ApplicationID, new Uri(returnUri), new PlatformParameters(UIApplication.SharedApplication.KeyWindow.RootViewController));

                    //result = await authContext.AcquireTokenSilentAsync(graphResourceUri, ApplicationID);
                }
                catch (AdalException adlException)
                {
                    if (adlException.ErrorCode == "user_interaction_required")
                    {
                        try
                        {
                            result = await authContext.AcquireTokenAsync(graphResourceUri, ApplicationID, new Uri(returnUri), new PlatformParameters(UIApplication.SharedApplication.KeyWindow.RootViewController));
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                catch (Exception unknownEx)
                {

                }

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void Logout()
        {
            NSHttpCookieStorage CookieStorage = NSHttpCookieStorage.SharedStorage;
            foreach (var cookie in CookieStorage.Cookies)
            {
                CookieStorage.DeleteCookie(cookie);
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
                    authResult = await authContext.AcquireTokenAsync(graphResourceUri, ApplicationID, new Uri(returnUri), new PlatformParameters(UIApplication.SharedApplication.KeyWindow.RootViewController));
                }
                return authResult;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}