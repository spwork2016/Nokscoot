using DevEnvAzure.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static DevEnvAzure.SPUtility;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DraftsPage : ContentPage
    {
        public class ReportItem
        {
            public string ReportName { get; set; }
            public OfflineItem Item { get; set; }
            public DateTime Created { get; set; }
            public bool HasError { get; set; }
            public string Error { get; set; }
        }

        public DraftsPage()
        {
            InitializeComponent();
            load_saveddrafts();
            BindingContext = this;
        }

        public void load_saveddrafts()
        {
            try
            {
                var d = App.DAUtil.GetAll<OfflineItem>("OfflineItem");
                App.offlineItems = new ObservableCollection<OfflineItem>(d);

                var dataSource = new List<ReportItem>();
                foreach (var item in d)
                {
                    string rName = "";
                    switch ((ReportType)item.ReportType)
                    {
                        case ReportType.CabinSafety:
                            rName = "Cabin Safety";
                            break;
                        case ReportType.Fatigue:
                            rName = "Fatigue";
                            break;
                        case ReportType.FlighCrewVoyage:
                            rName = "Flight Crew Voyage";
                            break;
                        case ReportType.FlightSafety:
                            rName = "Flight Safety";
                            break;
                        case ReportType.GroundSafety:
                            rName = "Ground Safety";
                            break;
                        case ReportType.InjuryIllness:
                            rName = "Injury/Illness";
                            break;
                        case ReportType.Security:
                            rName = "Security";
                            break;
                        case ReportType.SationInfo:
                            rName = "Station Information";
                            break;
                        case ReportType.Kaizen:
                            rName = "Kaizen";
                            break;
                        default:
                            rName = "Offline Item";
                            break;
                    }

                    dataSource.Add(new ReportItem
                    {
                        Item = item,
                        ReportName = rName,
                        Created = item.Created,
                        HasError = string.IsNullOrEmpty(item.Error) ? false : true,
                        Error = item.Error
                    });
                }
               
                lstOfflineItems.ItemsSource = dataSource;

                if (dataSource.Count == 0)
                {
                    lstOfflineItems.IsVisible = false;
                    stkNodata.IsVisible = true;
                }
                else
                {
                    lstOfflineItems.IsVisible = true;
                    stkNodata.IsVisible = false;
                }
            }
            catch (Exception ex)
            {

            }
        }
        private async void Details_OnClicked(object sender, EventArgs e)
        {
            try
            {
                var item = (Button)sender;
                await Navigation.PushAsync(new DraftsContentPage(item));
            }
            catch (Exception ex)
            {
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                // var item = (Button)sender; 
                // await Navigation.PushAsync(new SafetyReport());
            }
            catch (Exception ex)
            {
            }
        }

        private async void btnError_Clicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            var item = (ReportItem)btn.CommandParameter;
            if (!item.HasError) return;

            await DisplayAlert("", item.Error, "Ok");
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var cp = (ReportItem)((MenuItem)sender).CommandParameter;
            if (cp == null) return;

            App.DAUtil.Delete(cp.Item);

            load_saveddrafts();
        }
    }
}
