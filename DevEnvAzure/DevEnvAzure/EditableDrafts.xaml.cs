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
            Load_saveddrafts();
            //SafetyReportView.ItemsSource = App.safetyReport;
            //SafetyReportView.BindingContext = App.safetyReport;
            //cabinSafetyListView.ItemsSource = App.cabinSafety;
            //  cabinSafetyListView.BindingContext = App.cabinSafety;
            // groundSafetyView.ItemsSource = App.groundSafety;
            // groundSafetyView.BindingContext = App.groundSafety;
            //  fatigueView.ItemsSource = App.fatigue;
            //  fatigueView.BindingContext = App.fatigue;
            //   injuryIllnessView.ItemsSource = App.injuryIllness;
            //   injuryIllnessView.BindingContext = App.injuryIllness;
            //   securityListView.ItemsSource = App.security;
            //   securityListView.BindingContext = App.security;

            BindingContext = this;

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
        }


    }
}