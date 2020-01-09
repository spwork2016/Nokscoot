using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DevEnvAzure
{
    public partial class Login : ContentPage, INotifyPropertyChanged
    {
        public Login()
        {
            InitializeComponent();

            Username.Keyboard = Keyboard.Create(KeyboardFlags.None);
            Password.Keyboard = Keyboard.Create(KeyboardFlags.None);

            BindingContext = this;
            IsBusy = true;
            var userCredentials = App.DAUtil.GetMasterInfoByName("UserCredentials");
            if (SPUtility.IsConnected() && userCredentials != null)
            {
                var cred = JsonConvert.DeserializeObject<dynamic>(userCredentials.content);
                string uName = cred.Username;
                string pwd = cred.Password;

                Username.Text = uName;
                Password.Text = pwd;

                // Login_OnClicked(btnLogin, null);

            }
        }

        protected override async void OnAppearing()
        {
            await PerformLoginAsync();
        }

        private async Task PerformLoginAsync()
        {
            try
            {
                await OAuthHelper.GetAccessToken();
                IsBusy = false;
                if (App.GraphAuthentication != null)
                {
                    await Task.Run(async () =>
                    {
                        MessagingCenter.Send<object>(this, App.EVENT_LAUNCH_MAIN_PAGE);
                    });
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private async void Login_OnClicked(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(Username.Text.Trim()) || string.IsNullOrEmpty(Username.Text.Trim()))
                //{
                //    DependencyService.Get<IMessage>().LongAlert("Login Failed! Please check email/password");
                //    return;
                //}

                IsBusy = true;
                btnLogin.IsVisible = false;

                var t1 = Task.Run(async () =>
               {
                   await OAuthHelper.GetAuthenticationHeader(Username.Text, Password.Text).ContinueWith(async (x) =>
                 {
                     await OAuthHelper.GetUserInfo().ContinueWith((y) =>
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            IsBusy = false;
                            if (App.GraphAuthentication != null)
                            {
                                MessagingCenter.Send<object>(this, App.EVENT_LAUNCH_MAIN_PAGE);
                            }
                            else
                            {
                                DependencyService.Get<IMessage>().ShortAlert("Login failed! Please check email/password");
                                btnLogin.IsVisible = true;
                            }
                        });
                    });
                 });
               });

            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert("Login failed! Please check email/password");
            }
        }
    }
}
