using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DevEnvAzure.Model;
using DevEnvAzure.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms.Internals;

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

            var stations = await SPUtility.GetStations();
            iataPicker.ItemsSource = stations.Select(x => x.Value).ToList();

            ToggleBusy(false);
        }

        private async void iataPicker_Changed(object sender, EventArgs e)
        {
            ToggleBusy(true);

            var SelectedValue = iataPicker.Items.ElementAt(iataPicker.SelectedIndex);
            await BindStationInfoByIATA(SelectedValue);

            ToggleBusy(false);
        }

        private async Task BindStationInfoByIATA(string IATACode)
        {
            var client = await OAuthHelper.GetHTTPClient();
            if (client == null) return;

            string url = ClientConfiguration.Default.ActiveDirectoryResource + string.Format("SSQServices/_api/Web/lists/GetByTitle('(Ops) Line Station Information')/items?$select=IATA_x0020_Code,Title,City_x0020_Name,Id,Country,Airport_x0020_Type&$filter=IATA_x0020_Code eq '{0}'&Status eq '{1}'", IATACode, "Open");
            var response = await client.GetStringAsync(url);
            if (response != null)
            {
                var spData = JsonConvert.DeserializeObject<SPData>(response,
                             new JsonSerializerSettings { DateParseHandling = DateParseHandling.None, NullValueHandling = NullValueHandling.Ignore });
                lstStationInfo.ItemsSource = spData.d.results;
                lstStationInfo.HeightRequest = 80 * spData.d.results.Count;
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