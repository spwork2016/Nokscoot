using DevEnvAzure.Model;
using DevEnvAzure.Utilities;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using static DevEnvAzure.SPUtility;
using System.IO;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StationInformation : ContentPage
    {
        Models.StationInformationModel _StationInformation;
        Jsonpropertyinitialise jsonInitObj = new Jsonpropertyinitialise();
        AttachmentView _attachementView;
        public StationInformation(object viewObject)
        {
            _StationInformation = (Models.StationInformationModel)viewObject;
            this.BindingContext = _StationInformation;
            InitializeComponent();

            _attachementView = new AttachmentView();
            //stkAttachment.Children.Add(_attachementView);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var allFilesExists = await _attachementView.CheckAttachments(_StationInformation.Attachments);
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

        private async void Save_clicked(object sender, XLabs.EventArgs<bool> e)
        {
            if (string.IsNullOrEmpty(_StationInformation.IATACode))
            {
                IATACodeLbl.TextColor = Color.Red;
                IATACodeEntry.Focus();
                return;
            }
            else
            {
                IATACodeLbl.TextColor = Color.Black;
            }

            _StationInformation.ReportType = null;
            _StationInformation.DateOfEvent = null;
            DataContracts.StationInformationSp spData = null;
            try
            {
                spData = jsonInitObj.getStationInformationJson(_StationInformation);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Please fill valid data", ex.Message, "Ok");
                return;
            }
            CreateItems(spData);
        }
        private void savedrafts_btn_Clicked(object sender, EventArgs e)
        {
            _StationInformation.ReportType = "Station Information" + _StationInformation.Id.ToString();
            _StationInformation.DateOfEvent = DateTime.Now;
            _StationInformation.Created = DateTime.Now;
            _StationInformation.Attachments = _attachementView.GetAttachmentInfoAsString();

            _StationInformation = App.DAUtil.SaveOrUpdate(_StationInformation);

            DependencyService.Get<IMessage>().ShortAlert("Item drafted");
        }
        private bool CheckConnection()
        {
            return SPUtility.IsConnected();
        }

        private void ToggleBusy(bool flag)
        {
            activityStack.IsVisible = flag;
            activityIndicator.IsRunning = flag;
        }

        protected void CreateItems<U>(U reportObject) where U : class
        {
            try
            {
                ToggleBusy(true);
                Device.BeginInvokeOnMainThread(async () =>
                {
                    // StringContent contents = null;
                    var client = await OAuthHelper.GetHTTPClientAsync();
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
                        string url = SPUtility.GetListURL(ReportType.SationInfo);
                        var postResult = await client.PostAsync(url, contents);

                        if (postResult.IsSuccessStatusCode)
                        {
                            App.DAUtil.Delete(_StationInformation);

                            lblLoading.Text = "Item created successfully." + Environment.NewLine;

                            var spData = JsonConvert.DeserializeObject<SPData>(await postResult.Content.ReadAsStringAsync(),
                                new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
                            int itemId = spData.d.Id;

                            await Task.Delay(500);
                            await SendAttachments(itemId);

                            MessagingCenter.Send(this, "home");
                        }
                        else
                        {
                            var ex = await postResult.Content.ReadAsStringAsync();
                            await DisplayAlert("Sharepoint error", ex, "Ok");
                        }
                        ToggleBusy(false);
                    }
                    else
                    {
                        SaveOfflineItem(body, ReportType.SationInfo, _attachementView.GetAttachmentInfoAsString());

                        DependencyService.Get<IMessage>().LongAlert("Item stored in local storage");
                        ToggleBusy(false);
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
                            GetListURL(ReportType.SationInfo), itemId, item.FileName);

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
                await _attachementView.AskForAttachment(selectedOption);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_StationInformation.NameofAirportLink))
            {
                Device.OpenUri(new Uri(_StationInformation.NameofAirportLink));
            }
        }
    }
}