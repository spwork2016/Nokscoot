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
    public partial class FatigueReportView : ContentView
    {
        public static string RatehowyoufeltValue;
        public FatigueReportView()
        {
            InitializeComponent();
        }
        private void Ratehowyoufelt_changed(object sender, EventArgs e)
        {
            RatehowyoufeltValue = Ratehowyoufeltpicker.Items.ElementAt(Ratehowyoufeltpicker.SelectedIndex);
        }
        
    }
}