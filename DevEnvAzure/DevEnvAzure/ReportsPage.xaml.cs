using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportsPage : ContentPage
    {
        public ReportsPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var sft = new Models.FlightSafetyReportModel();
            sft.DateOfEvent = DateTime.Now;
            //sft.AircraftRegis = null;
            await Navigation.PushAsync(new SSIRShortForm(sft, "safety"));
        }

        private async void Security_Clicked(object sender, EventArgs e)
        {
            var scrt = new Models.SecurityModel();
            scrt.DateOfEvent = DateTime.Now;
            //scrt.AircraftRegis = null;
            //scrt.onGround = null;
            //scrt.flightEvent = null;
            await Navigation.PushAsync(new SSIRShortForm(scrt, "security"));
        }
        private async void CabinSafety_Clicked(object sender, EventArgs e)
        {
            var csft = new Models.CabibSafetyReport();
           csft.DateOfEvent = DateTime.Now;
            //csft.AircraftRegis = null;
            await Navigation.PushAsync(new SSIRShortForm(csft, "cabin"));
        }
        private async void Fatigue_Clicked(object sender, EventArgs e)
        {
            var cbn = new Models.FatigueReport();
            cbn.DateOfEvent = DateTime.Now;
            cbn.localReportDate = DateTime.Now;
            //cbn.AircraftRegis = null;
            await Navigation.PushAsync(new SSIRShortForm(cbn, "fatigue"));
        }
        private async void GroundSafety_Clicked(object sender, EventArgs e)
        {
            var grnd = new Models.GroundSafetyReport();
            grnd.DateOfEvent = DateTime.Now;
            //grnd.AircraftRegis = null;
            await Navigation.PushAsync(new SSIRShortForm(grnd, "ground"));
        }

        private async void InjuryIllness_Clicked(object sender, EventArgs e)
        {
            var injr = new Models.InjuryIllnessReport();
            injr.DateOfEvent = DateTime.Now;
            //injr.AircraftRegis = null;
            await Navigation.PushAsync(new SSIRShortForm(injr, "Injury"));
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            var fcvoyage = new Models.FlightCrewVoyageRecordModel();
            //fcvoyage.SectorNumber = null;
            //fcvoyage.AircraftRegistration = null;
            //fcvoyage.ManualorAuto = null;
            //fcvoyage.ReportCategories = null;
            //fcvoyage.ReplyRequired = null;
            //fcvoyage.Rank = null;
            await Navigation.PushAsync(new FlightCrewVoyageRecord(fcvoyage, "fcVoyage"));
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            var kaizen = new Models.KaizenReportModel();
           // kaizen.BenefitsCategory = null;
            

             await Navigation.PushAsync(new KaizenReport(kaizen, "kaizen"));
        }
    }
}