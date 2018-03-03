using DevEnvAzure.Model;
using DevEnvAzure.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
        public static ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
        public static ObservableCollection<object> savedDrafts = new ObservableCollection<object>();

        public static ObservableCollection<CabibSafetyReport> cabinSafety = new ObservableCollection<CabibSafetyReport>();
        public static ObservableCollection<FatigueReport> fatigue = new ObservableCollection<FatigueReport>();
        public static ObservableCollection<GroundSafetyReport> groundSafety = new ObservableCollection<GroundSafetyReport>();
        public static ObservableCollection<InjuryIllnessReport> injuryIllness = new ObservableCollection<InjuryIllnessReport>();
        public static ObservableCollection<SecurityModel> security = new ObservableCollection<SecurityModel>();
        public static ObservableCollection<SafetyReportModel> safetyReport = new ObservableCollection<SafetyReportModel>();

        public App()
        {
            InitializeComponent();
            if (App.AuthenticationResponse == null)
                MainPage = new Login();
            else
                MainPage = new DevEnvAzure.StartPage();
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
