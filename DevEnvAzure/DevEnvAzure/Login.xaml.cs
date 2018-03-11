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

            //REMOVE - only dev
            Username.Text = ClientConfiguration.Default.UserName;
            Password.Text = ClientConfiguration.Default.Password;

            BindingContext = this;
        }

        private async void Login_OnClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Username.Text.Trim()) || string.IsNullOrEmpty(Username.Text.Trim()))
                {
                    DependencyService.Get<IMessage>().LongAlert("Login Failed! Please check email/passowrd");
                    return;
                }

                IsBusy = true;
                btnLogin.IsVisible = false;

                var t1 = Task.Run(async () =>
               {
                   await OAuthHelper.GetAuthenticationHeader(Username.Text, Password.Text).ContinueWith(async (x) =>
                 {
                     await OAuthHelper.GetUserInfo(Username.Text, Password.Text).ContinueWith((y) =>
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            if (App.AuthenticationResponse != null)
                            {
                                await this.Navigation.PushModalAsync(new StartPage());
                            }
                            else
                            {
                                DependencyService.Get<IMessage>().LongAlert("Login Failed! Please check email/password");

                                btnLogin.IsVisible = true;
                                IsBusy = false;
                            }
                        });
                    });
                 });
               });

            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().LongAlert("Login Failed! Please check email/passowrd");
            }
        }
    }
}
