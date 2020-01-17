using DevEnvAzure.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DevEnvAzure
{
    public class OAuthHelper
    {
        public static bool IsAuthenticationRequestInProgress { get; set; }
        /// <summary>
        /// Retrieves an authentication header from the service.
        /// </summary>
        /// <returns>The authentication header for th.e Web API call.</returns>
        public static async Task<bool> GetAuthenticationHeader(string username = "", string password = "", bool useWebAppAuthentication = false)
        {
            string aadTenant = ClientConfiguration.Default.ActiveDirectoryTenant;
            string aadClientAppId = ClientConfiguration.Default.ActiveDirectoryClientAppId;
            string aadClientAppSecret = ClientConfiguration.Default.ActiveDirectoryClientAppSecret;
            string aadResource = ClientConfiguration.Default.ActiveDirectoryResource;

            AuthenticationContext authenticationContext = new AuthenticationContext(aadTenant, false);

            if (useWebAppAuthentication)
            {
                if (string.IsNullOrEmpty(aadClientAppSecret))
                {
                    //Console.WriteLine("Please fill AAD application secret in ClientConfiguration if you choose authentication by the application.");
                    throw new Exception("Failed OAuth by empty application secret.");
                }

                try
                {
                    // OAuth through application by application id and application secret.
                    var creadential = new ClientCredential(aadClientAppId, aadClientAppSecret);
                    var r = await authenticationContext.AcquireTokenAsync(aadResource, creadential);
                    if (r != null)
                    {

                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(string.Format("Failed to authenticate with AAD by application with exception {0} and the stack trace {1}", ex.ToString(), ex.StackTrace));
                    throw new Exception("Failed to authenticate with AAD by application.");
                }
            }
            else
            {
                if (string.IsNullOrEmpty(password))
                {
                    //Console.WriteLine("Please fill user password in ClientConfiguration if you choose authentication by the credential.");
                    throw new Exception("Failed OAuth by empty password.");
                }

                await GetAccessToken();
            }

            return false;
        }

        public static async Task<int> SyncOfflineItems()
        {
            var eValue = App.DAUtil.GetAll<OfflineItem>("OfflineItem");
            if (eValue != null && eValue.Count > 0)
            {
                return await DataUpload.CreateItemsOffline(eValue);
            }

            return 0;
        }

        public static async Task<HttpClient> GetHTTPClientAsync(bool IsGraphRequest = false)
        {
            //await GetAccessToken();

            if (IsGraphRequest && App.GraphAuthentication == null) return null;

            if (App.SharePointAuthentication == null) return null;

            HttpClient client = new HttpClient();
            // if not adal - used application/json;odata=verbose
            if (IsGraphRequest)
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            else
                client.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");

            client.DefaultRequestHeaders.Add("ContentType", "application/json");
            client.DefaultRequestHeaders.Add("Authorization",
                IsGraphRequest ?
                App.GraphAuthentication.CreateAuthorizationHeader() :
                App.SharePointAuthentication.CreateAuthorizationHeader());

            return client;
        }

        public static async Task<Model.User> GetUserInfo()
        {
            var uInfo = App.DAUtil.GetMasterInfoByName("UserInfo");
            if (uInfo != null)
            {
                var obj = JsonConvert.DeserializeObject<UserInfo>(uInfo.content, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
                if (obj != null)
                {
                    App.CurrentUser = new Model.User
                    {
                        Id = obj.UniqueId,
                        Name = obj.GivenName + " " + obj.FamilyName,
                        Email = obj.DisplayableId,
                    };
                }
            }

            if (!SPUtility.IsConnected())
            {
                return null;
            }

            try
            {
                var userInfo = App.GraphAuthentication?.UserInfo;
                App.DAUtil.RefreshMasterInfo(new MasterInfo { Name = App.USER_INFO_KEY, content = JsonConvert.SerializeObject(userInfo) });
                if (App.GraphAuthentication?.UserInfo != null)
                {
                    App.CurrentUser = new Model.User
                    {
                        Id = userInfo.UniqueId,
                        Name = userInfo.GivenName + " " + userInfo.FamilyName,
                        Email = userInfo.DisplayableId,
                        PictureBytes = await GetPicture()
                    };
                }

            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public static async Task<byte[]> GetPicture()
        {
            try
            {
                if (App.GraphAuthentication != null)
                {
                    var auth = DependencyService.Get<IAuthenticator>();
                    var nokScootConfig = ClientConfiguration.NokScoot;

                    var client = await GetHTTPClientAsync(true);
                    var picResponse = await client.GetByteArrayAsync(nokScootConfig.GraphAPIRootURL + "v1.0/me/photo/$value");
                    return picResponse;
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public static void GetLoggedInToken()
        {
            var graphResponse = App.DAUtil.GetMasterInfoByName(App.GRAPH_AUTH_RESULT_KEY);
            var spResponse = App.DAUtil.GetMasterInfoByName(App.SHAREPOINT_AUTH_RESULT_KEY);

            if (App.GraphAuthentication == null && graphResponse != null)
            {
                App.GraphAuthentication = JsonConvert.DeserializeObject<AuthContext>(graphResponse.content);
            }

            if (App.SharePointAuthentication == null && spResponse != null)
            {
                App.SharePointAuthentication = JsonConvert.DeserializeObject<AuthContext>(spResponse.content);
            }
        }

        public static bool IsLoggedIn()
        {
            if (!SPUtility.IsConnected())
            {
                GetLoggedInToken();

                MasterInfo userInfo = App.DAUtil.GetMasterInfoByName(App.USER_INFO_KEY);
                if (userInfo != null)
                {
                    var spData = JsonConvert.DeserializeObject<UserInfo>(userInfo.content, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
                    if (spData != null)
                        App.CurrentUser = new Model.User { Name = spData.GivenName + " " + spData.FamilyName, Email = spData.DisplayableId };
                }
            }

            if (App.GraphAuthentication == null) return false;

            //For offline fuctionality
            if (App.GraphAuthentication != null && !SPUtility.IsConnected()) return true;

            return SPUtility.IsConnected() && App.GraphAuthentication.ExpiresOn > DateTime.UtcNow;
        }

        public static async Task GetAccessToken()
        {
            if (IsAuthenticationRequestInProgress)
            {
                return;
            }

            IsAuthenticationRequestInProgress = true;

            var auth = DependencyService.Get<IAuthenticator>();
            var msg = DependencyService.Get<IMessage>();
            var nokScootConfig = ClientConfiguration.NokScoot;
            try
            {
                AuthenticationResult result = await auth.Authenticate(nokScootConfig.ActiveDirectoryTenant,
                                                            nokScootConfig.GraphAPIRootURL,
                                                            nokScootConfig.ActiveDirectoryClientAppId,
                                                            nokScootConfig.ActiveDirectoryResource);

                if (result == null)
                {
                    throw new Exception("Authentication failed.");
                }

                App.GraphAuthentication = new AuthContext(result);
            }
            catch (Exception ex)
            {
                IsAuthenticationRequestInProgress = false;
                msg.LongAlert(ex.Message);
                throw ex;
            }

            try
            {
                AuthenticationResult spResult = await auth.Authenticate(nokScootConfig.ActiveDirectoryTenant,
                                                            nokScootConfig.ActiveDirectoryResource,
                                                            nokScootConfig.ActiveDirectoryClientAppId,
                                                            nokScootConfig.ActiveDirectoryResource);

                if (spResult == null)
                {
                    throw new Exception("Authentication failed.");
                }


                App.SharePointAuthentication = new AuthContext(spResult);
            }
            catch (Exception ex)
            {
                IsAuthenticationRequestInProgress = false;
                msg.LongAlert(ex.Message);
                throw ex;
            }

            IsAuthenticationRequestInProgress = false;
        }

        public static Exception CreateExceptionFromResponseErrors(HttpResponseMessage response)
        {
            var httpErrorObject = response.Content.ReadAsStringAsync().Result;

            // Create an anonymous object to use as the template for deserialization:
            var anonymousErrorObject =
                new { message = "", ModelState = new Dictionary<string, string[]>() };

            // Deserialize:
            var deserializedErrorObject =
                JsonConvert.DeserializeAnonymousType(httpErrorObject, anonymousErrorObject);

            // Now wrap into an exception which best fullfills the needs of your application:
            var ex = new Exception();

            // Sometimes, there may be Model Errors:
            if (deserializedErrorObject.ModelState != null)
            {
                var errors =
                    deserializedErrorObject.ModelState
                                            .Select(kvp => string.Join(". ", kvp.Value));
                for (int i = 0; i < errors.Count(); i++)
                {
                    // Wrap the errors up into the base Exception.Data Dictionary:
                    ex.Data.Add(i, errors.ElementAt(i));
                }
            }
            // Othertimes, there may not be Model Errors:
            else
            {
                var error =
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(httpErrorObject);
                foreach (var kvp in error)
                {
                    // Wrap the errors up into the base Exception.Data Dictionary:
                    ex.Data.Add(kvp.Key, kvp.Value);
                }
            }
            return ex;
        }
    }

    public class AuthContext 
    {
        private AuthenticationResult AuthResult;

        public AuthContext(AuthenticationResult result)
        {
            if (result == null)
            {
                return;
            }

            AuthResult = result;

            AccessTokenType = result.AccessTokenType;
            AccessToken = result.AccessToken;
            ExpiresOn = result.ExpiresOn;
            ExtendedLifeTimeToken = result.ExtendedLifeTimeToken;
            TenantId = result.TenantId;
            UserInfo = result.UserInfo;
            IdToken = result.IdToken;

        }

        //
        // Summary:
        //     Gets the type of the Access Token returned.
        public string AccessTokenType { get; set; }
        //
        // Summary:
        //     Gets the Access Token requested.
        public string AccessToken { get; set; }
        //
        // Summary:
        //     Gets the point in time in which the Access Token returned in the AccessToken
        //     property ceases to be valid. This value is calculated based on current UTC time
        //     measured locally and the value expiresIn received from the service.
        public DateTimeOffset ExpiresOn { get; set; }
        //
        // Summary:
        //     Gives information to the developer whether token returned is during normal or
        //     extended lifetime.
        public bool ExtendedLifeTimeToken { get; set; }
        //
        // Summary:
        //     Gets an identifier for the tenant the token was acquired from. This property
        //     will be null if tenant information is not returned by the service.
        public string TenantId { get; set; }
        //
        // Summary:
        //     Gets user information including user Id. Some elements in UserInfo might be null
        //     if not returned by the service.
        public UserInfo UserInfo { get; set; }

        public string IdToken { get; set; }
        //
        // Summary:
        //     Gets the authority that has issued the token.
        public string Authority { get; set; }

        public string CreateAuthorizationHeader()
        {
            if (AuthResult != null)
            {
                return AuthResult.CreateAuthorizationHeader();
            }

            return "Bearer " + AccessToken;
        }
    }
}