using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DevEnvAzure.Models;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace DevEnvAzure.Droid
{
    [Service]
    public class AppService : Service
    {
        static int val = 0;
        public const int SERVICE_RUNNING_NOTIFICATION_ID = 10000;
        public const int SERVICE_RUNNING_NOTIFICATION_ID1 = 123;
        public Notification notification;
        public Notification.Builder builder;
        public override IBinder OnBind(Intent intent)
        {
            // This is a started service, not a bound service, so we just return null.
            return null;
        }
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            // Code not directly related to publishing the notification has been omitted for clarity.
            // Normally, this method would hold the code to be run when the service is started.


            int i = 0;
            string val1 = string.Empty;
            string val2 = string.Empty;
            if (val == 0)
            {
                new Task(() =>
                {
                    Plugin.Connectivity.CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;

                }).Start();
            }
            builder = new Notification.Builder(this);
            builder.SetContentTitle("Nokcoot");
            builder.SetContentText("Offline drafts");
            notification = builder.Build();
            //.SetContentTitle("Nokcoot")
            ////.SetContentText("Offline drafts uploaded")
            ////.SetSmallIcon(Resource.Drawable.icon)
            ////.SetDefaults(NotificationDefaults.Sound)
            //.SetAutoCancel(true)
            //.SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
            //////  .SetContentIntent(BuildIntentToShowMainActivity())
            ////.SetOngoing(true)
            ////.AddAction(BuildRestartTimerAction())
            //// .AddAction(BuildStopServiceAction())
            //.Build();

            // Enlist this instance of the service as a foreground service
            // StartForeground(SERVICE_RUNNING_NOTIFICATION_ID, notification);
            StartForeground(SERVICE_RUNNING_NOTIFICATION_ID, notification);
            return StartCommandResult.Sticky;
        }

        private async void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            // Get the notification manager:
            NotificationManager notificationManager =
                GetSystemService(Context.NotificationService) as NotificationManager;
            Notification.Builder builder1 = new Notification.Builder(this);
            Notification notification1 = builder1.Build();


            if (e.IsConnected)
            {
                var eValue = App.DAUtil.GetAll<OfflineItem>("OfflineItem");
                if (eValue.Count > 0)
                {

                    var intent = new Intent(this, typeof(MainActivity));
                    intent.AddFlags(ActivityFlags.ClearTop);
                    var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

                    builder1.SetContentTitle("Nokcoot");
                    builder1.SetContentText("Offline drafts uploaded");
                    builder1.SetAutoCancel(true);
                    builder1.SetDefaults(NotificationDefaults.Sound);

                    builder1.SetSmallIcon(Resource.Drawable.logo);
                    builder1.SetContentIntent(pendingIntent);
                    builder1.SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis());
                    notification1 = builder1.Build();
                    notification1.Defaults |= NotificationDefaults.Vibrate;
                    await OAuthHelper.SyncOfflineItems();
                }

                MessagingCenter.Subscribe<object, string>(this, "notify", (s, e1) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (App.DAUtil.GetAll<OfflineItem>("OfflineItem").Count == 0)
                        {

                            notificationManager.Notify(SERVICE_RUNNING_NOTIFICATION_ID1, notification1);
                            StopForeground(true);
                        }
                    });
                });

                //  notification.SetDefaults(NotificationDefaults.Sound | NotificationDefaults.Vibrate);
            }
        }

    }
}