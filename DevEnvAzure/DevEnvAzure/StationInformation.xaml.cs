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
        }
        private void Save_clicked(object sender, XLabs.EventArgs<bool> e)
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
            CreateItems(jsonInitObj.getStationInformationJson(_StationInformation));
        }
        private void savedrafts_btn_Clicked(object sender, EventArgs e)
        {
            _StationInformation.ReportType = "Station Information" + _StationInformation.Id.ToString();
            _StationInformation.DateOfEvent = DateTime.Now;
            _StationInformation.Created = DateTime.Now;

            _StationInformation = App.DAUtil.SaveOrUpdate(_StationInformation);

            DependencyService.Get<IMessage>().ShortAlert("Item drafted");
        }
        private bool CheckConnection()
        {
            return CrossConnectivity.Current.IsConnected;
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
                            await DisplayAlert("Sharepoint error", ex, "Ok");
                        }
                        ToggleBusy(false);
                    }
                    else
                    {
                        OfflineItem dt = new OfflineItem();
                        dt.Created = DateTime.Now;
                        dt.ReportType = (int)SPUtility.ReportType.SationInfo;
                        dt.Value = body;// contents.ToString();
                        App.DAUtil.Save<OfflineItem>(dt);

                        var vList = App.DAUtil.GetAll<OfflineItem>("OfflineItem");
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