using DevEnvAzure.Models;
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

            btnLogin.Clicked += BtnLogin_Clicked;
        }

        protected void BtnLogin_Clicked(object sender, EventArgs e)
        {
            OnAppearing();
        }

        protected override async void OnAppearing()
        {
            if (SPUtility.IsConnected())
            {
                lblLoading.Text = "Authenticating...";
                btnLogin.IsVisible = false;
                await PerformLoginAsync();
            }
            else
            {
                IsBusy = false;
                btnLogin.IsVisible = true;
                lblLoading.Text = "Network not connected to internet.";
            }
        }

        private async Task PerformLoginAsync()
        {
            try
            {
                await OAuthHelper.GetAccessToken();

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
                btnLogin.IsVisible = true;
                IsBusy = false;
                lblLoading.Text = ex.Message;
            }
        }

    }
}
