using System;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using UIKit;
using Xamarin.Forms;

[assembly : Dependency (typeof (DevEnvAzure.iOS.Authenticator))]
namespace DevEnvAzure.iOS {
	class Authenticator : IAuthenticator {
		private async Task<AuthenticationResult> AcquireTokenAsync (AuthenticationContext ac, string tenantUrl, string resourceUri, string ApplicationID, string returnUri) {
			return await ac.AcquireTokenAsync (resourceUri, ApplicationID, new Uri (returnUri),
				new PlatformParameters (UIApplication.SharedApplication.KeyWindow.RootViewController));
		}

		public async Task<AuthenticationResult> Authenticate (string tenantUrl, string resourceUri, string ApplicationID, string returnUri) {
			var ac = new AuthenticationContext (tenantUrl);
			AuthenticationResult result = null;

			try {
				result = await ac.AcquireTokenSilentAsync (resourceUri, ApplicationID);

				if (result == null) {
					result = await AcquireTokenAsync (ac, tenantUrl, resourceUri, ApplicationID, returnUri);
				}
			} catch (AdalException adalException) {
				if (adalException.ErrorCode == AdalError.FailedToAcquireTokenSilently ||
					adalException.ErrorCode == AdalError.InteractionRequired) {
					result = await AcquireTokenAsync (ac, tenantUrl, resourceUri, ApplicationID, returnUri);
				}
			} catch (Exception ex) {
				return null;
			}

			return result;
		}

		public void Logout () {
			NSHttpCookieStorage CookieStorage = NSHttpCookieStorage.SharedStorage;
			foreach (var cookie in CookieStorage.Cookies) {
				CookieStorage.DeleteCookie (cookie);
			}
		}

		public async Task<AuthenticationResult> ReAuthenticate (string tenantUrl, string graphResourceUri, string ApplicationID, string returnUri) {
			try {
				var authContext = new AuthenticationContext (tenantUrl);
				if (authContext.TokenCache.ReadItems ().Any ())
					authContext = new AuthenticationContext (authContext.TokenCache.ReadItems ().FirstOrDefault ().Authority);
				var authResult = await authContext.AcquireTokenAsync (graphResourceUri, ApplicationID, new Uri (returnUri),
					new PlatformParameters (UIApplication.SharedApplication.KeyWindow.RootViewController));
				return authResult;
			} catch (Exception) {
				return null;
			}
		}
	}
}
