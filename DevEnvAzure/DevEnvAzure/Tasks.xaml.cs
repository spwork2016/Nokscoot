using DevEnvAzure.Model;
using Newtonsoft.Json;
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
    public partial class Tasks : ContentPage
    {
        readonly string SPTasksURL = "{0}GetByTitle('Read and Sign Confirmation')/Items?$filter=PercentComplete eq 0 and AssignedToId eq {1}";
        public Tasks()
        {
            if (!OAuthHelper.IsLoggedIn()) Navigation.PushAsync(new Login());

            SPTasksURL = string.Format(SPTasksURL, ClientConfiguration.Default.SPRootURLList, App.CurrentUser.Id);
            InitializeComponent();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            IsBusy = true;
            await GetTasks();
        }

        private void taskList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Alert!", "Set the task status as completed?", "Yes", "No");
                if (result)
                {
                    IsBusy = true;
                    var item = (Result)e.SelectedItem;
                    await CompleteTheTask(item);
                }
                else
                {
                    taskList.SelectedItem = null;
                }
            });
        }

        private async Task CompleteTheTask(Result item)
        {
            var client = OAuthHelper.GetHTTPClient();

            var data = new DataContracts.TaskSp();
            data.PercentComplete = 100;
            data.__metadata.uri = item.__metadata.uri;
            data.__metadata.id = item.__metadata.id;
            data.__metadata.etag = item.__metadata.etag;
            data.__metadata.type = item.__metadata.type;

            var body = JsonConvert.SerializeObject(data, Formatting.None,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
            var contents = new StringContent(body);
            contents.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;odata=verbose");
            contents.Headers.Add("X-HTTP-Method", "MERGE");
            client.DefaultRequestHeaders.Add("If-Match", "*");

            var postResult = client.PostAsync(item.__metadata.uri, contents).Result;
            if (!postResult.IsSuccessStatusCode)
            {
                var httpErrorObject = postResult.Content.ReadAsStringAsync().Result;
            }
            else
            {
                await GetTasks();
            }

            //var body = JsonConvert.SerializeObject(data, Formatting.None,
            //        new JsonSerializerSettings
            //        {
            //            NullValueHandling = NullValueHandling.Ignore
            //        });
            //var contents = new System.Net.Http.StringContent(body);
            //contents.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;odata=verbose");

            //var response = await client.PostAsync(SPTasksURL, contents);
        }

        private async Task GetTasks()
        {
            try
            {
                var client = OAuthHelper.GetHTTPClient();
                var response = await client.GetStringAsync(SPTasksURL);
                if (response != null)
                {

                    var spData = JsonConvert.DeserializeObject<SPData>(response, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
                    if (spData != null)
                    {
                        taskList.ItemsSource = spData.d.results;
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            IsBusy = false;
                        });
                    }
                }
            }
            catch (Exception)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsBusy = false;
                });
            }

        }
    }
}