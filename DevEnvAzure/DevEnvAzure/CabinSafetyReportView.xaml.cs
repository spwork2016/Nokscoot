using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CabinSafetyReportView : ContentView
    {
        public static string turbulenceValue;
        public static string dangergoodsValue;
        public static string dangerWhenValue;
        public static string dangerWhereValue;

        public CabinSafetyReportView()
        {
            InitializeComponent();
        }
        private void turbulence_Changed(object sender, EventArgs e)
        {
            if (turbulancepicker.SelectedIndex > 0)
                turbulenceValue = turbulancepicker.Items.ElementAt(turbulancepicker.SelectedIndex);
        }
        private void dangergoods_Changed(object sender, EventArgs e)
        {
            if (paxpicker.SelectedIndex > 0)
                dangergoodsValue = paxpicker.Items.ElementAt(paxpicker.SelectedIndex);
        }
        private void dangerwhen_Changed(object sender, EventArgs e)
        {
            if (identifiedpicker.SelectedIndex > 0)
                dangerWhenValue = identifiedpicker.Items.ElementAt(identifiedpicker.SelectedIndex);
        }
        private void dangerwhere_Changed(object sender, EventArgs e)
        {
            if (identifiedwherepicker.SelectedIndex > 0)
                dangerWhereValue = identifiedwherepicker.Items.ElementAt(identifiedwherepicker.SelectedIndex);
        }
    }
}