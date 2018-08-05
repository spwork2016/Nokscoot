using DevEnvAzure.Model;
using DevEnvAzure.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DevEnvAzure
{
    public static class SPUtility
    {
        public const string SEPARATOR = "|<>|";
        public const string ATTACHMENT_FILES_NOT_FOUND = "Some of the attachments were not available. Please re-check the attachments";
        public const string REFRESH_OFFLINE_ITEMS = "REFRESH_OFFLINE_ITEMS";
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
            DelayHandling = 12,
            MORType = 13,
            OperationPlan = 14,
            AircraftRegistration = 15,
            none = -1
        }

        public enum LookupType
        {
            AircraftRegistraions = 1,
            FlightNumbers = 2
        }

        public static string[] GetPathsFromAttachemntInfo(string attachmentInfo)
        {
            if (string.IsNullOrEmpty(attachmentInfo)) return null;

            return attachmentInfo.Split(new string[] { SEPARATOR }, StringSplitOptions.None);
        }

        public static string GetListURL(ReportType reportType = ReportType.none)
        {
            //throw new Exception("Are you sure sending a data to prod?");
            string url = "";

            switch (reportType)
            {
                case ReportType.CabinSafety:
                case ReportType.Fatigue:
                case ReportType.FlightSafety:
                case ReportType.GroundSafety:
                case ReportType.InjuryIllness:
                case ReportType.Security:
                    url = ClientConfiguration.Default.ActiveDirectoryResource + "SSQServices/_api/web/lists/GetByTitle('Operational_Hazard_Event_Register')/items";
                    break;
                case ReportType.SationInfo:
                    url = ClientConfiguration.Default.ActiveDirectoryResource + "SSQServices/_api/web/lists/GetByTitle('(Ops) Line Station Information')/items";
                    break;
                case ReportType.Kaizen:
                    url = ClientConfiguration.Default.ActiveDirectoryResource + "SSQServices/_api/web/lists/GetByTitle('Kaizen Report')/items";
                    break;
                case ReportType.MORType:
                    url = ClientConfiguration.Default.ActiveDirectoryResource + "SSQServices/_api/web/Lists(guid'32d46a92-0ee3-4a85-83ab-12ca72dce65e')/items";
                    break;
                case ReportType.FlighCrewVoyage:
                    url = ClientConfiguration.Default.ActiveDirectoryResource + "SSQServices/_api/web/lists/GetByTitle('Flight Crew Voyage Record')/items";
                    break;
                case ReportType.OperationPlan:
                    url = ClientConfiguration.Default.ActiveDirectoryResource + "SSQServices/_api/web/lists/GetByTitle('NokScoot Operating Plan')/items";
                    break;
                case ReportType.AircraftRegistration:
                    url = ClientConfiguration.Default.ActiveDirectoryResource + "SSQServices/_api/web/lists/GetByTitle('Aicraft Fleet Information')/items";
                    break;
                case ReportType.none:
                    url = ClientConfiguration.Default.ActiveDirectoryResource + "SSQServices/_api/web/lists/GetByTitle('Operational_Hazard_Event_Register')/items";
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

        private static List<PeoplePicker> GetUsers()
        {
            var userInfo = App.DAUtil.GetMasterInfoByName("Users");
            if (userInfo != null)
                return ResponseToUsers(userInfo.content);

            return null;
        }

        public static async Task<List<PeoplePicker>> GetUsersForPicker()
        {
            if (!SPUtility.IsConnected())
            {
                return GetUsers();
            }

            var client = await OAuthHelper.GetHTTPClient();

            try
            {
                var response = await client.GetStringAsync(ClientConfiguration.Default.SPRootURL + "web/siteusers?");
                if (response != null)
                {
                    App.DAUtil.RefreshMasterInfo(new MasterInfo { Name = "Users", content = response });
                    return ResponseToUsers(response);

                }
            }
            catch (Exception ex)
            {
                return GetUsers();
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

        public static async Task<Attachment> TakePhotoAsync()
        {
            await CrossMedia.Current.Initialize();

            var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                cameraStatus = results[Permission.Camera];
                storageStatus = results[Permission.Storage];
            }

            string fileName = $"IMG_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}.jpg";

            if (cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted)
            {
                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    ModalPresentationStyle = MediaPickerModalPresentationStyle.OverFullScreen,
                    SaveToAlbum = true,
                    Name = fileName,
                    Directory = ClientConfiguration.Default.APPNAME,
                    AllowCropping = true,
                    PhotoSize = PhotoSize.Small
                });

                if (file != null)
                {
                    string path = file.Path;
                    file.Dispose();
                    return new Attachment { FilePath = path, FileName = fileName };
                }
            }
            else
            {
                //await DisplayAlert("Permissions Denied", "Unable to take photos.", "OK");
                //On iOS you may want to send your user to the settings screen.
                CrossPermissions.Current.OpenAppSettings();
            }

            return null;
        }

        public static async Task<Attachment> PicPhotoAsync()
        {
            await CrossMedia.Current.Initialize();
            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file != null)
            {
                string path = file.Path;
                string name = Path.GetFileName(file.Path);

                file.Dispose();
                return new Attachment { FileName = name, FilePath = path };
            }

            return null;
        }

        public static OfflineItem SaveOfflineItem(string serializedBody, ReportType reportType, string attachementInfo = "")
        {
            OfflineItem dt = new OfflineItem
            {
                Created = DateTime.Now,
                ReportType = (int)reportType,
                Value = serializedBody,
                Attachments = attachementInfo
            };

            dt = App.DAUtil.Save<OfflineItem>(dt);
            App.offlineItems.Add(dt);
            return dt;
        }

        public static async Task<List<Value>> GetAttachedFilesForItem(string itemUrl)
        {
            var client = await OAuthHelper.GetHTTPClient();
            var response = await client.GetStringAsync($"{itemUrl}/AttachmentFiles");
            if (response != null)
            {
                var files = JsonConvert.DeserializeObject<LookUp>(response);
                return files.value;
            }

            return null;
        }

        public static async Task<DataContracts.StationInformationSp> GetStationInfoItem(int id)
        {
            var client = await OAuthHelper.GetHTTPClient();
            string url = GetListURL(ReportType.SationInfo) + $"({id})";
            try
            {
                var response = await client.GetStringAsync(url);
                var sInfo = JsonConvert.DeserializeObject<StationInformationSpRoot>(response,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
                return sInfo.d;
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        public static async Task<string[]> GetAirCraftRegistrations()
        {
            MasterInfo mInfo = null;
            if (!SPUtility.IsConnected())
            {
                mInfo = App.DAUtil.GetMasterInfoByName("AircraftRegistrations");
            }
            else
            {
                try
                {
                    var client = await OAuthHelper.GetHTTPClient();
                    var response = await client.GetStringAsync(GetListURL(ReportType.AircraftRegistration));
                    if (response != null)
                    {
                        mInfo = new MasterInfo { Name = "AircraftRegistrations", content = response };
                        App.DAUtil.RefreshMasterInfo(mInfo);
                    }
                }
                catch (Exception)
                {
                    mInfo = App.DAUtil.GetMasterInfoByName("AircraftRegistrations");
                }
            }

            if (mInfo != null)
            {
                var stations = JsonConvert.DeserializeObject<SPData>(mInfo.content);
                return stations.d.results.Select(x => x.Aircraft_x0020_Registration).ToArray();
            }

            return null;
        }

        public static async Task<Dictionary<int, string>> GetStations()
        {
            MasterInfo mInfo = null;
            if (!SPUtility.IsConnected())
            {
                mInfo = App.DAUtil.GetMasterInfoByName("Stations");
            }
            else
            {
                try
                {
                    var client = await OAuthHelper.GetHTTPClient();
                    var response = await client.GetStringAsync(GetListURL(ReportType.SationInfo));
                    if (response != null)
                    {
                        mInfo = new MasterInfo { Name = "Stations", content = response };
                        App.DAUtil.RefreshMasterInfo(mInfo);
                    }
                }
                catch (Exception)
                {
                    mInfo = App.DAUtil.GetMasterInfoByName("Stations");
                }
            }

            if (mInfo != null)
            {
                var stations = JsonConvert.DeserializeObject<SPData>(mInfo.content);
                return stations.d.results.Select(x => new { Key = x.Id, Value = x.IATA_x0020_Code }).ToDictionary(v => v.Key, v => v.Value);
            }

            return null;
        }

        public static string GetLookupIdFromValue(LookupType lType, string value)
        {
            string lookup = "";
            switch (lType)
            {
                case LookupType.AircraftRegistraions:
                    lookup = "AircraftRegistrations";
                    break;
                case LookupType.FlightNumbers:
                    lookup = "OperatingPlans";
                    break;
                default:
                    break;
            }

            MasterInfo mInfo = mInfo = App.DAUtil.GetMasterInfoByName(lookup);
            var oPlans = JsonConvert.DeserializeObject<SPData>(mInfo.content);
            int id = 0;
            Result res = null;

            switch (lType)
            {
                case LookupType.AircraftRegistraions:
                    res = oPlans.d.results.FirstOrDefault(x => x.Aircraft_x0020_Registration == value);
                    break;
                case LookupType.FlightNumbers:
                    res = oPlans.d.results.FirstOrDefault(x => x.Flight_x0020_Number == value);
                    break;
                default:
                    break;
            }

            if (res != null)
            {
                id = res.ID;
            }

            return Convert.ToString(id);
        }

        public static async Task<List<OperatingPlan>> GetOperatingPlans()
        {
            MasterInfo mInfo = null;
            if (!SPUtility.IsConnected())
            {
                mInfo = App.DAUtil.GetMasterInfoByName("OperatingPlans");
            }
            else
            {
                try
                {
                    var client = await OAuthHelper.GetHTTPClient();
                    var response = await client.GetStringAsync(GetListURL(ReportType.OperationPlan));
                    if (response != null)
                    {
                        mInfo = new MasterInfo { Name = "OperatingPlans", content = response };
                        App.DAUtil.RefreshMasterInfo(mInfo);
                    }
                }
                catch (Exception)
                {
                    mInfo = App.DAUtil.GetMasterInfoByName("OperatingPlans");
                }
            }

            if (mInfo != null)
            {
                var stations = await GetStations();

                var oPlans = JsonConvert.DeserializeObject<SPData>(mInfo.content);
                return oPlans.d.results.Select(x => new OperatingPlan
                {
                    FlighNumber = x.Flight_x0020_Number,
                    ArrivalStationId = x.Arrival_x0020_StationId,
                    DepartureStationId = x.Departure_x0020_StationId,

                    ArrivalStation = stations[x.Arrival_x0020_StationId],
                    DepartureStation = stations[x.Departure_x0020_StationId]
                }).ToList();
            }

            return null;
        }

        public static bool IsConnected()
        {
            if (Device.RuntimePlatform == Device.iOS) return CrossConnectivity.Current.IsConnected;
            else if (Device.RuntimePlatform == Device.Android)
            {
                var networkConnection = DependencyService.Get<INetworkConnection>();
                networkConnection.CheckNetworkConnection();
                return networkConnection.IsConnected;
            }

            return false;
        }
    }
}
