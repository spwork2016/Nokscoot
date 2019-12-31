using DevEnvAzure.Controls;
using DevEnvAzure.Model;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tasks : ContentPage
    {
        readonly string SPTasksURL = "{0}SSQServices/DMS/_api/web/lists/GetByTitle('Read and Sign Confirmation')/Items?$filter=PercentComplete eq 0";
        private bool enableMultiSelect;
        private bool EmptyData;
        public Tasks()
        {
            SPTasksURL = string.Format(SPTasksURL, ClientConfiguration.Default.ActiveDirectoryResource);
            InitializeComponent();

            enableMultiSelect = true;
            Items = new SelectableObservableCollection<Result>(new List<Result>());
            //AddItemCommand = new Command(OnAddItem);
            RemoveSelectedCommand = new Command(OnRemoveSelected);
            ToggleSelectionCommand = new Command(OnToggleSelection);

            BindingContext = this;
        }

        public bool EnableMultiSelect
        {
            get { return enableMultiSelect; }
            set
            {
                enableMultiSelect = value;
                OnPropertyChanged();
            }
        }

        public SelectableObservableCollection<Result> Items { get; }

        public ICommand AddItemCommand { get; }

        public ICommand RemoveSelectedCommand { get; }

        public ICommand ToggleSelectionCommand { get; }

        private void OnAddItem()
        {
            // Items.Add(Guid.NewGuid().ToString());
        }

        private void OnRemoveSelected()
        {
            var selectedItems = Items.SelectedItems.ToArray();
            foreach (var item in selectedItems)
            {
                Items.Remove(item);
            }
        }

        private void OnToggleSelection()
        {
            foreach (var item in Items)
            {
                item.IsSelected = !item.IsSelected;
            }
        }

        private async Task DataBind()
        {
            if (!SPUtility.IsConnected()) return;

            Items.Clear();

            IsBusy = true;
            var data = await GetTasks();
            IsBusy = false;
            if (data != null)
            {
                foreach (var item in data)
                {
                    Items.Add(item);
                }

                ToggleVisibility();
            }
            else
            {

            }
        }

        protected override async void OnAppearing()
        {
            await DataBind();
        }

        private void ToggleVisibility()
        {
            EmptyData = Items.Count == 0;
            tBarItemBackBtn.Text = EmptyData ? "" : "Mark as Complete";
            lblNoData.IsVisible = EmptyData;
            lstTasks.IsVisible = !EmptyData;
        }

        private async Task CompleteTheTask(Result item)
        {
            IsBusy = true;
            var client = await OAuthHelper.GetHTTPClient();

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

            var postResult = await client.PostAsync(item.__metadata.uri, contents);
            if (!postResult.IsSuccessStatusCode)
            {
                var httpErrorObject = postResult.Content.ReadAsStringAsync().Result;
            }
            else
            {
                IsBusy = false;
                Items.Remove(item);
                ToggleVisibility();
            }
        }

        private async Task<IList<Result>> GetTasks()
        {
            try
            {
                var client = await OAuthHelper.GetHTTPClient();
                var response = await client.GetStringAsync(SPTasksURL);
                if (response != null)
                {

                    var spData = JsonConvert.DeserializeObject<SPData>(response, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
                    if (spData != null)
                    {
                        return spData.d.results;
                    }
                }
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    DependencyService.Get<IMessage>().ShortAlert(ex.Message);
                    IsBusy = false;
                });
            }

            return null;
        }

        private async void tBarItemBackBtn_Activated(object sender, EventArgs e)
        {
            var lst = Items.SelectedItems.ToArray();
            if (lst.Length == 0)
            {
                DependencyService.Get<IMessage>().ShortAlert("No tasks were selected");
                return;
            }

            IsBusy = true;
            foreach (Result item in lst)
            {
                await CompleteTheTask(item);
            }

        }

        private bool isRowEven;
        private void SelectableViewCell_Appearing(object sender, EventArgs e)
        {
            var viewCell = (ViewCell)sender;
            if (viewCell.View != null)
            {
                viewCell.View.BackgroundColor = Xamarin.Forms.Color.FromHex(isRowEven ? "#FDCE02" : "#FFFFF");
            }

            this.isRowEven = !this.isRowEven;
        }
    }
}