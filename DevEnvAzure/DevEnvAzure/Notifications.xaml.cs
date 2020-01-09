using DevEnvAzure.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Notifications : ContentPage
    {
        private bool isRowEven;

        public List<NotificationItem> NotificationSource { get; set; }
        public Notifications()
        {
            InitializeComponent();
            lstNotifications.Refreshing += LstNotifications_Refreshing;
            LoadNotifications();
        }

        private async void LstNotifications_Refreshing(object sender, EventArgs e)
        {
            await LoadNotifications();
        }

        private string StripHtml(string source)
        {
            string output;

            //get rid of HTML tags
            output = Regex.Replace(source, "<[^>]*>", string.Empty);

            //get rid of multiple blank lines
            output = Regex.Replace(output, @"^\s*$\n", string.Empty, RegexOptions.Multiline);

            return output;
        }

        public async Task LoadNotifications()
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsBusy = true;
                });
                List<NotificationItem> items = new List<NotificationItem>();
                var client = await OAuthHelper.GetHTTPClientAsync();
                var result = await client.GetStringAsync(string.Format(ClientConfiguration.Default.SPListURL, "announcements"));
                if (result != null)
                {
                    var spData = JsonConvert.DeserializeObject<SPData>(result, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
                    int id = 1;
                    foreach (var val in spData.d.results)
                    {
                        items.Add(new NotificationItem { Id = id, Modified = val.Modified, Title = val.Title, Body = val.Body, IsBodyVisible = false });
                        id++;
                    }

                    NotificationSource = items;
                    lstNotifications.ItemsSource = items;

                    stkNodata.IsVisible = items.Count == 0;
                    stkCtn.IsVisible = items.Count != 0;

                    IsBusy = false;
                }
            }
            catch (Exception)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsBusy = false;
                });
                lstNotifications.EndRefresh();

                DependencyService.Get<IMessage>().ShortAlert("Unable to retrive notifications.");
            }
        }

        public async void lstNotifications_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            NotificationItem selectedItem = (NotificationItem)e.Item;
            int index = NotificationSource.FindIndex((x) =>
            {
                return x.Id == selectedItem.Id;
            });


            string strippedString = StripHtml(selectedItem.Body);
            await DisplayAlert(selectedItem.Title, strippedString, "Close");


            //NotificationSource[index].IsBodyVisible = !NotificationSource[index].IsBodyVisible;
            //lstNotifications.BeginRefresh();
            //lstNotifications.ItemsSource = NotificationSource;
            //lstNotifications.EndRefresh();
        }

        private void ViewCell_Appearing(object sender, EventArgs e)
        {
            if (this.isRowEven)
            {
                var viewCell = (ViewCell)sender;
                if (viewCell.View != null)
                {
                    viewCell.View.BackgroundColor = Color.FromHex("FDCE02");
                }
            }

            this.isRowEven = !this.isRowEven;
        }
    }

    public class NotificationItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsBodyVisible { get; set; }
        public DateTime Modified { get; set; }
    }
}