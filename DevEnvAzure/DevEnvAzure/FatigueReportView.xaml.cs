using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FatigueReportView : ContentView
    {
        public static string RatehowyoufeltValue;
        //  Models.FatigueReport _FatigueReport;
        public FatigueReportView()
        {
            InitializeComponent();
            //  _FatigueReport = new Models.FatigueReport();
            //  _FatigueReport.localReportDate = DateTime.Now;
        }
        private void Ratehowyoufelt_changed(object sender, EventArgs e)
        {
            if (Ratehowyoufeltpicker.SelectedIndex > 0)
                RatehowyoufeltValue = Ratehowyoufeltpicker.Items.ElementAt(Ratehowyoufeltpicker.SelectedIndex);
        }

    }
}