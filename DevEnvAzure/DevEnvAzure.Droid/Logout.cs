using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Android.Webkit;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

[assembly: Dependency(typeof(DevEnvAzure.Droid.Logout))]
namespace DevEnvAzure.Droid
{
    class Logout : ILogout
    {
        public Logout() { }
        public bool LogoutfromAuth()
        {
            try
            {
                //if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
                //    Android.Webkit.CookieManager.Instance.RemoveAllCookies(null);
                //else
                //    Android.Webkit.CookieManager.Instance.RemoveAllCookie();
                // App.AuthResult..Clear();
                //  App.authcontext.TokenCache.DeleteItem()

                if (App.AuthResult != null)
                {
                    AuthenticationContext ac = new AuthenticationContext(ClientConfiguration.NokScoot.ActiveDirectoryTenant);
                    ac.TokenCache.Clear();
                }

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

    }
}