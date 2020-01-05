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
            await PerformLoginAsync();
        }

        private async Task PerformLoginAsync()
        {
            try
            {
                var auth = DependencyService.Get<IAuthenticator>();
                var nokScootConfig = ClientConfiguration.NokScoot;

                if (App.AuthResult == null)
                {
                    await OAuthHelper.GetAccessToken();
                    await OAuthHelper.GetUserInfo();
                    MessagingCenter.Send<object>(this, App.EVENT_LAUNCH_MAIN_PAGE);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }
    }
}
