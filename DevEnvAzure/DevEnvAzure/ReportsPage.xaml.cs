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
            await Navigation.PushAsync(new SSIRShortForm(new Models.SafetyReportModel(), "safety"));
        }

        private async void Security_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SSIRShortForm(new Models.SecurityModel(), "security"));
        }
        private async void CabinSafety_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SSIRShortForm(new Models.CabibSafetyReport(), "cabin"));
        }
        private async void Fatigue_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SSIRShortForm(new Models.FatigueReport(), "fatigue"));
        }
        private async void GroundSafety_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SSIRShortForm(new Models.GroundSafetyReport(), "ground"));
        }

        private async void InjuryIllness_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SSIRShortForm(new Models.InjuryIllnessReport(), "Injury"));
        }
    }
}