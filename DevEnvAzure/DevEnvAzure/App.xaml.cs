using DevEnvAzure.Model;
using DevEnvAzure.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.LocalNotifications;

namespace DevEnvAzure
{
    public partial class App : Application
    {
        public static string ApplicationID = "d4c9dc64-803f-4dce-842c-380ce91f60d4";
        public static string tenanturl = "https://login.windows.net/common";// "https://login.microsoftonline.com/542381e6-b9d2-4fe3-a20b-e575f656c08c";
        public static string ReturnUri = "http://DevEnvAzure.microsoft.net";
        public static string GraphResourceUri = "https://nok365.sharepoint.com"; // "https://graph.microsoft.com";
        public static AuthenticationResponse AuthenticationResponse = null;
        public static User CurrentUser = null;
        static DataAccess dbUtils;
        public static AuthenticationContext authcontext = null;
        public static List<PeoplePicker> peoplePickerDataSource;
        public static ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
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

        public static string EVENT_LAUNCH_MAIN_PAGE = "EVENT_LAUNCH_MAIN_PAGE";
        public App()
        {
            InitializeComponent();
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    var userCredentials = App.DAUtil.GetMasterInfoByName("UserCredentials");
                    if (userCredentials != null)
                    {
                        var cred = JsonConvert.DeserializeObject<dynamic>(userCredentials.content);
                        string uName = cred.Username;
                        string pwd = cred.Password;
                        MainPage = new StartPage();
                        OAuthHelper.GetAuthenticationHeader(uName, pwd).ContinueWith(async (x) =>
                       {
                           await SyncLocalData();
                           await OAuthHelper.GetUserInfo(uName, pwd).ContinueWith((y) =>
                           {
                               Device.BeginInvokeOnMainThread(async () =>
                               {
                                   MessagingCenter.Send<App>(this, "userInfo");
                                   if (App.AuthenticationResponse == null)
                                   {
                                       MainPage = new Login();
                                       DependencyService.Get<IMessage>().ShortAlert("Login failed! Please check email/password");
                                   }
                               });
                           });
                       });
                    }
                    else
                    {
                        MainPage = new Login();
                    }
                }
                else
                {
                    var authResponse = App.DAUtil.GetMasterInfoByName("Authentication");
                    if (authResponse == null)
                        MainPage = new Login();
                    else
                    {
                        App.AuthenticationResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(authResponse.content);
                        MainPage = new StartPage();
                    }
                }

                MessagingCenter.Subscribe<object>(this, EVENT_LAUNCH_MAIN_PAGE, SetMainPageAsRootPage);
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert(string.Format("Error: {0}", ex.Message));
            }
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

        private async Task SyncLocalData()
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
                DependencyService.Get<IMessage>().ShortAlert(string.Format("Unable to sync local info: {0}", ex.Message));
            }
        }

        public void SetMainPageAsRootPage(object sender)
        {
            MainPage = new StartPage();
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
                Plugin.Connectivity.CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
            }
            // Handle when your app starts
            // Plugin.Connectivity.CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected async override void OnResume()
        {
            try
            {
                await OAuthHelper.RefreshAccessToken();
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert(string.Format("Unable to refresh token: {0}", ex.Message));
            }
        }

        private async void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            if (e.IsConnected)
            {
                await OAuthHelper.SyncOfflineItems();
            }
        }
    }
}
