using DevEnvAzure.Model;
using DevEnvAzure.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DevEnvAzure
{
    public partial class App : Application
    {
        public static AuthContext GraphAuthentication = null;
        public static AuthContext SharePointAuthentication = null;
        public static string SHAREPOINT_AUTH_RESULT_KEY = "SHAREPOINT_AUTH_RESULT";
        public static string GRAPH_AUTH_RESULT_KEY = "GRAPH_AUTH_RESULT";
        public static string USER_INFO_KEY = "UserInfo";

        public static User CurrentUser = null;
        static DataAccess dbUtils;
        public static List<PeoplePicker> peoplePickerDataSource;
        public static ObservableCollection<OfflineItem> offlineItems = new ObservableCollection<OfflineItem>();
        public static ObservableCollection<object> savedDrafts = new ObservableCollection<object>();

        public static ObservableCollection<CabibSafetyReport> cabinSafety = new ObservableCollection<CabibSafetyReport>();
        public static ObservableCollection<FatigueReport> fatigue = new ObservableCollection<FatigueReport>();
        public static ObservableCollection<GroundSafetyReport> groundSafety = new ObservableCollection<GroundSafetyReport>();
        public static ObservableCollection<InjuryIllnessReport> injuryIllness = new ObservableCollection<InjuryIllnessReport>();
        public static ObservableCollection<SecurityModel> security = new ObservableCollection<SecurityModel>();
        public static ObservableCollection<FlightSafetyReportModel> safetyReport = new ObservableCollection<FlightSafetyReportModel>();

        public static ObservableCollection<KaizenReportModel> kaizen = new ObservableCollection<KaizenReportModel>();
        public static ObservableCollection<FlightCrewVoyageRecordModel> fcVoyage = new ObservableCollection<FlightCrewVoyageRecordModel>();
        public static ObservableCollection<StationInformationModel> statInfo = new ObservableCollection<StationInformationModel>();
        public static ObservableCollection<Attachment> attachments = new ObservableCollection<Attachment>();
        public static string EVENT_LAUNCH_MAIN_PAGE = "EVENT_LAUNCH_MAIN_PAGE";
        public static string EVENT_LAUNCH_MULTIFACTOR_PAGE = "EVENT_LAUNCH_MULTIFACTOR_PAGE";
        public static string AUTHENTICATION_SUCCESS = "AUTHENTICATION_SUCCESS";

        public App()
        {
            InitializeComponent();
            attachments.CollectionChanged += Attachments_CollectionChanged;

            try
            {
                var eValue = DAUtil.GetAll<OfflineItem>("OfflineItem");
                if (eValue != null && eValue.Count > 0)
                    App.offlineItems = new ObservableCollection<OfflineItem>(eValue);

                MessagingCenter.Subscribe<object>(this, EVENT_LAUNCH_MAIN_PAGE, SetMainPageAsRootPage);
                MessagingCenter.Subscribe<object>(this, EVENT_LAUNCH_MULTIFACTOR_PAGE, SetMultiFactorAuthenticationPage);

                if (SPUtility.IsConnected())
                {
                    MainPage = new MultiFactorLogin();
                }
                else
                {
                    var graphResponse = App.DAUtil.GetMasterInfoByName(GRAPH_AUTH_RESULT_KEY);
                    var spResponse = App.DAUtil.GetMasterInfoByName(SHAREPOINT_AUTH_RESULT_KEY);
                    if (graphResponse == null || spResponse == null)
                    {
                        MainPage = new MultiFactorLogin();
                    }
                    else
                    {
                        OAuthHelper.GetLoggedInToken();

                        var uInfo = App.DAUtil.GetMasterInfoByName(USER_INFO_KEY);
                        if (uInfo != null)
                        {
                            var obj = JsonConvert.DeserializeObject<UserInfo>(uInfo.content, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
                            if (obj != null)
                            {
                                App.CurrentUser = new User
                                {
                                    Id = obj.UniqueId,
                                    Name = obj.GivenName + " " + obj.FamilyName,
                                    Email = obj.DisplayableId
                                };
                            }
                        }

                        MainPage = new StartPage();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private async void Attachments_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            try
            {
                var itemsToSync = e.NewItems;
                if (e.Action != System.Collections.Specialized.NotifyCollectionChangedAction.Add) return;

                foreach (Attachment item in itemsToSync)
                {
                    var attachemntResponse = await item.PostAttachment().ConfigureAwait(false);
                    if (!attachemntResponse.IsSuccessStatusCode)
                    {
                        string postError = await attachemntResponse.Content.ReadAsStringAsync();
                        string msg = $"Unble to post attachment - {item.FileName} {Environment.NewLine} {postError}";
                        ShowError(msg);
                    }
                    else
                    {
                        await Task.Delay(1000);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }

        }

        public void ShowError(string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                DependencyService.Get<IMessage>().ShortAlert(string.Format("Error: {0}", message));
            });
        }

        public async static Task SyncOfflineItemsBG()
        {
            try
            {
                var updatedCount = await OAuthHelper.SyncOfflineItems();
                if (updatedCount > 0)
                {
                    CrossLocalNotifications.Current.Show("", "Offline items has been synced");
                }

            }
            catch (Exception ex)
            {
            }
        }

        public async Task SyncLocalData()
        {
            try
            {
                await OAuthHelper.SyncOfflineItems();

                var users = await SPUtility.GetUsersForPicker();
                if (users != null)
                    App.peoplePickerDataSource = new List<PeoplePicker>(users);
            }
            catch (Exception ex)
            {
                ShowError(string.Format("Unable to sync local info: {0}", ex.Message));
            }
        }

        public void SetMainPageAsRootPage(object sender)
        {
            Device.BeginInvokeOnMainThread(async () => await SyncLocalData());
            MainPage = new StartPage();
        }

        public void SetMultiFactorAuthenticationPage(object sender)
        {
            MainPage = new MultiFactorLogin();
        }

        public static PeoplePicker validatePeoplePicker(string name)
        {
            var found = peoplePickerDataSource.Find((x) =>
            {
                return x.Name.ToLower() == name.ToLower();
            });

            return found;
        }

        public static DataAccess DAUtil
        {
            get
            {
                if (dbUtils == null)
                {
                    dbUtils = new DataAccess();
                }
                return dbUtils;
            }
        }
        protected async override void OnStart()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                //iOS stuff
                CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                CrossConnectivity.Current.ConnectivityTypeChanged += Current_ConnectivityTypeChanged;
            }
        }

        private void Current_ConnectivityTypeChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityTypeChangedEventArgs e)
        {
            if (e.IsConnected)
            {
                ConnectionChanged();
            }
            else
            {
                OAuthHelper.GetLoggedInToken();
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected async override void OnResume()
        {
            //try
            //{
            //    await OAuthHelper.GetAccessToken();
            //}
            //catch (Exception ex)
            //{
            //    ShowError(string.Format("Unable to refresh token: {0}", ex.Message));
            //}
        }

        private async void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            if (e.IsConnected)
            {
                await ConnectionChanged();
            }
            else
            {
                OAuthHelper.GetLoggedInToken();
            }
        }

        private async Task ConnectionChanged()
        {
            try
            {
                await OAuthHelper.SyncOfflineItems();
            }
            catch (Exception ex)
            {
                //ShowError(ex.Message);
            }

            //var userCredentials = App.DAUtil.GetMasterInfoByName("UserCredentials");
            //if (userCredentials != null)
            //{
            //    var cred = JsonConvert.DeserializeObject<dynamic>(userCredentials.content);
            //    string uName = cred.Username;
            //    string pwd = cred.Password;

            //    await OAuthHelper.GetAuthenticationHeader(uName, pwd).ContinueWith(async (x) =>
            //    {
            //        try
            //        {
            //            await OAuthHelper.SyncOfflineItems();
            //        }
            //        catch (Exception ex)
            //        {
            //            ShowError(ex.Message);
            //        }
            //    }).ConfigureAwait(false);
            //}
        }
    }
}
