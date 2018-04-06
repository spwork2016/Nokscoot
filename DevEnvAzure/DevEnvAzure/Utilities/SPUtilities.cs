using DevEnvAzure.Model;
using DevEnvAzure.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
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
        public enum ReportType
        {
            Fatigue = 1,
            Kaizen = 2,
            SationInfo = 3,
            InjuryIllness = 4,
            FlightSafety = 5,
            CabinSafety = 6,
            GroundSafety = 7,
            Security = 8,
            FlighCrewVoyage = 9,
            TechnicalDElay = 10,
            DelayOccurance = 11,
            DelayHandling = 12

        }

        public static string GetListURL(ReportType reportType)
        {
            string url = "";

            switch (reportType)
            {
                case ReportType.CabinSafety:
                case ReportType.Fatigue:
                case ReportType.FlighCrewVoyage:
                case ReportType.FlightSafety:
                case ReportType.GroundSafety:
                case ReportType.InjuryIllness:
                case ReportType.Security:
                    url = string.Format(ClientConfiguration.Default.SPListURL, "Operational_Hazard_Event_Register_04042018");
                    break;
                case ReportType.SationInfo:
                    url = string.Format(ClientConfiguration.Default.SPListURL, "(Ops) Line Station Information");
                    break;
                case ReportType.Kaizen:
                    url = string.Format(ClientConfiguration.Default.SPListURL, "Kaizen Report");
                    break;
                default:
                    break;
            }

            return url;
        }

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

        private static List<PeoplePicker> ResponseToUsers(string response)
        {
            var spData = JsonConvert.DeserializeObject<SPData>(response, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
            if (spData != null && spData.d.results.Count > 0)
            {
                List<PeoplePicker> usrs = new List<PeoplePicker>();
                foreach (var item in spData.d.results)
                {
                    if (!string.IsNullOrEmpty(item.Email) && usrs.FirstOrDefault(x => x.Id == item.Id) == null)
                        usrs.Add(new PeoplePicker { Name = item.Title, Id = item.Id, LoginName = item.LoginName });
                }

                return usrs;
            }

            return null;
        }

        public static async Task<List<PeoplePicker>> GetUsersForPicker()
        {
            var client = await OAuthHelper.GetHTTPClient();

            try
            {

                if (!CrossConnectivity.Current.IsConnected)
                {
                    var userInfo = App.DAUtil.GetMasterInfoByName("Users");
                    if (userInfo != null)
                        return ResponseToUsers(userInfo.content);
                }

                var response = await client.GetStringAsync(ClientConfiguration.Default.SPRootURL + "web/siteusers?");
                if (response != null)
                {
                    App.DAUtil.RefreshMasterInfo(new MasterInfo { Name = "Users", content = response });
                    return ResponseToUsers(response);

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
                var client = await OAuthHelper.GetHTTPClient();
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
