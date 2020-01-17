using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DevEnvAzure
{
    public partial class SecurityEventType : PopupPage
    {
        public IList<string> selectedValues;
        public SecurityEventType(string prevSelectedValues)
        {
            InitializeComponent();
            if (prevSelectedValues == null) return;

            selectedValues = prevSelectedValues.Split(',');

            foreach (var item in selectedValues)
            {
                if (item == lblAssault.Text)
                {
                    assaultSwt.IsToggled = true;
                }
                if (lblDamag.Text == item)
                {
                    damageSwt.IsToggled = true;
                }
                if (lblDisp.Text == item)
                {
                    dispSwt.IsToggled = true;
                }
                if (lblDisp.Text == item)
                {
                    fraudSwt.IsToggled = true;
                }
                if (lblInaComment.Text == item)
                {
                    inaCommentSwt.IsToggled = true;
                }
                if (lblIncBrdPass.Text == item)
                {
                    SwtIncBrdPass.IsToggled = true;
                }
                if (lblPassFraud.Text == item)
                {
                    SwtPassFraud.IsToggled = true;
                }

                if (lblProItem.Text == item)
                {
                    SwtProbItem.IsToggled = true;
                }

                if (lblSabotage.Text == item)
                {
                    swtSabotage.IsToggled = true;
                }

                if (lblTheft.Text == item)
                {
                    swtThreat.IsToggled = true;
                }

                if (lblunaccess.Text == item)
                {
                    swtunaccess.IsToggled = true;
                }

                if (lblUnAircraft.Text == item)
                {
                    swtUnAircraft.IsToggled = true;
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

            if (assaultSwt.IsToggled)
            {
                selectedValues.Add(lblAssault.Text);
            }
            if (damageSwt.IsToggled)
            {
                selectedValues.Add(lblDamag.Text);
            }
            if (dispSwt.IsToggled)
            {
                selectedValues.Add(lblDisp.Text);
            }
            if (fraudSwt.IsToggled)
            {
                selectedValues.Add(lblFraud.Text);
            }
            if (inaCommentSwt.IsToggled)
            {
                selectedValues.Add(lblInaComment.Text);
            }
            if (SwtIncBrdPass.IsToggled)
            {
                selectedValues.Add(lblIncBrdPass.Text);
            }
            if (SwtPassFraud.IsToggled)
            {
                selectedValues.Add(lblPassFraud.Text);
            }

            if (SwtProbItem.IsToggled)
            {
                selectedValues.Add(lblProItem.Text);
            }

            if (swtSabotage.IsToggled)
            {
                selectedValues.Add(lblSabotage.Text);
            }

            if (swtThreat.IsToggled)
            {
                selectedValues.Add(lblTheft.Text);
            }

            if (swtunaccess.IsToggled)
            {
                selectedValues.Add(lblunaccess.Text);
            }

            if (swtUnAircraft.IsToggled)
            {
                selectedValues.Add(lblUnAircraft.Text);
            }

            MessagingCenter.Send<SecurityEventType, string>(this, "SelectedItems", string.Join(",", selectedValues));
            await PopupNavigation.PopAsync();

        }
    }
}
