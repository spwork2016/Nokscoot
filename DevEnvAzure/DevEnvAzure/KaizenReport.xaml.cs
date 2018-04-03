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
        public KaizenReport()
        {
            _KaizenReport = new Models.KaizenReportModel();
            //_KaizenReport.DateofCompletion = DateTime.Now;
            //_KaizenReport.ImplementationDate = DateTime.Now;

            this.BindingContext = _KaizenReport;
            InitializeComponent();
        }
        private void Save_clicked(object sender, XLabs.EventArgs<bool> e)
        {
            CreateItems(jsonInitObj.getKaizenReportJson(_KaizenReport));
        }
        private void savedrafts_btn_Clicked(object sender, EventArgs e)
        {
            App.DAUtil.SaveEmployee<KaizenReportModel>(_KaizenReport);
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
                            DependencyService.Get<IMessage>().LongAlert("List updated successfully");
                            App.ResetToHome();
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
                        App.DAUtil.SaveEmployee<DatatableData>(dt);

                        var vList = App.DAUtil.GetAllEmployees<DatatableData>("DatatableData1");
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