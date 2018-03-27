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
    public partial class IntruderRelativePositionMultiSelect : PopupPage
    {
        public static string relativePosition;
        public IntruderRelativePositionMultiSelect()
        {
            InitializeComponent();
        }
        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }
        private async void OnSave(object sender, EventArgs e)
        {
            relativePosition = "";
            if (levelsw.IsToggled)
            {
                relativePosition += levellbl.Text + ",";
            }
            if (climbingsw.IsToggled)
            {
                relativePosition += climbinglbl.Text + ",";
            }
            if (Descendingsw.IsToggled)
            {
                relativePosition += Descendinglbl.Text + ",";
            }
            if (abovesw.IsToggled)
            {
                relativePosition += abovelbl.Text + ",";
            }
            if (Belowsw.IsToggled)
            {
                relativePosition += Belowlbl.Text + ",";
            }
            if (coaltisw.IsToggled)
            {
                relativePosition += coaltilbl.Text + ",";
            }

            MessagingCenter.Send<IntruderRelativePositionMultiSelect, string>(this, "Hi", relativePosition);
            await PopupNavigation.PopAsync();

        }
    }
}