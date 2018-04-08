using System;
using System.Collections.Generic;
using DevEnvAzure.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace DevEnvAzure
{
    public partial class MultiSelectMenuPage1 : PopupPage
    {
        public IList<string> selectedValues;
        public MultiSelectMenuPage1(string prevSelectedValues)
        {
            InitializeComponent();
            if (prevSelectedValues == null) return;

            selectedValues = prevSelectedValues.Split(',');

            foreach (var item in selectedValues)
            {
                if (item == autolbl.Text)
                    autolblws.IsToggled = true;
                else if (item == ilslbl.Text)
                    ilslblsw.IsToggled = true;
                else if (item == autolbl.Text)
                    autolblws.IsToggled = true;
                else if (precilbl.Text == item)
                    precisw.IsToggled = true;
                else if (vclbl.Text == item)
                    vclblsw.IsToggled = true;
                else if (rnplbl.Text == item)
                    rnplblsw.IsToggled = true;
                else if (rnpapvlbl.Text == item)
                    rnpapvlblsw.IsToggled = true;
            }
        }

        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }

        private async void OnSave(object sender, EventArgs e)
        {
            selectedValues = new List<string>();

            if (autolblws.IsToggled)
            {
                selectedValues.Add(autolbl.Text);
            }
            if (ilslblsw.IsToggled)
            {
                selectedValues.Add(ilslbl.Text);
            }
            if (autolblws.IsToggled)
            {
                selectedValues.Add(autolbl.Text);
            }
            if (precisw.IsToggled)
            {
                selectedValues.Add(precilbl.Text);
            }
            if (vclblsw.IsToggled)
            {
                selectedValues.Add(vclbl.Text);
            }
            if (rnplblsw.IsToggled)
            {
                selectedValues.Add(rnplbl.Text);
            }
            if (rnpapvlblsw.IsToggled)
            {
                selectedValues.Add(rnpapvlbl.Text);
            }

            MessagingCenter.Send<MultiSelectMenuPage1, string>(this, "SelectedItems", string.Join(",", selectedValues));
            await PopupNavigation.PopAsync();

        }
    }
}
