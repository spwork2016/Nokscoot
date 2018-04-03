using DevEnvAzure.Models;
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
        private void Save_clicked(object sender, XLabs.EventArgs<bool> e)
        {
            _KaizenReport.ReportType = null;
            _KaizenReport.DateOfEvent = null;
            CreateItems(jsonInitObj.getKaizenReportJson(_KaizenReport));
        }
        private void savedrafts_btn_Clicked(object sender, EventArgs e)
        {
            _KaizenReport.ReportType = string.IsNullOrEmpty(_KaizenReport.ReportType) ? "Kaizen Report" + _KaizenReport.Id.ToString() : _KaizenReport.ReportType;
            _KaizenReport.DateOfEvent = DateTime.Now;
            _KaizenReport = _KaizenReport.Id == 0 ? App.DAUtil.Save(_KaizenReport) : App.DAUtil.Update(_KaizenReport);
            DependencyService.Get<IMessage>().ShortAlert("Kaizen report drafted");
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
                ToggleBusy(true);
                Device.BeginInvokeOnMainThread(async () =>
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
                        var postResult = await client.PostAsync("https://sptechnophiles.sharepoint.com/_api/web/lists/GetByTitle('Kaizen Report')/items", contents);

                        if (postResult.IsSuccessStatusCode)
                        {
                            App.DAUtil.Delete(_KaizenReport);
                            DependencyService.Get<IMessage>().LongAlert("List updated successfully");
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

                        DatatableData dt = new DatatableData();
                        dt.Value = body;// contents.ToString();
                        App.DAUtil.Save<DatatableData>(dt);

                        var vList = App.DAUtil.GetAll<DatatableData>("DatatableData1");
                        DependencyService.Get<IMessage>().LongAlert("List data stored in local storage");
                        ToggleBusy(false);
                        await Navigation.PushAsync(new HomePage());
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