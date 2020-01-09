using DevEnvAzure.Model;
using DevEnvAzure.Models;
using DevEnvAzure.Utilities;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            _KaizenReport = (KaizenReportModel)viewObject;
            _attachementView = new AttachmentView();

            BindingContext = _KaizenReport;

            Title = "Kaizen Report";
            stkAttachment.Children.Add(_attachementView);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var allFilesExists = await _attachementView.CheckAttachments(_KaizenReport.Attachments);
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

        protected void Save_clicked(object sender, EventArgs e)
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
            _KaizenReport.Attachments = _attachementView.GetAttachmentInfoAsString();
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
                await _attachementView.AskForAttachment(selectedOption);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private bool CheckConnection()
        {
            return SPUtility.IsConnected();
        }

        private async Task SendAttachments(string itemURL, int itemId)
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
                            SPUtility.GetListURL(ReportType.Kaizen), itemId, item.FileName);

                        item.SaveToURL = attachmentURL;

                        lblLoading.Text += "Sending - " + item.FileName + Environment.NewLine;

                        var attachemntResponse = await item.PostAttachment();
                        if (!attachemntResponse.IsSuccessStatusCode)
                        {
                            var msg = await attachemntResponse.Content.ReadAsStringAsync();
                            lblLoading.Text += "Failed - " + item.FileName + " - " + msg + Environment.NewLine;
                        }
                        else
                        {
                            filesSent++;
                            lblLoading.Text += "Sent - " + item.FileName + Environment.NewLine;
                            // let sharepoint to complete its task before sending a new one
                            await Task.Delay(1000);
                        }
                    }
                }

                ToggleBusy(false);

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
                    var client = await OAuthHelper.GetHTTPClientAsync();
                    var data = reportObject;

                    // https://github.com/microsoftgraph/msgraph-sdk-dotnet/issues/308

                    var body = JsonConvert.SerializeObject(data, Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });

                    var contents = new StringContent(body);
                    contents.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    if (CheckConnection())
                    {
                        string url = SPUtility.GetListURL(ReportType.Kaizen);
                        var postResult = await client.PostAsync(url, contents);

                        if (postResult.IsSuccessStatusCode)
                        {
                            App.DAUtil.Delete(_KaizenReport);
                            lblLoading.Text = "Item created successfully." + Environment.NewLine;

                            var spData = JsonConvert.DeserializeObject<SPData>(await postResult.Content.ReadAsStringAsync(),
                                new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
                            int itemId = spData.d.Id;

                            await Task.Delay(500);
                            await SendAttachments(url, itemId);
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
                        SPUtility.SaveOfflineItem(body, ReportType.Kaizen, _attachementView.GetAttachmentInfoAsString());

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

        private void MenuItem_OnDelete_Clicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }
    }
}