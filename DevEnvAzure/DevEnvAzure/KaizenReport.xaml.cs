using DevEnvAzure.Models;
using DevEnvAzure.Utilities;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

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
        public KaizenReport(object viewObject, string modelname)
        {
            InitializeComponent();
            _KaizenReport = (KaizenReportModel)viewObject; //new Models.KaizenReportModel();

            this.BindingContext = _KaizenReport;

            Title = "Kaizen Report";
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
        private bool CheckConnection()
        {
            return CrossConnectivity.Current.IsConnected;
        }

        protected void CreateItems<U>(U reportObject) where U : class
        {
            try
            {
                ToggleBusy(true);
                Device.BeginInvokeOnMainThread(async () =>
                {
                    // StringContent contents = null;
                    var client = await OAuthHelper.GetHTTPClient();
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
                        string url = SPUtility.GetListURL(SPUtility.ReportType.Kaizen);
                        var postResult = await client.PostAsync(url, contents);

                        if (postResult.IsSuccessStatusCode)
                        {
                            App.DAUtil.Delete(_KaizenReport);

                            await DisplayAlert("Success", "Item created successfully", "Ok");
                            MessagingCenter.Send(this, "home");
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
    }
}