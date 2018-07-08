﻿using DevEnvAzure.Model;
using DevEnvAzure.Models;
using DevEnvAzure.Utilities;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static DevEnvAzure.SPUtility;

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
        AttachmentView _attachementView;
        public FlightCrewVoyageRecord(object viewObject, string modelname)
        {
            try
            {
                InitializeComponent();
                _flightcrew = (FlightCrewVoyageRecordModel)viewObject;
                _flightcrew.ScheduledDeparture = DateTime.Now;
                this.BindingContext = _flightcrew;

                ReportRaisedByEntry.DataSource = App.peoplePickerDataSource;
                if (!string.IsNullOrEmpty(_flightcrew.ReportRaisedBy))
                {
                    var selectedItem = App.peoplePickerDataSource.Find(x => { return Convert.ToString(x.Id) == _flightcrew.ReportRaisedBy; });
                    if (selectedItem != null)
                    {
                        ReportRaisedByEntry.Text = selectedItem.Name;
                        ReportRaisedByEntry.SelectedItem = selectedItem;
                    }
                }

                _attachementView = new AttachmentView();
                stkAttachment.Children.Add(_attachementView);
            }
            catch (Exception ex)
            {

            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var allFilesExists = await _attachementView.CheckAttachments(_flightcrew.Attachments);
                if (!allFilesExists)
                {
                    await DisplayAlert("Warning", SPUtility.ATTACHMENT_FILES_NOT_FOUND, "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }


        private bool ValidatePeoplePickers()
        {
            if (!string.IsNullOrEmpty(_flightcrew.ReportRaisedBy) && App.validatePeoplePicker(_flightcrew.ReportRaisedBy) != null)
                _flightcrew.ReportRaisedBy = ((PeoplePicker)ReportRaisedByEntry.SelectedItem).Id.ToString();
            else if (!string.IsNullOrEmpty(_flightcrew.ReportRaisedBy))
            {
                ReportRaisedByEntry.SelectedItem = null;
                ReportRaisedByEntry.ShowBorder = true;
                ReportRaisedByEntry.BorderColor = Color.OrangeRed;
                ReportRaisedByEntry.Focus();
                return false;
            }

            return true;
        }

        private void Save_clicked(object sender, XLabs.EventArgs<bool> e)
        {
            _flightcrew.ReportType = null;
            _flightcrew.DateOfEvent = null;
            if (!ValidatePeoplePickers()) return;

            if (string.IsNullOrEmpty(_flightcrew.VoyageRecord))
            {
                VoyageRecordLbl.TextColor = Color.Red;
                VoyageRecordEntry.Focus();
                return;
            }
            else
            {
                VoyageRecordLbl.TextColor = Color.Black;
            }

            ReportRaisedByEntry.SelectedItem = null;
            ReportRaisedByEntry.ShowBorder = false;

            CreateItems(jsonInitObj.getFlightCrewVoyageJson(_flightcrew));
        }

        private void savedrafts_btn_Clicked(object sender, EventArgs e)
        {
            _flightcrew.ReportType = string.IsNullOrEmpty(_flightcrew.ReportType) ? "Flight Crew" + _flightcrew.Id.ToString() : _flightcrew.ReportType;
            _flightcrew.DateOfEvent = DateTime.Now;
            if (!ValidatePeoplePickers()) return;
            _flightcrew.Created = DateTime.Now;
            _flightcrew.Attachments = _attachementView.GetAttachmentInfoAsString();

            _flightcrew = App.DAUtil.SaveOrUpdate(_flightcrew);
            DependencyService.Get<IMessage>().ShortAlert("Item drafted");
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
        }

        private async Task<HttpClient> GetHTTPClient()
        {
            var client = await OAuthHelper.GetHTTPClient();

            if (client == null)
            {
                return null;
            }

            return client;
        }
        protected async void CreateItems<U>(U reportObject) where U : class
        {
            try
            {
                ToggleBusy(true);

                var client = await GetHTTPClient();
                var data = reportObject;

                var body = JsonConvert.SerializeObject(data, Formatting.None,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        });
                var contents = new StringContent(body);
                contents.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;odata=verbose");
                if (CheckConnection())
                {

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        string url = SPUtility.GetListURL(ReportType.FlightCrewVoyageReport);
                        var postResult = await client.PostAsync(url, contents);
                        if (postResult.IsSuccessStatusCode)
                        {
                            App.DAUtil.Delete(_flightcrew);

                            lblLoading.Text = "Item created successfully." + Environment.NewLine;

                            var spData = JsonConvert.DeserializeObject<SPData>(postResult.Content.ReadAsStringAsync().Result,
                                new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
                            int itemId = spData.d.Id;
                            await SendAttachments(itemId);

                            MessagingCenter.Send(this, "home");
                        }
                        else
                        {
                            var ex = await postResult.Content.ReadAsStringAsync();
                            await DisplayAlert("Error", ex, "Ok");
                        }

                        ToggleBusy(false);
                    });
                }
                else
                {

                    OfflineItem dt = new OfflineItem();
                    dt.Value = body;
                    dt.Created = DateTime.Now;
                    dt.ReportType = (int)ReportType.FlighCrewVoyage;
                    dt.Attachments = _attachementView.GetAttachmentInfoAsString();

                    App.DAUtil.Save(dt);
                    App.DAUtil.GetAll<OfflineItem>("OfflineItem");

                    DependencyService.Get<IMessage>().LongAlert("Item stored in local storage");
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

        private void ToggleBusy(bool flag)
        {
            activityStack.IsVisible = flag;
            activityIndicator.IsRunning = flag;
        }

        private async Task SendAttachments(int itemId)
        {
            try
            {
                var attachments = _attachementView.GetAttachments();
                int filesSent = 0;
                if (attachments.Count > 0)
                {
                    foreach (var item in attachments)
                    {
                        string attachmentURL = string.Format("{0}({1})/AttachmentFiles/add(FileName='{2}')",
                            SPUtility.GetListURL(SPUtility.ReportType.Kaizen), itemId, item.FileName);

                        Stream stream = await item.GetStream();
                        if (stream == null)
                        {
                            lblLoading.Text += "Not found - " + item.FileName + Environment.NewLine;
                            continue;
                        }

                        lblLoading.Text += "Sending - " + item.FileName + Environment.NewLine;
                        var attachemntResponse = await SPUtility.SaveAttachment(attachmentURL, stream);
                        if (!attachemntResponse.IsSuccessStatusCode)
                        {
                            var msg = await attachemntResponse.Content.ReadAsStringAsync();
                            lblLoading.Text += "Failed - " + item.FileName + " - " + msg + Environment.NewLine;
                            //await DisplayAlert("Error - Unable to post attachments", msg, "Ok");

                        }
                        else
                        {
                            filesSent++;
                            lblLoading.Text += "Sent - " + item.FileName + Environment.NewLine;
                        }
                    }
                }

                if (filesSent == attachments.Count)
                {
                    await DisplayAlert("Success", "Item created successfully", "Ok");
                }
                else
                {
                    await DisplayAlert("Info", lblLoading.Text, "Ok");

                }

                MessagingCenter.Send(this, "home");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private async void attachments_Clicked(object sender, EventArgs e)
        {
            string selectedOption = await DisplayActionSheet("Attachment", "Cancel", null, ClientConfiguration.Default.AttachmentOptions);
            try
            {
                _attachementView.AskForAttachment(selectedOption);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }
    }
}