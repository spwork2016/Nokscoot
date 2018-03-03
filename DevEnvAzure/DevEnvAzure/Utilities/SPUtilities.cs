using DevEnvAzure.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DevEnvAzure
{
    public static class SPUtility
    {
        public static async Task<Result> ValidateUser(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)) return null;

            try
            {
                var info = App.DAUtil.GetMasterInfoByName("Users");
                var spData = JsonConvert.DeserializeObject<SPData>(info.content, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
                Result found = null;
                foreach (var item in spData.d.results)
                {
                    if (name.ToLower() == item.Title.ToLower())
                    {
                        found = item;
                        break;
                    }
                }

                return found;

                //if (found == null && OAuthHelper.IsLoggedIn())
                //{
                //    string url = ClientConfiguration.Default.SPRootURL + "SP.UI.ApplicationPages.ClientPeoplePickerWebServiceInterface.clientPeoplePickerSearchUser";
                //    var client = OAuthHelper.GetHTTPClient();
                //    var httpContent = new HttpContent();
                //    var result = await client.PostAsync(url);
                //}
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public static async Task<List<string>> GetUsersForPicker()
        {
            var client = OAuthHelper.GetHTTPClient();

            try
            {
                var response = await client.GetStringAsync(ClientConfiguration.Default.SPRootURL + "web/siteusers?");
                if (response != null)
                {
                    App.DAUtil.RefreshMasterInfo(new MasterInfo { Name = "Users", content = response });
                    var spData = JsonConvert.DeserializeObject<SPData>(response, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
                    if (spData != null && spData.d.results.Count > 0)
                    {
                        List<string> usrs = new List<string>();
                        foreach (var item in spData.d.results)
                        {
                            usrs.Add(item.Title);
                        }
                        return usrs;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public static async Task<string> GetProfilePicUrl()
        {
            try
            {
                string picURL = ClientConfiguration.Default.SPRootURL + "SP.UserProfiles.PeopleManager/GetMyProperties?$select=PictureUrl";
                var client = OAuthHelper.GetHTTPClient();
                var response = await client.GetStringAsync(picURL);
                if (response != null)
                {
                    var spData = JsonConvert.DeserializeObject<SPData>(response);
                    return spData.d.PictureUrl;
                }
            }
            catch (Exception ex)
            {

            }
            return "";
        }
    }
}
