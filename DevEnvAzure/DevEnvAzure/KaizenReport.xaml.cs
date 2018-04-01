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
                    var postResult = client.PostAsync("https://sptechnophiles.sharepoint.com/_api/web/lists/GetByTitle('Kaizen Report')/items", contents).Result;

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