using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;

using UIKit;
using ImageCircle.Forms.Plugin.iOS;
using UserNotifications;
using System.Threading.Tasks;
using Plugin.Connectivity;
//using Plugin.LocalNotifications;

namespace DevEnvAzure.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //private const double MINIMUM_BACKGROUND_FETCH_INTERVAL = 300;

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();
            global::Xamarin.Forms.Forms.Init();

            new Syncfusion.SfAutoComplete.XForms.iOS.SfAutoCompleteRenderer();
            ImageCircleRenderer.Init();

            //if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            //{
            //    // Ask the user for permission to get notifications on iOS 10.0+
            //    UNUserNotificationCenter.Current.RequestAuthorization(
            //            UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound,
            //            (approved, error) => { });
            //}
            //else if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            //{
            //    // Ask the user for permission to get notifications on iOS 8.0+
            //    var settings = UIUserNotificationSettings.GetSettingsForTypes(
            //            UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
            //            new NSSet());

            //    UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            //}

            //UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval(MINIMUM_BACKGROUND_FETCH_INTERVAL);

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }


        public async override void PerformFetch(UIApplication application, Action<UIBackgroundFetchResult> completionHandler)
        {
            var result = UIBackgroundFetchResult.NoData;
            try
            {
                if (SPUtility.IsConnected())
                {
                    await App.SyncOfflineItemsBG();
                    result = UIBackgroundFetchResult.NewData;
                }

            }
            catch (Exception exx) // Fetch Errors are Reported via analytics system within BackgroundFetchBoxOfficeTicketResults
            {
                result = UIBackgroundFetchResult.Failed;
            }
            completionHandler(result);
        }
    }
}
