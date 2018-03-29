﻿using Rg.Plugins.Popup.Extensions;
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
        private static string name;

        public FlightSafetyReportView()
        {
            InitializeComponent();
            peoplePickerCommander.DataSource = App.peoplePickerDataSource;
            peoplePickercrew1email.DataSource = App.peoplePickerDataSource;
            peoplePickercrew2email.DataSource = App.peoplePickerDataSource;
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

            MessagingCenter.Subscribe<MultiSelectMenuPage1, string>(this, "", (sender1, arg) =>
            {
                ApproachTypeEntry.Text = arg.ToString().TrimEnd(',');
            });
        }
        private async void reasondeviamulti_clicked(object sender, EventArgs e)
        {
            // ApproachTypeEntry.BindingContext = MultiSelectMenuPage1.approachvalue;
            await Navigation.PushPopupAsync(new ReasonforDeviationMultiSelect(ReasonforDeviationEntry.Text));

            MessagingCenter.Subscribe<ReasonforDeviationMultiSelect, string>(this, "", (sender1, arg) =>
            {
                ReasonforDeviationEntry.Text = arg.ToString();
            });
        }
        private async void RealtivePosimulti_clicked(object sender, EventArgs e)
        {
            // ApproachTypeEntry.BindingContext = MultiSelectMenuPage1.approachvalue;
            await Navigation.PushPopupAsync(new IntruderRelativePositionMultiSelect(RelativepositionEntry.Text));

            MessagingCenter.Subscribe<IntruderRelativePositionMultiSelect, string>(this, "", (sender1, arg) =>
            {
                RelativepositionEntry.Text = arg.ToString();
            });
        }
        private async void unf(object sender, EventArgs e)
        {
            //  
        }
    }
}