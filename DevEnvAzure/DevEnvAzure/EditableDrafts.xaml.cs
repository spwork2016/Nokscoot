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
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            Load_saveddrafts();
        }

        public void Load_saveddrafts()
        {
            try
            {
                App.security = new ObservableCollection<SecurityModel>(App.DAUtil.GetAllEmployees<SecurityModel>("SecurityModel"));
                lblSecurityReportCount.Text = string.Format("({0})", App.security.Count.ToString());
                stkSecurityReports.IsVisible = App.security.Count == 0 ? false : true;

                App.safetyReport = new ObservableCollection<FlightSafetyReportModel>(App.DAUtil.GetAllEmployees<FlightSafetyReportModel>("SafetyReportModel"));
                lblSafetyReportCount.Text = string.Format("({0})", App.safetyReport.Count.ToString());
                stkSafetyReports.IsVisible = App.safetyReport.Count == 0 ? false : true;

                MessagingCenter.Subscribe<DraftsExpandContentView, string>(this, "", (sender1, arg) =>
                {
                    lblSafetyReportCount.Text = string.Format("({0})", App.safetyReport.Count.ToString());
                });


                App.cabinSafety = new ObservableCollection<CabibSafetyReport>(App.DAUtil.GetAllEmployees<CabibSafetyReport>("CabibSafetyReport"));
                lblcabinSafetyReportCount.Text = string.Format("({0})", App.cabinSafety.Count.ToString());
                stkcabinSafetyReports.IsVisible = App.cabinSafety.Count == 0 ? false : true;

                App.injuryIllness = new ObservableCollection<InjuryIllnessReport>(App.DAUtil.GetAllEmployees<InjuryIllnessReport>("InjuryIllnessReport"));
                lblinjuryIllnessReportCount.Text = string.Format("({0})", App.injuryIllness.Count.ToString());
                stkinjuryIllnessReports.IsVisible = App.injuryIllness.Count == 0 ? false : true;

                App.groundSafety = new ObservableCollection<GroundSafetyReport>(App.DAUtil.GetAllEmployees<GroundSafetyReport>("GroundSafetyReport"));
                lblGroundSafetyReportCount.Text = string.Format("({0})", App.groundSafety.Count.ToString());
                stkGroundSafetyReports.IsVisible = App.groundSafety.Count == 0 ? false : true;

                App.fatigue = new ObservableCollection<FatigueReport>(App.DAUtil.GetAllEmployees<FatigueReport>("FatigueReport"));
                lblFatigueReportCount.Text = string.Format("({0})", App.fatigue.Count.ToString());
                stkFatigueReports.IsVisible = App.fatigue.Count == 0 ? false : true;

                App.kaizen = new ObservableCollection<KaizenReportModel>(App.DAUtil.GetAllEmployees<KaizenReportModel>("KaizenReportModel"));
                kaizencnt.Text = string.Format("({0})", App.kaizen.Count.ToString());
                stkKaizen.IsVisible = App.kaizen.Count == 0 ? false : true;
            }
            catch (Exception ex)
            {

            }
        }

        private void ExpandCollapsePanel(object sender, StackLayout layout, string rptType)
        {
            var btn = (Button)sender;
            if (btn.Text == "+")
            {
                btn.Text = "-";
                removeChildren();
                layout.Children.Add(new DraftsExpandContentView(rptType));
            }
            else
            {
                btn.Text = "+";
                layout.Children.RemoveAt(0);
            }
        }

        private void Safety_Expand(object sender, EventArgs e)
        {
            ExpandCollapsePanel(sender, SafetyLayout, "safety");
        }

        private void GroundSafety_Expand(object sender, EventArgs e)
        {
            ExpandCollapsePanel(sender, groundSafetyLayout, "ground");
        }

        private void Security_Expand(object sender, EventArgs e)
        {
            ExpandCollapsePanel(sender, securityLayout, "security");
        }

        private void Fatigue_Expand(object sender, EventArgs e)
        {
            ExpandCollapsePanel(sender, fatigueLayout, "fatigue");
        }

        private void Cabin_Expand(object sender, EventArgs e)
        {
            ExpandCollapsePanel(sender, cabinSafetyLayout, "cabin");
        }

        private void Injuryillness_Expand(object sender, EventArgs e)
        {
            ExpandCollapsePanel(sender, injuryillnessLayout, "illness");
        }
        private void kaizen_Expand(object sender, EventArgs e)
        {
            ExpandCollapsePanel(sender, kaizenLayout, "kaizen");
        }
        private void fcVoyage_Expand(object sender, EventArgs e)
        {
            ExpandCollapsePanel(sender, fcVoyageLayout, "fcVoyage");
        }
        private void StatnInfo_Expand(object sender, EventArgs e)
        {
            ExpandCollapsePanel(sender, StatnInfoLayout, "stsnInfo");
        }
        public void removeChildren()
        {
            if (groundSafetyLayout.Children.Count > 0)
            {
                groundSafetyLayout.Children.RemoveAt(0);
                btnGroundSafetyPanel.Text = "+";
            }
            if (securityLayout.Children.Count > 0)
            {
                securityLayout.Children.RemoveAt(0);
                btnSecurityPanel.Text = "+";
            }
            if (fatigueLayout.Children.Count > 0)
            {
                fatigueLayout.Children.RemoveAt(0);
                btnFatiguePanel.Text = "+";
            }
            if (cabinSafetyLayout.Children.Count > 0)
            {
                cabinSafetyLayout.Children.RemoveAt(0);
                btnCabinPanel.Text = "+";
            }
            if (injuryillnessLayout.Children.Count > 0)
            {
                injuryillnessLayout.Children.RemoveAt(0);
                btnInjuryIllnessPanel.Text = "+";
            }
            if (SafetyLayout.Children.Count > 0)
            {
                SafetyLayout.Children.RemoveAt(0);
                btnSafetyPanel.Text = "+";
            }
        }


    }
}