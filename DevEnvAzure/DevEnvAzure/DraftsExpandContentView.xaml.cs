using DevEnvAzure.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
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
            switch (reportType)
            {
                case "safety":
                    SafetyReportView.ItemsSource = App.safetyReport;
                    SafetyReportView.BindingContext = App.safetyReport;
                    break;
                case "ground":
                    SafetyReportView.ItemsSource = App.groundSafety;
                    SafetyReportView.BindingContext = App.groundSafety;
                    break;

                case "cabin":
                    SafetyReportView.ItemsSource = App.cabinSafety;
                    SafetyReportView.BindingContext = App.cabinSafety;
                    break;

                case "security":
                    SafetyReportView.ItemsSource = App.security;
                    SafetyReportView.BindingContext = App.security;
                    break;

                case "illness":
                    SafetyReportView.ItemsSource = App.injuryIllness;
                    SafetyReportView.BindingContext = App.injuryIllness;
                    break;
                case "fatigue":
                    SafetyReportView.ItemsSource = App.fatigue;
                    SafetyReportView.BindingContext = App.fatigue;
                    break;
                    //Other 3 views
                case "kaizen":
                    SafetyReportView.ItemsSource = App.kaizen;
                    SafetyReportView.BindingContext = App.kaizen;
                    break;
                case "statInfo":
                    SafetyReportView.ItemsSource = App.statInfo;
                    SafetyReportView.BindingContext = App.statInfo;
                    break;
                case "fcVoyage":
                    SafetyReportView.ItemsSource = App.fcVoyage;
                    SafetyReportView.BindingContext = App.fcVoyage;
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

                case "kaizen":

                    KaizenReportModel listitem6 = (from itm in App.kaizen
                                                   where itm.After.ToString() == item.CommandParameter.ToString()
                                               select itm).FirstOrDefault();
                    await Navigation.PushAsync(new KaizenReport());
                    break;
                case "fcVoyage":

                    FlightCrewVoyageRecordModel listitem7 = (from itm in App.fcVoyage
                                                             where itm.VoyageRecord.ToString() == item.CommandParameter.ToString()
                                               select itm).FirstOrDefault();
                    await Navigation.PushAsync(new FlightCrewVoyageRecord());
                    break;
                case "statInfo":

                    StationInformationModel listitem8 = (from itm in App.statInfo
                                               where itm.IATACode.ToString() == item.CommandParameter.ToString()
                                               select itm).FirstOrDefault();
                    await Navigation.PushAsync(new StationInformation());
                    break;

            }

        }

    }
}