using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
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
    public partial class ReasonforDeviationMultiSelect : PopupPage
    {
        public static string reasonDeviation;
        public ReasonforDeviationMultiSelect()
        {
            InitializeComponent();
        }
        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }
        private async void OnSave(object sender, EventArgs e)
        {
            reasonDeviation = "";
            if (lateclearsw.IsToggled)
            {
                reasonDeviation += lateclearancelbl.Text + ",";
            }
            if (turbsw.IsToggled)
            {
                reasonDeviation += turblbl.Text + ",";
            }
            if (tcassw.IsToggled)
            {
                reasonDeviation += tcaslbl.Text + ",";
            }
            if (tcassw.IsToggled)
            {
                reasonDeviation += tcaslbl.Text + ",";
            }
            if (automalsw.IsToggled)
            {
                reasonDeviation += automallbl.Text + ",";
            }

            MessagingCenter.Send<ReasonforDeviationMultiSelect, string>(this, "Hi", reasonDeviation);
            await PopupNavigation.PopAsync();

        }
    }
}