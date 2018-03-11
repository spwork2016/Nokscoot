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
    public partial class securityReportView : ContentView
    {
        public static string flightphase;
        public static string flightwheresel;
        public securityReportView()
        {
            InitializeComponent();
        }
        private void flightphase_selected(object sender, EventArgs e)
        {
            if (FlightPhasepicker.SelectedIndex > 0)
                flightphase = FlightPhasepicker.Items.ElementAt(FlightPhasepicker.SelectedIndex);
        }
        private void flightwhere_selected(object sender, EventArgs e)
        {
            if (flightwherepicker.SelectedIndex > 0)
                flightwheresel = flightwherepicker.Items.ElementAt(flightwherepicker.SelectedIndex);
        }
    }
}