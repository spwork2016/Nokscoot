﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using DevEnvAzure.Models;
using DevEnvAzure.Utilities;
using Plugin.Connectivity;
using System.Threading.Tasks;
using DevEnvAzure.Model;
using System.IO;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KaizenReport : ContentPage
    {
        public static string BenefitsCategorypickerValue;
        Models.KaizenReportModel _KaizenReport;
        Jsonpropertyinitialise jsonInitObj = new Jsonpropertyinitialise();
        AttachmentView _attachementView;
        public KaizenReport(object viewObject, string modelname)
        {
            InitializeComponent();
            _KaizenReport = (KaizenReportModel)viewObject; //new Models.KaizenReportModel();
            _attachementView = new AttachmentView();

            BindingContext = _KaizenReport;

            Title = "Kaizen Report";
            stkAttachment.Children.Add(_attachementView);
        }
        private void Save_clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_KaizenReport.Subject))
            {
                SubjectLbl.TextColor = Color.Red;
                SubjectEntry.Focus();
                return;
            }
            else
            {
                SubjectLbl.TextColor = Color.Black;
            }

            _KaizenReport.ReportType = null;
            _KaizenReport.DateOfEvent = null;
            CreateItems(jsonInitObj.getKaizenReportJson(_KaizenReport));
        }

        private void savedrafts_btn_Clicked(object sender, EventArgs e)
        {
            _KaizenReport.ReportType = string.IsNullOrEmpty(_KaizenReport.ReportType) ? "Kaizen Report" + _KaizenReport.Id.ToString() : _KaizenReport.ReportType;
            _KaizenReport.DateOfEvent = DateTime.Now;
            _KaizenReport.Created = DateTime.Now;
            _KaizenReport = App.DAUtil.SaveOrUpdate(_KaizenReport);
            DependencyService.Get<IMessage>().ShortAlert("Item drafted");
        }

        private void BenefitsCategorypicker_changed(object sender, EventArgs e)
        {
            if (BenefitsCategorypicker.SelectedIndex > 0)
                BenefitsCategorypickerValue = BenefitsCategorypicker.Items.ElementAt(BenefitsCategorypicker.SelectedIndex);
        }

        private async void Attachments_Clicked(object sender, EventArgs e)
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

        private bool CheckConnection()
        {
            return CrossConnectivity.Current.IsConnected;
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

        protected void CreateItems<U>(U reportObject) where U : class
        {
            try
            {
                ToggleBusy(true);
                Device.BeginInvokeOnMainThread(async () =>
                {
                    lblLoading.Text = "Sending...";
                    var client = await OAuthHelper.GetHTTPClient();
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
                        string url = SPUtility.GetListURL(SPUtility.ReportType.Kaizen);
                        var postResult = await client.PostAsync(url, contents);

                        if (postResult.IsSuccessStatusCode)
                        {
                            App.DAUtil.Delete(_KaizenReport);
                            lblLoading.Text = "Item created successfully." + Environment.NewLine;

                            var spData = JsonConvert.DeserializeObject<SPData>(postResult.Content.ReadAsStringAsync().Result,
                                new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
                            int itemId = spData.d.Id;
                            await SendAttachments(itemId);
                        }
                        else
                        {
                            var ex = await postResult.Content.ReadAsStringAsync();
                            await DisplayAlert("Error", ex, "Ok");
                        }
                        ToggleBusy(false);
                    }
                    else
                    {
                        OfflineItem dt = new OfflineItem();
                        dt.Created = DateTime.Now;
                        dt.ReportType = (int)SPUtility.ReportType.Kaizen;
                        dt.Value = body;
                        dt.Attachments = _attachementView.GetAttachmentInfoAsString();
                        App.DAUtil.Save<OfflineItem>(dt);

                        var vList = App.DAUtil.GetAll<OfflineItem>("OfflineItem");
                        await DisplayAlert("", "Item stored in local storage", "Ok");
                        ToggleBusy(false);
                        MessagingCenter.Send(this, "home");
                    }
                });
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

        private async void AttachmentList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void MenuItem_OnDelete_Clicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }
    }
}