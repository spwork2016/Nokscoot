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
        public static ObservableCollection<DatatableData> fullDataTablecollection = new ObservableCollection<DatatableData>();
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

            if (CrossConnectivity.Current.IsConnected && App.AuthenticationResponse != null)
            {
                //Making sure we always refresh the token if conneted
                MainPage = new Login();
            }
            else if (!CrossConnectivity.Current.IsConnected)
            {
                var authResponse = App.DAUtil.GetMasterInfoByName("Authentication");
                if (authResponse != null)
                {
                    App.AuthenticationResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(authResponse.content);
                    MainPage = new DevEnvAzure.StartPage();
                }
            }
            else if (App.AuthenticationResponse == null)
                MainPage = new Login();

            MessagingCenter.Subscribe<object>(this, EVENT_LAUNCH_MAIN_PAGE, SetMainPageAsRootPage);
        }

        public void SetMainPageAsRootPage(object sender)
        {
            MainPage = new DevEnvAzure.StartPage();
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
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
