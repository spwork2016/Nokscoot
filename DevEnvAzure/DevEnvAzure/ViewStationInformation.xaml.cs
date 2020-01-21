using DevEnvAzure.Model;
using DevEnvAzure.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewStationInformation : ContentPage
    {
        public ViewStationInformation()
        {
            InitializeComponent();
            BindIATAPicker();
        }

        private async void BindIATAPicker()
        {
            MasterInfo mInfo = App.DAUtil.GetMasterInfoByName("Stations");

            if (mInfo != null)
            {
                var stationsOffline = JsonConvert.DeserializeObject<SPData>(mInfo.content);
                iataPicker.ItemsSource = stationsOffline.d.results.Select(x => x.IATA_x0020_Code).ToList();
            }

            ToggleBusy(true);

            var stations = await SPUtility.GetStations(true);
            iataPicker.ItemsSource = stations.Select(x => x.Value).ToList();

            ToggleBusy(false);
        }

        private async void iataPicker_Changed(object sender, EventArgs e)
        {
            ToggleBusy(true);

            var SelectedValue = iataPicker.Items.ElementAt(iataPicker.SelectedIndex);
            if (!SPUtility.IsConnected())
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    DependencyService.Get<IMessage>().ShortAlert("No active internet connection was found!");
                });

                ToggleBusy(false);
                return;
            }

            await BindStationInfoByIATA(SelectedValue);

            ToggleBusy(false);
        }

        private async Task BindStationInfoByIATA(string IATACode)
        {
            var client = await OAuthHelper.GetHTTPClientAsync();
            if (client == null) return;

            string url = ClientConfiguration.Default.ActiveDirectoryResource + string.Format("SSQServices/_api/Web/lists/GetByTitle('(Ops) Line Station Information')/items?$select=IATA_x0020_Code,Title,City_x0020_Name,Id,Country,Airport_x0020_Type&$filter=IATA_x0020_Code eq '{0}'&Status eq '{1}'", IATACode, "Open");

            try
            {
                var response = await client.GetStringAsync(url);
                if (response != null)
                {
                    var spData = JsonConvert.DeserializeObject<SPData>(response,
                                 new JsonSerializerSettings { DateParseHandling = DateParseHandling.None, NullValueHandling = NullValueHandling.Ignore });

                    if (spData.d.results.Count > 0)
                    {
                        int id = Convert.ToInt32(spData.d.results[0].Id);
                        DataContracts.StationInformationSp sInfo = await SPUtility.GetStationInfoItem(id);

                        ToggleBusy(false);
                        await Navigation.PushAsync(new StationInformation(sInfo.GetModel()));
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void lstStationInfo_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            System.Reflection.PropertyInfo pi = e.Item.GetType().GetProperty("Id");
            if (pi != null)
            {
                ToggleBusy(true);

                int id = (int)(pi.GetValue(e.Item, null));
                DataContracts.StationInformationSp sInfo = await SPUtility.GetStationInfoItem(id);

                ToggleBusy(false);
                await Navigation.PushAsync(new StationInformation(sInfo.GetModel()));
            }
        }

        private void ToggleBusy(bool flag)
        {
            activityStack.IsVisible = flag;
            activityIndicator.IsRunning = flag;
        }
    }
}