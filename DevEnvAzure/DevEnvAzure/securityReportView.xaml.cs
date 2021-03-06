﻿using Rg.Plugins.Popup.Extensions;
using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class securityReportView : ContentView
    {
        public static string flightphase;
        public static string flightwheresel;
        public securityReportView()
        {
            InitializeComponent();
        }
        private void flightphase_selected(object sender, EventArgs e)
        {
            if (FlightPhasepicker.SelectedIndex > 0)
                flightphase = FlightPhasepicker.Items.ElementAt(FlightPhasepicker.SelectedIndex);
        }
        private void flightwhere_selected(object sender, EventArgs e)
        {
            if (flightwherepicker.SelectedIndex > 0)
                flightwheresel = flightwherepicker.Items.ElementAt(flightwherepicker.SelectedIndex);
        }

        private async void secEventEntrybtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new SecurityEventType(secEventEntry.Text));

            MessagingCenter.Subscribe<SecurityEventType, string>(this, "SelectedItems", (sender1, arg) =>
            {
                secEventEntry.Text = arg.ToString().TrimEnd(',');
            });
        }
    }
}