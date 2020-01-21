using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReasonforDeviationMultiSelect : PopupPage
    {
        public IList<string> selectedValues;
        public ReasonforDeviationMultiSelect(string prevSelectedValues)
        {
            InitializeComponent();

            if (prevSelectedValues == null) return;

            selectedValues = prevSelectedValues.Split(',');
            foreach (var item in selectedValues)
            {
                if (item == lateclearancelbl.Text)
                {
                    lateclearsw.IsToggled = true;
                }
                if (item == turblbl.Text)
                {
                    turbsw.IsToggled = true;
                }
                if (tcaslbl.Text == item)
                {
                    tcassw.IsToggled = true;
                }
                if (item == automallbl.Text)
                {
                    automalsw.IsToggled = true;
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

            if (lateclearsw.IsToggled)
            {
                selectedValues.Add(lateclearancelbl.Text);
            }
            if (turbsw.IsToggled)
            {
                selectedValues.Add(turblbl.Text);
            }
            if (tcassw.IsToggled)
            {
                selectedValues.Add(tcaslbl.Text);
            }
            if (automalsw.IsToggled)
            {
                selectedValues.Add(automallbl.Text);
            }

            MessagingCenter.Send<ReasonforDeviationMultiSelect, string>(this, "SelectedItems", string.Join(",", selectedValues));
            await PopupNavigation.PopAsync();

        }
    }
}