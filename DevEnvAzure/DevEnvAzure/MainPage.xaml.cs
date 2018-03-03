﻿using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //auth();
        }

        public async void  auth()
        {
            AuthenticationResult data = await DependencyService.Get<IAuthenticator>()
                                  .ReAuthenticate(App.tenanturl, App.GraphResourceUri, App.ApplicationID, App.ReturnUri);
            if (data != null)
            {
                //App.AuthenticationResponse = data;
                // Messgage.Text = App.username = data.UserInfo.GivenName + " " + data.UserInfo.FamilyName;
            }
           
        }
    }
}