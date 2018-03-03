using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DevEnvAzure
{
    public partial class Login : ContentPage, INotifyPropertyChanged
    {
        public Login()
        {
            InitializeComponent();
            //this.AddIndicator();

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

                ActivityIndicator spinner = this.FindByName<ActivityIndicator>("activityIndicator");

                spinner.IsVisible = true;
                spinner.IsRunning = true;
                btnLogin.IsVisible = false;

                var t1 = Task.Run(async () =>
               {
                   await OAuthHelper.GetAuthenticationHeader(Username.Text, Password.Text).ContinueWith((x) =>
                  {
                      Device.BeginInvokeOnMainThread(() =>
                      {
                          OAuthHelper.GetUserInfo();
                          if (App.AuthenticationResponse != null)
                          {
                              this.Navigation.PushModalAsync(new StartPage());
                          }
                          else
                          {
                              DependencyService.Get<IMessage>().LongAlert("Login Failed! Please check email/password");

                              btnLogin.IsVisible = true;
                              spinner.IsVisible = false;
                              spinner.IsRunning = false;
                          }
                      });

                  });
               });

            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().LongAlert("Login Failed! Please check email/passowrd");
            }
        }

        //public async Task<void> NavigateTopage()
        //{
        //    try
        //    {
        //        await Navigation.PushModalAsync(new StartPage());
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
    }
}
