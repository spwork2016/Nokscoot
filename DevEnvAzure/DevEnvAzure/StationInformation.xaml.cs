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
    public partial class StationInformation : ContentPage
    {
        Models.StationInformationModel _StationInformation;
        Jsonpropertyinitialise jsonInitObj = new Jsonpropertyinitialise();
        public StationInformation(object viewObject, string modelname)
        {
            _StationInformation = (Models.StationInformationModel)viewObject;
            this.BindingContext = _StationInformation;
            InitializeComponent();
            Title = "Station Information";
        }
        private void Save_clicked(object sender, XLabs.EventArgs<bool> e)
        {
            _StationInformation.ReportType = null;
            _StationInformation.DateOfEvent = null;
            CreateItems(jsonInitObj.getStationInformationJson(_StationInformation));
        }
        private void savedrafts_btn_Clicked(object sender, EventArgs e)
        {
            _StationInformation.ReportType = "Station Information" + _StationInformation.Id.ToString();
            _StationInformation.DateOfEvent = DateTime.Now;
            _StationInformation = _StationInformation.Id == 0 ? App.DAUtil.Save(_StationInformation) : App.DAUtil.Update(_StationInformation);

            DependencyService.Get<IMessage>().ShortAlert("Item drafted");
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
                        var postResult = await client.PostAsync("https://sptechnophiles.sharepoint.com/_api/web/lists/GetByTitle('(Ops) Line Station Information')/items", contents);

                        if (postResult.IsSuccessStatusCode)
                        {
                            App.DAUtil.Delete(_StationInformation);

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
                        DatatableData dt = new DatatableData();
                        dt.Value = body;// contents.ToString();
                        App.DAUtil.Save<DatatableData>(dt);

                        var vList = App.DAUtil.GetAll<DatatableData>("DatatableData1");
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
    }
}