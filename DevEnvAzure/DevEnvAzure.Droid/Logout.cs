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
                App.authcontext.TokenCache.Clear();
              //  App.authcontext.TokenCache.DeleteItem()
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

    }
}