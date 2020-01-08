using DevEnvAzure.Model;
using Microsoft.Graph;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DevEnvAzure
{
    public class OAuthHelper
    {
        private static readonly HttpProvider httpProvider = new HttpProvider(new HttpClientHandler(), false);
        /// <summary>
        /// The header to use for OAuth authentication.
        /// </summary>
        public const string OAuthHeader = "Authorization";
        public const string GraphURL = "https://graph.windows.net/542381e6-b9d2-4fe3-a20b-e575f656c08c";

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

                //try
                //{
                //    // Get token object
                //    var userCredential = new UserPasswordCredential(username, password);
                //    authenticationResult = authenticationContext.AcquireTokenAsync(aadResource, aadClientAppId, userCredential).Result;
                //}
                //catch (Exception ex)
                //{
                //    //Console.WriteLine(string.Format("Failed to authenticate with AAD by the credential with exception {0} and the stack trace {1}", ex.ToString(), ex.StackTrace));
                //    throw new Exception("Failed to authenticate with AAD by the credential.");
                //}
            }

            return false;
        }

        //public static async Task<AuthenticationResponse> GetAccessToken(string username, string password, string aadClientAppId, string aadResource, string aadTenant)
        //{
        //    var pairs = new List<KeyValuePair<string, string>>
        //            {
        //                new KeyValuePair<string, string>("grant_type", "password"),
        //                new KeyValuePair<string, string>("username", username),
        //                new KeyValuePair<string, string>("password", password),
        //                new KeyValuePair<string, string>("client_id", aadClientAppId),
        //                new KeyValuePair<string, string>("resource", aadResource)
        //            };

        //    HttpResponseMessage response = null;
        //    try
        //    {
        //        var content = new FormUrlEncodedContent(pairs);
        //        var client = new HttpClient { BaseAddress = new Uri(aadTenant) };

        //        // call sync
        //        response = await client.PostAsync("", content);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string str = await response.Content.ReadAsStringAsync();
        //            App.DAUtil.RefreshMasterInfo(new MasterInfo { content = str, Name = "Authentication" });
        //            AuthenticationResponse json = JsonConvert.DeserializeObject<AuthenticationResponse>(str);
        //            return json;
        //        }
        //        else
        //        {
        //            string str2 = await response.Content.ReadAsStringAsync();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return null;
        //}

        public static async Task<int> SyncOfflineItems()
        {
            var eValue = App.DAUtil.GetAll<OfflineItem>("OfflineItem");
            if (eValue != null && eValue.Count > 0)
            {
                return await DataUpload.CreateItemsOffline(eValue);
            }

            return 0;
        }

        public static async Task<HttpClient> GetHTTPClientAsync()
        {
            //await GetAccessToken();

            if (App.AuthResult == null) return null;

            HttpClient client = new HttpClient();
            // if not adal - used application/json;odata=verbose
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("ContentType", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", App.AuthResult.CreateAuthorizationHeader());

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

            var client = GetHTTPClientAsync();
            if (client == null)
            {
                return null;
            }

            try
            {
                //var response = await client.GetStringAsync(ClientConfiguration.Default.SPRootURL + "web/currentuser");
                //if (response != null)
                //{
                var userInfo = App.AuthResult?.UserInfo;
                App.DAUtil.RefreshMasterInfo(new MasterInfo { Name = "UserInfo", content = JsonConvert.SerializeObject(userInfo) });
                if (App.AuthResult?.UserInfo != null)
                {
                    App.CurrentUser = new Model.User
                    {
                        Id = userInfo.UniqueId,
                        Name = userInfo.GivenName + " " + userInfo.FamilyName,
                        Email = userInfo.DisplayableId,
                        PictureBytes = await GetPicture()
                    };
                }
                //}

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
                await GetAccessToken();
                if (App.AuthResult != null)
                {
                    var auth = DependencyService.Get<IAuthenticator>();
                    var nokScootConfig = ClientConfiguration.NokScoot;

                    var client = await GetHTTPClientAsync();
                    var picResponse = await client.GetByteArrayAsync(nokScootConfig.GraphAPIRootURL + "v1.0/me/photo/$value");
                    return picResponse;
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public static bool IsLoggedIn()
        {
            if (!SPUtility.IsConnected())
            {
                MasterInfo auth = App.DAUtil.GetMasterInfoByName("Authentication");
                if (auth != null)
                {
                    AuthenticationResult json = JsonConvert.DeserializeObject<AuthenticationResult>(auth.content);
                    App.AuthResult = json;
                }

                MasterInfo userInfo = App.DAUtil.GetMasterInfoByName("UserInfo");
                if (userInfo != null)
                {
                    var spData = JsonConvert.DeserializeObject<UserInfo>(userInfo.content, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
                    if (spData != null)
                        App.CurrentUser = new Model.User { Name = spData.GivenName + " " + spData.FamilyName, Email = spData.DisplayableId };
                }
            }

            if (App.AuthResult == null) return false;

            //For offline fuctionality
            if (App.AuthResult != null && !SPUtility.IsConnected()) return true;

            return SPUtility.IsConnected() && App.AuthResult.ExpiresOn > DateTime.UtcNow;
        }

        //public static async Task<bool> RefreshAccessToken()
        //{
        //    if (!SPUtility.IsConnected()) return false;

        //    if (App.AuthenticationResponse == null)
        //    {
        //        MasterInfo auth = App.DAUtil.GetMasterInfoByName("Authentication");
        //        if (auth != null)
        //        {
        //            AuthenticationResponse json = JsonConvert.DeserializeObject<AuthenticationResponse>(auth.content);
        //            App.AuthenticationResponse = json;
        //        }
        //        else return false;
        //    }

        //    DateTime expries = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        //    expries = expries.AddSeconds(long.Parse(App.AuthenticationResponse.expires_on)).ToLocalTime();

        //    if (expries > DateTime.Now) return true;
        //    else
        //    {
        //        MasterInfo userInfo = App.DAUtil.GetMasterInfoByName("UserCredentials");
        //        if (userInfo != null)
        //        {
        //            var cred = JsonConvert.DeserializeObject<dynamic>(userInfo.content, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
        //            if (cred != null)
        //            {
        //                string aadTenant = ClientConfiguration.Default.ActiveDirectoryTenant;
        //                string aadClientAppId = ClientConfiguration.Default.ActiveDirectoryClientAppId;
        //                string aadResource = ClientConfiguration.Default.ActiveDirectoryResource;

        //                string uname = cred.Username;
        //                string pwd = cred.Password;
        //                var authResponse = await GetAccessToken(uname, pwd, aadClientAppId, aadResource, aadTenant);
        //                if (authResponse != null)
        //                {
        //                    var str = JsonConvert.SerializeObject(new { cred.Username, cred.Password });
        //                    App.DAUtil.RefreshMasterInfo(new MasterInfo { content = str, Name = "UserCredentials" });
        //                    App.AuthenticationResponse = authResponse;
        //                    return true;
        //                }
        //            }
        //        }
        //    }

        //    return false;
        //}

        public static async Task GetAccessToken()
        {
            try
            {
                var auth = DependencyService.Get<IAuthenticator>();
                var nokScootConfig = ClientConfiguration.NokScoot;

                App.AuthResult = await auth.Authenticate(nokScootConfig.ActiveDirectoryTenant,
                                                            nokScootConfig.GraphAPIRootURL,
                                                            nokScootConfig.ActiveDirectoryClientAppId,
                                                            nokScootConfig.ActiveDirectoryResource);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static GraphServiceClient GetGraphClient(string accessToken, IHttpProvider provider = null)
        {
            var delegateAuthProvider = new DelegateAuthenticationProvider((requestMessage) =>
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                return Task.FromResult(0);
            });

            var graphClient = new GraphServiceClient(delegateAuthProvider, provider ?? httpProvider);

            return graphClient;
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

    //public class AuthenticationResponse
    //{
    //    public string token_type { get; set; }
    //    public string scope { get; set; }
    //    public string expires_in { get; set; }
    //    public string ext_expires_in { get; set; }
    //    public string expires_on { get; set; }
    //    public string not_before { get; set; }
    //    public string resource { get; set; }
    //    public string access_token { get; set; }
    //    public string refresh_token { get; set; }
    //}
}