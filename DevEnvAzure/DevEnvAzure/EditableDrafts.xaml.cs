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
            SafetyReportView.ItemsSource = App.safetyReport;
            SafetyReportView.BindingContext = App.safetyReport;
            cabinSafetyListView.ItemsSource = App.cabinSafety;
            cabinSafetyListView.BindingContext = App.cabinSafety;
            groundSafetyView.ItemsSource = App.groundSafety;
            groundSafetyView.BindingContext = App.groundSafety;
            fatigueView.ItemsSource = App.fatigue;
            fatigueView.BindingContext = App.fatigue;
            injuryIllnessView.ItemsSource = App.injuryIllness;
            injuryIllnessView.BindingContext = App.injuryIllness;
            securityListView.ItemsSource = App.security;
            securityListView.BindingContext = App.security;

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
            }
            catch (Exception ex)
            {

            }
        }
        private async void security_Clicked(object sender, EventArgs e)
        {
            try
            {
                var item = (Button)sender;
                SecurityModel listitem = (from itm in App.security
                                          where itm.ReportType.ToString() == item.CommandParameter.ToString()
                                          select itm).FirstOrDefault();
                await Navigation.PushAsync(new SSIRShortForm(listitem, "security"));
            }
            catch(Exception ex)
            {

            }
        }

        private async void cabinSafety_Clicked(object sender, EventArgs e)
        {
            var item = (Button)sender;
            CabibSafetyReport listitem = (from itm in App.cabinSafety
                                          where itm.ReportType.ToString() == item.CommandParameter.ToString()
                                          select itm).FirstOrDefault();
            await Navigation.PushAsync(new SSIRShortForm(listitem, "cabin"));
        }

        private async void SafetyReport_Clicked(object sender, EventArgs e)
        {
            var item = (Button)sender;
            FlightSafetyReportModel listitem = (from itm in App.safetyReport
                                          where itm.ReportType.ToString() == item.CommandParameter.ToString()
                                          select itm).FirstOrDefault();
            await Navigation.PushAsync(new SSIRShortForm(listitem, "safety"));
        }

        private async void groundSafety_Clicked(object sender, EventArgs e)
        {
            var item = (Button)sender;
            GroundSafetyReport listitem = (from itm in App.groundSafety
                                          where itm.ReportType.ToString() == item.CommandParameter.ToString()
                                          select itm).FirstOrDefault();
            await Navigation.PushAsync(new SSIRShortForm(listitem, "ground"));
        }

        private async void fatigue_Clicked(object sender, EventArgs e)
        {
            var item = (Button)sender;
            FatigueReport listitem = (from itm in App.fatigue
                                          where itm.ReportType.ToString() == item.CommandParameter.ToString()
                                          select itm).FirstOrDefault();
            await Navigation.PushAsync(new SSIRShortForm(listitem, "fatigue"));
        }
        private async void injuryIllness_Clicked(object sender, EventArgs e)
        {
            var item = (Button)sender;
            InjuryIllnessReport listitem = (from itm in App.injuryIllness
                                      where itm.ReportType.ToString() == item.CommandParameter.ToString()
                                      select itm).FirstOrDefault();
            await Navigation.PushAsync(new SSIRShortForm(listitem, "Injury"));
        }


    }
}