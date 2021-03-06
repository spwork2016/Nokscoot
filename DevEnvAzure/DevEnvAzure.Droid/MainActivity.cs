﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Android.Content;
using ImageCircle.Forms.Plugin.Droid;
using Xamarin.Forms;
using Plugin.Permissions;
using Plugin.CurrentActivity;

namespace DevEnvAzure.Droid
{
    [Activity(Label = "NokScoot", Icon = "@drawable/Nokscoot", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            CrossCurrentActivity.Current.Init(this, bundle);
            
            try
            {
                Syncfusion.Licensing.SyncfusionLicenseProvider
                    .RegisterLicense("MTk5MzQzQDMxMzcyZTM0MmUzMEJ5enBKWDdrR1dSN0REeUs1YXB2TWUzQ0pPS1V6WDduY0xKYkhYQStiUm89");
                Rg.Plugins.Popup.Popup.Init(this, bundle);

                global::Xamarin.Forms.Forms.Init(this, bundle);
                ImageCircleRenderer.Init();
                LoadApplication(new App());
                //MessagingCenter.Subscribe<object>(this, "StartService", (arg) =>
                //{
                //    try
                //    {
                //        serviceStarter();
                //    }
                //    catch (Exception ex)
                //    {

                //    }
                //});
            }
            catch (Exception ex)
            {
                
            }
        }
        public void serviceStarter()
        {
            var intent = new Intent(this, typeof(AppService));

            StartService(intent);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

