using Android.App;
using Android.Webkit;
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
                    result = await authContext.AcquireTokenAsync(graphResourceUri, ApplicationID, new Uri(returnUri), new PlatformParameters((Activity)Forms.Context));

                    //result = await authContext.AcquireTokenSilentAsync(graphResourceUri, ApplicationID);
                }
                catch (AdalException adlException)
                {
                    if (adlException.ErrorCode == "user_interaction_required")
                    {
                        try
                        {
                            result = await authContext.AcquireTokenAsync(graphResourceUri, ApplicationID, new Uri(returnUri), new PlatformParameters((Activity)Forms.Context));
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
            CookieManager.Instance.RemoveAllCookie();
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
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}