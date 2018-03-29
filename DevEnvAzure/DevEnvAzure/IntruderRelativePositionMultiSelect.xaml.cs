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
        public IList<string> selectedValues;
        public IntruderRelativePositionMultiSelect(string prevSelectedValues)
        {
            InitializeComponent();

            if (prevSelectedValues == null) return;

            selectedValues = prevSelectedValues.Split(',');

            foreach (var item in selectedValues)
            {
                if (item == levellbl.Text)
                {
                    levelsw.IsToggled = true;
                }
                if (climbinglbl.Text == item)
                {
                    climbingsw.IsToggled = true;
                }
                if (Descendinglbl.Text == item)
                {
                    Descendingsw.IsToggled = true;
                }
                if (abovelbl.Text == item)
                {
                    abovesw.IsToggled = true;
                }
                if (item == Belowlbl.Text)
                {
                    Belowsw.IsToggled = true;
                }
                if (coaltilbl.Text == item)
                {
                    coaltisw.IsToggled = true;
                }
            }
        }
        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }
        private async void OnSave(object sender, EventArgs e)
        {
            selectedValues = new List<string>();
            if (levelsw.IsToggled)
            {
                selectedValues.Add(levellbl.Text);
            }
            if (climbingsw.IsToggled)
            {
                selectedValues.Add(climbinglbl.Text);
            }
            if (Descendingsw.IsToggled)
            {
                selectedValues.Add(Descendinglbl.Text);
            }
            if (abovesw.IsToggled)
            {
                selectedValues.Add(abovelbl.Text);
            }
            if (Belowsw.IsToggled)
            {
                selectedValues.Add(Belowlbl.Text);
            }
            if (coaltisw.IsToggled)
            {
                selectedValues.Add(coaltilbl.Text);
            }

            MessagingCenter.Send<IntruderRelativePositionMultiSelect, string>(this, "Hi", string.Join(",", selectedValues));
            await PopupNavigation.PopAsync();
        }
    }
}