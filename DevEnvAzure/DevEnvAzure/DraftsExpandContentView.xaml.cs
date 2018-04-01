using DevEnvAzure.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DraftsExpandContentView : ContentView
    {
        public string _reportType;
        public DraftsExpandContentView(string reportType)
        {
            InitializeComponent();
            _reportType = reportType;
            //  App.safetyReport = new ObservableCollection<FlightSafetyReportModel>(App.DAUtil.GetAllEmployees<FlightSafetyReportModel>("SafetyReportModel"));
            DataBind(reportType);

        }

        private void DataBind(string reportType)
        {
            switch (reportType)
            {
                case "safety":
                    ItemsListView.ItemsSource = App.safetyReport;
                    ItemsListView.BindingContext = App.safetyReport;
                    ItemsListView.HeightRequest = App.safetyReport.Count * 45;
                    break;
                case "ground":
                    ItemsListView.ItemsSource = App.groundSafety;
                    ItemsListView.BindingContext = App.groundSafety;
                    ItemsListView.HeightRequest = App.groundSafety.Count * 45;
                    break;

                case "cabin":
                    ItemsListView.ItemsSource = App.cabinSafety;
                    ItemsListView.BindingContext = App.cabinSafety;
                    ItemsListView.HeightRequest = App.cabinSafety.Count * 45;
                    break;

                case "security":
                    ItemsListView.ItemsSource = App.security;
                    ItemsListView.BindingContext = App.security;
                    ItemsListView.HeightRequest = App.security.Count * 45;
                    break;

                case "illness":
                    ItemsListView.ItemsSource = App.injuryIllness;
                    ItemsListView.BindingContext = App.injuryIllness;
                    ItemsListView.HeightRequest = App.injuryIllness.Count * 45;
                    break;
                case "fatigue":
                    ItemsListView.ItemsSource = App.fatigue;
                    ItemsListView.BindingContext = App.fatigue;
                    ItemsListView.HeightRequest = App.fatigue.Count * 45;
                    break;
            }
        }

        private async void SafetyReport_Clicked(object sender, EventArgs e)
        {
            var item = (Button)sender;
            switch (_reportType)
            {
                case "safety":
                    FlightSafetyReportModel listitem = (from itm in App.safetyReport
                                                        where itm.ReportType.ToString() == item.CommandParameter.ToString()
                                                        select itm).FirstOrDefault();
                    await Navigation.PushAsync(new SSIRShortForm(listitem, "safety"));
                    break;
                case "ground":
                    GroundSafetyReport listitem1 = (from itm in App.groundSafety
                                                    where itm.ReportType.ToString() == item.CommandParameter.ToString()
                                                    select itm).FirstOrDefault();
                    await Navigation.PushAsync(new SSIRShortForm(listitem1, "ground"));
                    break;
                case "cabin":

                    CabibSafetyReport listitem2 = (from itm in App.cabinSafety
                                                   where itm.ReportType.ToString() == item.CommandParameter.ToString()
                                                   select itm).FirstOrDefault();
                    await Navigation.PushAsync(new SSIRShortForm(listitem2, "cabin"));
                    break;
                case "security":
                    try
                    {

                        SecurityModel listitem3 = (from itm in App.security
                                                   where itm.ReportType.ToString() == item.CommandParameter.ToString()
                                                   select itm).FirstOrDefault();
                        await Navigation.PushAsync(new SSIRShortForm(listitem3, "security"));
                    }
                    catch (Exception ex)
                    {

                    }
                    break;
                case "illness":
                    InjuryIllnessReport listitem4 = (from itm in App.injuryIllness
                                                     where itm.ReportType.ToString() == item.CommandParameter.ToString()
                                                     select itm).FirstOrDefault();
                    await Navigation.PushAsync(new SSIRShortForm(listitem4, "Injury"));
                    break;
                case "fatigue":
                    FatigueReport listitem5 = (from itm in App.fatigue
                                               where itm.ReportType.ToString() == item.CommandParameter.ToString()
                                               select itm).FirstOrDefault();
                    await Navigation.PushAsync(new SSIRShortForm(listitem5, "fatigue"));
                    break;
            }

        }

        private void MenuItem_Delete_Clicked(object sender, EventArgs e)
        {
            var cp = ((MenuItem)sender).CommandParameter;
            if (cp == null) return;

            //TODO - delete appropriate report
            App.DAUtil.DeleteEmployee(cp);
            App.security = new ObservableCollection<SecurityModel>(App.DAUtil.GetAllEmployees<SecurityModel>("SecurityModel"));
            DataBind(_reportType);
            DependencyService.Get<IMessage>().LongAlert("Draft deleted");
        }
    }
}