﻿using DevEnvAzure.Models;
using DevEnvAzure.Utilities;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlightCrewVoyageRecord : ContentPage
    {
        public static string SectorNumberpickerValue;
        public static string AircraftRegistrationpickerValue;
        public static string LandingManualOrAutopickerValue;
        public static string ReportCategoriespickerValue;
        public static string ReplyRequiredpickerValue;
        public static string RankpickerValue;
        Models.FlightCrewVoyageRecordModel _flightcrew;
        Jsonpropertyinitialise jsonInitObj = new Jsonpropertyinitialise();
        public FlightCrewVoyageRecord(object viewObject, string modelname)
        {
            try
            {
                InitializeComponent();
                _flightcrew = (FlightCrewVoyageRecordModel)viewObject;
                _flightcrew.ScheduledDeparture = DateTime.Now;
                this.BindingContext = _flightcrew;

                Title = "Flightcrew Voyage Record";
                ReportRaisedByEntry.DataSource = App.peoplePickerDataSource;
            }
            catch(Exception ex)
            {

            }
        }
        private void Save_clicked(object sender, XLabs.EventArgs<bool> e)
        {
            _flightcrew.ReportType = null;
           _flightcrew.DateOfEvent = null;

            CreateItems(jsonInitObj.getFlightCrewVoyageJson(_flightcrew));
        }
        private void savedrafts_btn_Clicked(object sender, EventArgs e)
        {
            _flightcrew.ReportType = "Flight Crew" + _flightcrew.Id.ToString();
            _flightcrew.DateOfEvent = DateTime.Now;
           App.DAUtil.SaveEmployee<Models.FlightCrewVoyageRecordModel>(_flightcrew);
        }
        private void SectorNumber_changed(object sender, EventArgs e)
        {
            if (SectorNumberpicker.SelectedIndex > 0)
                SectorNumberpickerValue = SectorNumberpicker.Items.ElementAt(SectorNumberpicker.SelectedIndex);
        }
        private void AircraftRegistration_changed(object sender, EventArgs e)
        {
            if (AircraftRegistrationpicker.SelectedIndex > 0)
                AircraftRegistrationpickerValue = AircraftRegistrationpicker.Items.ElementAt(AircraftRegistrationpicker.SelectedIndex);
        }
        private void ManualorAuto_changed(object sender, EventArgs e)
        {
            if (ManualorAutopicker.SelectedIndex > 0)
                LandingManualOrAutopickerValue = ManualorAutopicker.Items.ElementAt(ManualorAutopicker.SelectedIndex);
        }
        private void ReportCategories_changed(object sender, EventArgs e)
        {
            if (ReportCategoriespicker.SelectedIndex > 0)
                ReportCategoriespickerValue = ReportCategoriespicker.Items.ElementAt(ReportCategoriespicker.SelectedIndex);
        }
        private void ReplyRequired_changed(object sender, EventArgs e)
        {
            if (ReplyRequiredpicker.SelectedIndex > 0)
                ReplyRequiredpickerValue = ReplyRequiredpicker.Items.ElementAt(ReplyRequiredpicker.SelectedIndex);
        }
        private void Rank_changed(object sender, EventArgs e)
        {
            if (Rankpicker.SelectedIndex > 0)
                RankpickerValue = Rankpicker.Items.ElementAt(Rankpicker.SelectedIndex);
        }
        private bool CheckConnection()
        {
            return CrossConnectivity.Current.IsConnected;

            //var networkConnection = DependencyService.Get<INetworkConnection>();
            //networkConnection.CheckNetworkConnection();
            //return networkConnection.IsConnected;
        }
        private HttpClient GetHTTPClient()
        {
            var client = OAuthHelper.GetHTTPClient();

            if (client == null)
            {
                return null;
            }

            return client;
        }
        protected void CreateItems<U>(U reportObject) where U : class
        {
            try
            {
                // StringContent contents = null;
                var client = GetHTTPClient();
                var data = reportObject;// _viewobject;

                var body = JsonConvert.SerializeObject(data, Formatting.None,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        });
                var contents = new StringContent(body);
                contents.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;odata=verbose");
                if (CheckConnection())
                {
                    var postResult = client.PostAsync("https://sptechnophiles.sharepoint.com/_api/web/lists/GetByTitle('Flight Crew Voyage Record')/items", contents).Result;
                    //var result = postResult.EnsureSuccessStatusCode();

                    if (!postResult.IsSuccessStatusCode)
                    {
                        // Unwrap the response and throw as an Api Exception:
                        var ex = OAuthHelper.CreateExceptionFromResponseErrors(postResult);

                    }
                    if (postResult.IsSuccessStatusCode)
                    {
                        DependencyService.Get<IMessage>().LongAlert("List updated successfully");
                    }
                    else
                    {
                        DatatableData dt = new DatatableData();
                        App.DAUtil.SaveEmployee<DatatableData>(dt);
                        DependencyService.Get<IMessage>().LongAlert("List data stored in local storage");
                    }
                }
                else
                {

                    DatatableData dt = new DatatableData();
                    dt.Value = body;// contents.ToString();
                    App.DAUtil.SaveEmployee<DatatableData>(dt);

                    var vList = App.DAUtil.GetAllEmployees<DatatableData>("DatatableData1");
                    DependencyService.Get<IMessage>().LongAlert("List data stored in local storage");
                }
            }
            catch (HttpRequestException ex)
            {
                DependencyService.Get<IMessage>().ShortAlert("Upload Error");
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert("Upload Error" + ex.Message);
            }
        }
    }
}