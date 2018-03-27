using DevEnvAzure.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditableDrafts : ContentPage
    {
        public EditableDrafts()
        {
            InitializeComponent();
            load_saveddrafts();
        }
        public void load_saveddrafts()
        {
            try
            {
                App.security = new ObservableCollection<SecurityModel>(App.DAUtil.GetAllEmployees<SecurityModel>("SecurityModel"));
                App.safetyReport = new ObservableCollection<FlightSafetyReportModel>(App.DAUtil.GetAllEmployees<FlightSafetyReportModel>("SafetyReportModel"));
                App.cabinSafety = new ObservableCollection<CabibSafetyReport>(App.DAUtil.GetAllEmployees<CabibSafetyReport>("CabibSafetyReport"));
                App.injuryIllness = new ObservableCollection<InjuryIllnessReport>(App.DAUtil.GetAllEmployees<InjuryIllnessReport>("InjuryIllnessReport"));
                App.groundSafety = new ObservableCollection<GroundSafetyReport>(App.DAUtil.GetAllEmployees<GroundSafetyReport>("GroundSafetyReport"));
                App.fatigue = new ObservableCollection<FatigueReport>(App.DAUtil.GetAllEmployees<FatigueReport>("FatigueReport"));
                App.kaizen = new ObservableCollection<KaizenReportModel>(App.DAUtil.GetAllEmployees<KaizenReportModel>("KaizenReportModel"));
                App.statInfo = new ObservableCollection<StationInformationModel>(App.DAUtil.GetAllEmployees<StationInformationModel>("StationInformationModel"));
                App.fcVoyage = new ObservableCollection<FlightCrewVoyageRecordModel>(App.DAUtil.GetAllEmployees<FlightCrewVoyageRecordModel>("FlightCrewVoyageRecordModel"));
            }
            catch (Exception ex)
            {

            }
        }
        private  void Safety_Expand(object sender, EventArgs e)
        {
            removeChildren();
            if (SafetyLayout.Children.Count<1)
            SafetyLayout.Children.Add(new DraftsExpandContentView("safety"));
        }
        private void groundSafety_Expand(object sender, EventArgs e)
        {
            removeChildren();


             if (groundSafetyLayout.Children.Count<1)
            groundSafetyLayout.Children.Add(new DraftsExpandContentView("ground"));

        }
        private void security_Expand(object sender, EventArgs e)
        {
            removeChildren();


            if (securityLayout.Children.Count < 1)
                securityLayout.Children.Add(new DraftsExpandContentView("security"));

        }
        private void fatigue_Expand(object sender, EventArgs e)
        {
            removeChildren();


            if (fatigueLayout.Children.Count < 1)
                fatigueLayout.Children.Add(new DraftsExpandContentView("fatigue"));

        }
        private void cabin_Expand(object sender, EventArgs e)
        {
            removeChildren();


            if (cabinSafetyLayout.Children.Count < 1)
                cabinSafetyLayout.Children.Add(new DraftsExpandContentView("cabin"));

        }
        private void injuryillness_Expand(object sender, EventArgs e)
        {
            removeChildren();


            if (injuryillnessLayout.Children.Count < 1)
                injuryillnessLayout.Children.Add(new DraftsExpandContentView("illness"));

        }
        //Remaining 3 forms
        private void KaizenReports_Expand(object sender, EventArgs e)
        {
            removeChildren();


            if (KaizenReportLayout.Children.Count < 1)
                KaizenReportLayout.Children.Add(new DraftsExpandContentView("kaizen"));

        }
        private void StationInformation_Expand(object sender, EventArgs e)
        {
            removeChildren();


            if (StationInformationLayout.Children.Count < 1)
                StationInformationLayout.Children.Add(new DraftsExpandContentView("statInfo"));

        }
        private void FlightCrewVoyageRecord_Expand(object sender, EventArgs e)
        {
            removeChildren();


            if (FlightCrewVoyageRecordLayout.Children.Count < 1)
                FlightCrewVoyageRecordLayout.Children.Add(new DraftsExpandContentView("fcVoyage"));

        }
        public void removeChildren()
        {
            if (groundSafetyLayout.Children.Count > 0)
            {
                groundSafetyLayout.Children.RemoveAt(0);
            }
            if (securityLayout.Children.Count > 0)
            {
                securityLayout.Children.RemoveAt(0);
            }
            if (fatigueLayout.Children.Count > 0)
            {
                fatigueLayout.Children.RemoveAt(0);
            }
            if (cabinSafetyLayout.Children.Count > 0)
            {
                cabinSafetyLayout.Children.RemoveAt(0);
            }
            if (injuryillnessLayout.Children.Count > 0)
            {
                injuryillnessLayout.Children.RemoveAt(0);
            }
            if (SafetyLayout.Children.Count > 0)
            {
                SafetyLayout.Children.RemoveAt(0);
            }
            //Remaining 3 forms
            if (KaizenReportLayout.Children.Count > 0)
            {
                KaizenReportLayout.Children.RemoveAt(0);
            }
            if (FlightCrewVoyageRecordLayout.Children.Count > 0)
            {
                FlightCrewVoyageRecordLayout.Children.RemoveAt(0);
            }
            if (StationInformationLayout.Children.Count > 0)
            {
                StationInformationLayout.Children.RemoveAt(0);
            }
        }


    }
}