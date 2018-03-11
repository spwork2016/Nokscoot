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
            turbulenceValue = turbulancepicker.Items.ElementAt(turbulancepicker.SelectedIndex);
        }
        private void dangergoods_Changed(object sender, EventArgs e)
        {
            dangergoodsValue = paxpicker.Items.ElementAt(paxpicker.SelectedIndex);
        }
        private void dangerwhen_Changed(object sender, EventArgs e)
        {
            dangerWhenValue = identifiedpicker.Items.ElementAt(identifiedpicker.SelectedIndex);
        }
        private void dangerwhere_Changed(object sender, EventArgs e)
        {
            dangerWhereValue = identifiedwherepicker.Items.ElementAt(identifiedwherepicker.SelectedIndex);
        }
    }
}