using DevEnvAzure.Model;
using DevEnvAzure.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static ObservableCollection<Attachment> attachments = new ObservableCollection<Attachment>();
        public static string EVENT_LAUNCH_MAIN_PAGE = "EVENT_LAUNCH_MAIN_PAGE";
        public App()
        {
            InitializeComponent();
            attachments.CollectionChanged += Attachments_CollectionChanged;

            var eValue = DAUtil.GetAll<OfflineItem>("OfflineItem");
            if (eValue != null && eValue.Count > 0)
                App.offlineItems = new ObservableCollection<OfflineItem>(eValue);

            try
            {
                if (SPUtility.IsConnected())
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
                            await OAuthHelper.GetUserInfo(uName, pwd).ContinueWith((y) =>
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    DependencyService.Get<IMessage>().ShortAlert("user info");
                                });

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

                            await SyncLocalData();
                        }).ConfigureAwait(false);
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
                        var uInfo = App.DAUtil.GetMasterInfoByName("UserInfo");
                        if (uInfo != null)
                        {
                            var obj = JsonConvert.DeserializeObject<SPData>(uInfo.content, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
                            if (obj != null)
                            {
                                App.CurrentUser = new User
                                {
                                    Id = obj.d.Id,
                                    Name = obj.d.Title,
                                    Email = obj.d.Email,
                                    //PictureBytes = GetPicture(username, password).Result
                                };
                            }
                        }

                        MainPage = new StartPage();
                    }
                }

                MessagingCenter.Subscribe<object>(this, EVENT_LAUNCH_MAIN_PAGE, SetMainPageAsRootPage);
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
                ShowError(string.Format("Unable to sync local info: {0}", ex.Message));
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
                ShowError(string.Format("Unable to refresh token: {0}", ex.Message));
            }
        }

        private async void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            if (e.IsConnected)
            {
                ConnectionChanged();
            }
        }

        private async void ConnectionChanged()
        {
            var userCredentials = App.DAUtil.GetMasterInfoByName("UserCredentials");
            if (userCredentials != null)
            {
                var cred = JsonConvert.DeserializeObject<dynamic>(userCredentials.content);
                string uName = cred.Username;
                string pwd = cred.Password;

                await OAuthHelper.GetAuthenticationHeader(uName, pwd).ContinueWith(async (x) =>
                {
                    try
                    {
                        await OAuthHelper.SyncOfflineItems();
                    }
                    catch (Exception ex)
                    {
                        ShowError(ex.Message);
                    }
                }).ConfigureAwait(false);
            }
        }
    }
}
