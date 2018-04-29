using DevEnvAzure.Models;
using Rg.Plugins.Popup.Extensions;
using Syncfusion.SfAutoComplete.XForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlightSafetyReportView : ContentView, INotifyPropertyChanged
    {
        public static string CommanderPForPMpickerValue;
        public static string FlightCrew1PFPMOBspickerValue;
        public static string FlightCrew2PFPMOBspickerValue;
        public static string IfflighteventselectphasepickerValue;
        public static string IfongroundselectwherepickerValue;
        public static string GearpickerValue;
        public static string SpeedbrakepickerValue;
        public static string MeteorologicalReportpickerValue;
        public static string LightpickerValue;
        public static string WeatherpickerValue;
        public static string PrecipitationpickerValue;
        public static string TurbulencepickerValue;
        public static string ConditionspickerValue;
        public static string NAVAIDSpickerValue;
        public static string ATCorAirportReportFiledpickerValue;
        public static string NumberofbirdspickerValue;
        public static string SizeofWildlifepickerValue;
        public static string AircraftDamagepickerValue;

        public static PeoplePicker PeoplePickerCommander;
        public static PeoplePicker PeoplePickercrew1email;
        public static PeoplePicker PeoplePickercrew2email;
        private static string name;

        public FlightSafetyReportView()
        {
            InitializeComponent();
            peoplePickerCommander.DataSource = App.peoplePickerDataSource;
            PeoplePickerCommander = null;
            peoplePickercrew1email.DataSource = App.peoplePickerDataSource;
            PeoplePickercrew1email = null;
            peoplePickercrew2email.DataSource = App.peoplePickerDataSource;
            PeoplePickercrew2email = null;
        }

        public FlightSafetyReportView(FlightSafetyReportModel obj)
        {
            InitializeComponent();

            if (App.peoplePickerDataSource == null)
            {
                var uss = SPUtility.GetUsersForPicker().Result;
                if (uss == null)
                    return;

                App.peoplePickerDataSource = new List<PeoplePicker>(uss);
            }

            peoplePickerCommander.DataSource = App.peoplePickerDataSource;
            PeoplePickerCommander = null;

            var selectedItem = App.peoplePickerDataSource.Find(x => { return Convert.ToString(x.Id) == obj.CommandersEmail; });
            if (selectedItem != null)
            {
                peoplePickerCommander.Text = selectedItem.Name;
                PeoplePickerCommander = selectedItem;
            }

            peoplePickercrew1email.DataSource = App.peoplePickerDataSource;
            PeoplePickercrew1email = null;

            var selectedItem1 = App.peoplePickerDataSource.Find(x => { return Convert.ToString(x.Id) == obj.FlightCrew1; });
            if (selectedItem1 != null)
            {
                peoplePickercrew1email.Text = selectedItem1.Name;
                PeoplePickercrew1email = selectedItem1;
            }

            peoplePickercrew2email.DataSource = App.peoplePickerDataSource;
            PeoplePickercrew2email = null;

            var selectedItem2 = App.peoplePickerDataSource.Find(x => { return Convert.ToString(x.Id) == obj.FlightCrew2; });
            if (selectedItem2 != null)
            {
                peoplePickercrew2email.Text = selectedItem2.Name;
                PeoplePickercrew2email = selectedItem2;
            }
        }

        private void CommanderPForPMpicker_changed(object sender, EventArgs e)
        {
            if (CommanderPForPMpicker.SelectedIndex > 0)
                CommanderPForPMpickerValue = CommanderPForPMpicker.Items.ElementAt(CommanderPForPMpicker.SelectedIndex);
        }
        private void FlightCrew1PFPMOBspicker_changed(object sender, EventArgs e)
        {
            if (FlightCrew1PFPMOBspicker.SelectedIndex > 0)
                FlightCrew1PFPMOBspickerValue = FlightCrew1PFPMOBspicker.Items.ElementAt(FlightCrew1PFPMOBspicker.SelectedIndex);
        }
        private void FlightCrew2PFPMOBspicker_changed(object sender, EventArgs e)
        {
            if (FlightCrew2PFPMOBspicker.SelectedIndex > 0)
                FlightCrew2PFPMOBspickerValue = FlightCrew2PFPMOBspicker.Items.ElementAt(FlightCrew2PFPMOBspicker.SelectedIndex);
        }
        private void Ifflighteventselectphasepicker_changed(object sender, EventArgs e)
        {
            if (Ifflighteventselectphasepicker.SelectedIndex > 0)
                IfflighteventselectphasepickerValue = Ifflighteventselectphasepicker.Items.ElementAt(Ifflighteventselectphasepicker.SelectedIndex);
        }
        private void Ifongroundselectwherepicker_changed(object sender, EventArgs e)
        {
            if (Ifongroundselectwherepicker.SelectedIndex > 0)
                IfongroundselectwherepickerValue = Ifongroundselectwherepicker.Items.ElementAt(Ifongroundselectwherepicker.SelectedIndex);
        }
        private void Gearpicker_changed(object sender, EventArgs e)
        {
            if (Gearpicker.SelectedIndex > 0)
                GearpickerValue = Gearpicker.Items.ElementAt(Gearpicker.SelectedIndex);
        }
        private void Speedbrakepicker_changed(object sender, EventArgs e)
        {
            if (Speedbrakepicker.SelectedIndex > 0)
                SpeedbrakepickerValue = Speedbrakepicker.Items.ElementAt(Speedbrakepicker.SelectedIndex);
        }
        private void MeteorologicalReportpicker_changed(object sender, EventArgs e)
        {
            if (MeteorologicalReportpicker.SelectedIndex > 0)
                MeteorologicalReportpickerValue = MeteorologicalReportpicker.Items.ElementAt(MeteorologicalReportpicker.SelectedIndex);
        }
        private void Lightpicker_changed(object sender, EventArgs e)
        {
            if (Lightpicker.SelectedIndex > 0)
                LightpickerValue = Lightpicker.Items.ElementAt(Lightpicker.SelectedIndex);
        }
        private void Weatherpicker_changed(object sender, EventArgs e)
        {
            if (Weatherpicker.SelectedIndex > 0)
                WeatherpickerValue = Weatherpicker.Items.ElementAt(Weatherpicker.SelectedIndex);
        }
        private void Precipitationpicker_changed(object sender, EventArgs e)
        {
            if (Precipitationpicker.SelectedIndex > 0)
                PrecipitationpickerValue = Precipitationpicker.Items.ElementAt(Precipitationpicker.SelectedIndex);
        }
        private void Turbulencepicker_changed(object sender, EventArgs e)
        {
            if (Turbulencepicker.SelectedIndex > 0)
                TurbulencepickerValue = Turbulencepicker.Items.ElementAt(Turbulencepicker.SelectedIndex);
        }
        private void Conditionspicker_changed(object sender, EventArgs e)
        {
            if (Conditionspicker.SelectedIndex > 0)
                ConditionspickerValue = Conditionspicker.Items.ElementAt(Conditionspicker.SelectedIndex);
        }
        private void NAVAIDSpicker_changed(object sender, EventArgs e)
        {
            if (NAVAIDSpicker.SelectedIndex > 0)
                NAVAIDSpickerValue = NAVAIDSpicker.Items.ElementAt(NAVAIDSpicker.SelectedIndex);
        }
        private void ATCorAirportReportFiledpicker_changed(object sender, EventArgs e)
        {
            if (ATCorAirportReportFiledpicker.SelectedIndex > 0)
                ATCorAirportReportFiledpickerValue = ATCorAirportReportFiledpicker.Items.ElementAt(ATCorAirportReportFiledpicker.SelectedIndex);
        }
        private void Numberofbirdspicker_changed(object sender, EventArgs e)
        {
            if (Numberofbirdspicker.SelectedIndex > 0)
                NumberofbirdspickerValue = Numberofbirdspicker.Items.ElementAt(Numberofbirdspicker.SelectedIndex);
        }
        private void SizeofWildlifepicker_changed(object sender, EventArgs e)
        {
            if (SizeofWildlifepicker.SelectedIndex > 0)
                SizeofWildlifepickerValue = SizeofWildlifepicker.Items.ElementAt(SizeofWildlifepicker.SelectedIndex);
        }
        private void AircraftDamagepicker_changed(object sender, EventArgs e)
        {
            if (AircraftDamagepicker.SelectedIndex > 0)
                AircraftDamagepickerValue = AircraftDamagepicker.Items.ElementAt(AircraftDamagepicker.SelectedIndex);
        }
        private async void approchType_Focused(object sender, EventArgs e)
        {

        }
        private async void approachmulti_clicked(object sender, EventArgs e)
        {
            // ApproachTypeEntry.BindingContext = MultiSelectMenuPage1.approachvalue;
            await Navigation.PushPopupAsync(new MultiSelectMenuPage1(ApproachTypeEntry.Text));

            MessagingCenter.Subscribe<MultiSelectMenuPage1, string>(this, "SelectedItems", (sender1, arg) =>
            {
                ApproachTypeEntry.Text = arg.ToString().TrimEnd(',');
            });
        }
        private async void reasondeviamulti_clicked(object sender, EventArgs e)
        {
            // ApproachTypeEntry.BindingContext = MultiSelectMenuPage1.approachvalue;
            await Navigation.PushPopupAsync(new ReasonforDeviationMultiSelect(ReasonforDeviationEntry.Text));

            MessagingCenter.Subscribe<ReasonforDeviationMultiSelect, string>(this, "SelectedItems", (sender1, arg) =>
            {
                ReasonforDeviationEntry.Text = arg.ToString();
            });
        }
        private async void RealtivePosimulti_clicked(object sender, EventArgs e)
        {
            // ApproachTypeEntry.BindingContext = MultiSelectMenuPage1.approachvalue;
            await Navigation.PushPopupAsync(new IntruderRelativePositionMultiSelect(RelativepositionEntry.Text));

            MessagingCenter.Subscribe<IntruderRelativePositionMultiSelect, string>(this, "SelectedItems", (sender1, arg) =>
            {
                RelativepositionEntry.Text = arg.ToString();
            });
        }
        private async void unf(object sender, EventArgs e)
        {
            //  
        }

        private void PeoplePickerCommander_SelectionChanged(object sender, Syncfusion.SfAutoComplete.XForms.SelectionChangedEventArgs e)
        {
            PeoplePickerCommander = (PeoplePicker)e.Value;
            var picker = ((SfAutoComplete)sender);
            picker.BorderColor = Color.Green;
        }

        private void PeoplePickercrew1email_SelectionChanged(object sender, Syncfusion.SfAutoComplete.XForms.SelectionChangedEventArgs e)
        {
            PeoplePickercrew1email = (PeoplePicker)e.Value;
            var picker = ((SfAutoComplete)sender);
            picker.BorderColor = Color.Green;
        }

        private void PeoplePickercrew2email_SelectionChanged(object sender, Syncfusion.SfAutoComplete.XForms.SelectionChangedEventArgs e)
        {
            PeoplePickercrew2email = (PeoplePicker)e.Value;
            var picker = ((SfAutoComplete)sender);
            picker.BorderColor = Color.Green;
        }

        private void peoplePickerCommander_ValueChanged(object sender, Syncfusion.SfAutoComplete.XForms.ValueChangedEventArgs e)
        {
            var found = App.validatePeoplePicker(e.Value);
            var picker = ((SfAutoComplete)sender);
            if (found == null)
            {
                PeoplePickercrew1email = null;

                if (!string.IsNullOrEmpty(e.Value))
                {
                    picker.BorderColor = Color.OrangeRed;
                }
            }
            else
            {
                PeoplePickerCommander = found;
                picker.BorderColor = Color.Green;
            }
        }

        private void peoplePickercrew1email_ValueChanged(object sender, Syncfusion.SfAutoComplete.XForms.ValueChangedEventArgs e)
        {
            var found = App.validatePeoplePicker(e.Value);
            var picker = ((SfAutoComplete)sender);
            if (found == null)
            {
                PeoplePickercrew1email = null;

                if (!string.IsNullOrEmpty(e.Value))
                {
                    picker.BorderColor = Color.OrangeRed;
                }
            }
            else
            {
                PeoplePickercrew1email = found;
                picker.BorderColor = Color.Green;
            }
        }

        private void peoplePickercrew2email_ValueChanged(object sender, Syncfusion.SfAutoComplete.XForms.ValueChangedEventArgs e)
        {
            var found = App.validatePeoplePicker(e.Value);
            var picker = ((SfAutoComplete)sender);
            if (found == null)
            {
                PeoplePickercrew1email = null;

                if (!string.IsNullOrEmpty(e.Value))
                {
                    picker.BorderColor = Color.OrangeRed;
                }
            }
            else
            {
                PeoplePickercrew1email = found;
                picker.BorderColor = Color.Green;
            }
        }
    }
}