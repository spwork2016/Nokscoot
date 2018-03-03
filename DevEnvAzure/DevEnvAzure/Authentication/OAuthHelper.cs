using DevEnvAzure.Model;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DevEnvAzure
{
    public class OAuthHelper
    {
        /// <summary>
        /// The header to use for OAuth authentication.
        /// </summary>
        public const string OAuthHeader = "Authorization";
        public const string GraphURL = "https://graph.windows.net/542381e6-b9d2-4fe3-a20b-e575f656c08c";

        /// <summary>
        /// Retrieves an authentication header from the service.
        /// </summary>
        /// <returns>The authentication header for th.e Web API call.</returns>
        public static async Task<bool> GetAuthenticationHeader(string username, string password, bool useWebAppAuthentication = false)
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

                var pairs = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", username),
                        new KeyValuePair<string, string>("password", password),
                        new KeyValuePair<string, string>("client_id", aadClientAppId),
                        new KeyValuePair<string, string>("resource", aadResource)
                    };

                HttpResponseMessage response = null;
                try
                {
                    var content = new FormUrlEncodedContent(pairs);
                    var client = new HttpClient { BaseAddress = new Uri(aadTenant) };

                    // call sync
                    response = client.PostAsync("", content).Result;
                    //response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        string str = await response.Content.ReadAsStringAsync();
                        AuthenticationResponse json = JsonConvert.DeserializeObject<AuthenticationResponse>(str);
                        App.AuthenticationResponse = json;
                        return true;
                    }
                    else
                    {
                        string ss = await response.Content.ReadAsStringAsync();
                    }
                }
                catch (Exception ex)
                {
                }
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

        public static HttpClient GetHTTPClient()
        {
            if (App.AuthenticationResponse == null)
            {
                return null;
            }

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
            client.DefaultRequestHeaders.Add("ContentType", "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(App.AuthenticationResponse.token_type, App.AuthenticationResponse.access_token);
            //MediaTypeWithQualityHeaderValue mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            //mediaType.Parameters.Add(new NameValueHeaderValue("odata", "verbose"));
            //client.DefaultRequestHeaders.Accept.Add(mediaType);

            return client;
        }

        public static async Task<User> GetUserInfo()
        {
            var client = GetHTTPClient();
            if (client == null) { return null; }

            try
            {
                var response = await client.GetStringAsync(ClientConfiguration.Default.SPRootURL + "web/currentUser?");
                if (response != null)
                {
                    var spData = JsonConvert.DeserializeObject<SPData>(response, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
                    if (spData != null)
                    {
                        App.CurrentUser = new User { Name = spData.d.Title, Email = spData.d.Email, PictureUrl = await SPUtility.GetProfilePicUrl() };

                        //string picUrl = ClientConfiguration.Default.SPRootURL + "_layouts/15/userphoto.aspx?size=M&accountname = " + spData.d.LoginName;
                        //var picResponse = await client.GetStringAsync(picUrl);
                        //if (picResponse != null)
                        //{

                        //}
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public static bool IsLoggedIn()
        {
            if (App.AuthenticationResponse == null) return false;

            //For offline fuctionality
            if (App.AuthenticationResponse != null && !CrossConnectivity.Current.IsConnected) return true;

            DateTime expries = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            expries = expries.AddSeconds(long.Parse(App.AuthenticationResponse.expires_on)).ToLocalTime();

            return CrossConnectivity.Current.IsConnected && expries > DateTime.Now;
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

    public class AuthenticationResponse
    {
        public string token_type { get; set; }
        public string scope { get; set; }
        public string expires_in { get; set; }
        public string ext_expires_in { get; set; }
        public string expires_on { get; set; }
        public string not_before { get; set; }
        public string resource { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
    }
}