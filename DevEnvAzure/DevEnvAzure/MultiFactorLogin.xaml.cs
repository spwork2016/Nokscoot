using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DevEnvAzure
{
    public partial class MultiFactorLogin : ContentPage, INotifyPropertyChanged
    {
        public MultiFactorLogin()
        {
            InitializeComponent();

            BindingContext = this;
            IsBusy = true;
        }

        protected override async void OnAppearing()
        {
            if (SPUtility.IsConnected())
            {
                lblLoading.Text = "Authenticating...";
                await PerformLoginAsync();
            }
            else
            {
                IsBusy = false;
                lblLoading.Text = "Network not connected to internet.";
            }
        }

        private async Task PerformLoginAsync()
        {
            try
            {
                await OAuthHelper.GetAccessToken();

                btnLogin.IsVisible = false;

                App.DAUtil.RefreshMasterInfo(new MasterInfo
                {
                    content = JsonConvert.SerializeObject(App.GraphAuthentication),
                    Name = App.GRAPH_AUTH_RESULT_KEY
                });

                App.DAUtil.RefreshMasterInfo(new MasterInfo
                {
                    content = JsonConvert.SerializeObject(App.SharePointAuthentication),
                    Name = App.SHAREPOINT_AUTH_RESULT_KEY
                });

                await OAuthHelper.GetUserInfo();

                MessagingCenter.Send<object>(this, App.EVENT_LAUNCH_MAIN_PAGE);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");

                btnLogin.IsVisible = true;
                IsBusy = false;
                lblLoading.Text = ex.Message;
            }
        }

        private void BtnLogin_Clicked(object sender, EventArgs e) => OnAppearing();
    }
}
