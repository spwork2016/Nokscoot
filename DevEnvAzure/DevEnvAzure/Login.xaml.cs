using Newtonsoft.Json;
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

            Username.Keyboard = Keyboard.Create(KeyboardFlags.None);
            Password.Keyboard = Keyboard.Create(KeyboardFlags.None);

            BindingContext = this;

            var userCredentials = App.DAUtil.GetMasterInfoByName("UserCredentials");
            if (userCredentials != null)
            {
                var cred = JsonConvert.DeserializeObject<dynamic>(userCredentials.content);
                string uName = cred.Username;
                string pwd = cred.Password;

                Username.Text = uName;
                Password.Text = pwd;

                Login_OnClicked(btnLogin, null);
            }
        }

        private async void Login_OnClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Username.Text.Trim()) || string.IsNullOrEmpty(Username.Text.Trim()))
                {
                    DependencyService.Get<IMessage>().LongAlert("Login Failed! Please check email/password");
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
                                MessagingCenter.Send<object>(this, App.EVENT_LAUNCH_MAIN_PAGE);
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
                DependencyService.Get<IMessage>().LongAlert("Login Failed! Please check email/password");
            }
        }
    }
}
