using DevEnvAzure.Models;
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
    public partial class EditableDrafts : ContentPage
    {
        public EditableDrafts()
        {
            InitializeComponent();

            SafetyReportView.ItemsSource = App.safetyReport;
            //SafetyReportView.BindingContext = App.safetyReport;
            cabinSafetyView.ItemsSource = App.cabinSafety;
            cabinSafetyView.BindingContext = App.cabinSafety;
            //groundSafetyView.ItemsSource = App.groundSafety;
            //groundSafetyView.BindingContext = App.groundSafety;
            ////fatigueView.ItemsSource = App.fatigue;
            ////fatigueView.BindingContext = App.fatigue;
            ////injuryIllnessView.ItemsSource = App.injuryIllness;
            ////injuryIllnessView.BindingContext = App.injuryIllness;
            ////securityView.ItemsSource = App.security;
            ////securityView.BindingContext = App.security;

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
           // var item = (Button)sender;
           // SafetyReportModel listitem = (from itm in App.savedDrafts
           //                 where itm.SafetyID.ToString() == item.CommandParameter.ToString()
           //                 select itm).SingleOrDefault();
           //// List<Employee> x = new List<Employee>();
           // await Navigation.PushAsync(new SafetyReport(listitem));
        }

        private async void Details_OnClicked(object sender, EventArgs e)
        {
            //var item = (Button)sender;
            //SafetyReportModel listitem = (from itm in App.savedDrafts
            //                              where itm.SafetyID.ToString() == item.CommandParameter.ToString()
            //                              select itm).SingleOrDefault();
            //// List<Employee> x = new List<Employee>();
            //await Navigation.PushAsync(new SafetyReport(listitem));
        }
        
    }
}