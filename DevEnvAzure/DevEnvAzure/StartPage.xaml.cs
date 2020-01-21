using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// do not remove
using ImageCircle.Forms.Plugin;
using System.IO;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : MasterDetailPage
    {
        public List<MasterPageItem> menuList { get; set; }
        public string UserTitle { get; set; }

        public StartPage()
        {
            try
            {
                InitializeComponent();
                menuList = new List<MasterPageItem>();

                // Creating our pages for menu navigation
                // Here you can define title for item, 
                // icon on the left side, and page that you want to open after selection
                var homePage = new MasterPageItem() { Title = "Home", Icon = "home.png", TargetType = typeof(MainPage) };
                var offDrafts = new MasterPageItem() { Title = "Offline Drafts", Icon = "offlinedrafts.png", TargetType = typeof(DraftsPage) };
                var logout = new MasterPageItem() { Title = "Logout", Icon = "logout.png" };
                var reportingPage = new MasterPageItem() { Title = "Reporting", Icon = "reporting.png", TargetType = typeof(ReportsPage) };
                var tasksPage = new MasterPageItem() { Title = "My Tasks", Icon = "mytasks.png", TargetType = typeof(Tasks) };
                var docsPage = new MasterPageItem() { Title = "Documents", Icon = "documents.png", TargetType = typeof(DocumentLibrary) };
                var StationInformationPage = new MasterPageItem() { Title = "Station Information", Icon = "stationinfo.png", TargetType = typeof(ViewStationInformation) };
                var editableDraftsPage = new MasterPageItem() { Title = "Editable Drafts", Icon = "editabledrafts.png", TargetType = typeof(EditableDrafts) };
                var notificationsPage = new MasterPageItem() { Title = "Notifications", Icon = "notifications.png", TargetType = typeof(Notifications) };

                menuList.Add(homePage);
                menuList.Add(reportingPage);

                menuList.Add(notificationsPage);
                menuList.Add(tasksPage);
                menuList.Add(docsPage);
                menuList.Add(StationInformationPage);

                menuList.Add(new MasterPageItem() { Title = "Work Day", Icon = "workday.png" });
                menuList.Add(new MasterPageItem() { Title = "SABA", Icon = "saba.png" });

                menuList.Add(editableDraftsPage);
                menuList.Add(offDrafts);
                menuList.Add(logout);

                // Setting our list to be ItemSource for ListView in MainPage.xaml
                navigationDrawerList.ItemsSource = menuList;

                // Initial navigation, this can be used for our home page
                SetNavigationPage(new MainPage());
            }
            catch (Exception ex)
            {

            }
        }

        protected override async void OnAppearing()
        {
            try
            {
                if (App.CurrentUser != null)
                {
                    loggrInUser.Text = App.CurrentUser?.Name;
                    if (App.CurrentUser?.PictureBytes != null)
                        profilePic.Source = ImageSource.FromStream(() => new MemoryStream(App.CurrentUser?.PictureBytes));
                }
                else
                {
                    MessagingCenter.Subscribe<App>(this, "userInfo", (arg) =>
                    {
                        loggrInUser.Text = App.CurrentUser?.Name;
                        if (App.CurrentUser?.PictureBytes != null)
                            profilePic.Source = ImageSource.FromStream(() => new MemoryStream(App.CurrentUser?.PictureBytes));
                    });
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void NavigationSubscribers()
        {
            MessagingCenter.Subscribe<StationInformation>(this, "home", (arg) =>
            {
                IsPresented = false;
                try
                {
                    SetNavigationPage(new MainPage());
                }
                catch (Exception ex)
                {

                }
            });

            MessagingCenter.Subscribe<KaizenReport>(this, "home", (arg) =>
            {
                IsPresented = false;
                try
                {
                    SetNavigationPage(new MainPage());
                }
                catch (Exception ex)
                {

                }
            });

            MessagingCenter.Subscribe<SSIRShortForm>(this, "home", (arg) =>
            {
                IsPresented = false;
                try
                {
                    SetNavigationPage(new MainPage());
                }
                catch (Exception ex)
                {

                }
            });

            MessagingCenter.Subscribe<FlightCrewVoyageRecord>(this, "home", (arg) =>
            {
                IsPresented = false;
                try
                {
                    SetNavigationPage(new MainPage());
                }
                catch (Exception ex)
                {

                }
            });

        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var item = (MasterPageItem)e.SelectedItem;
                ((ListView)sender).SelectedItem = null;

                NavigationSubscribers();

                if (item == null) return;

                if (item.Title == "SABA")
                {
                    Launcher.OpenAsync(new Uri("https://wd3.myworkday.com/flyscoot/d/inst/1$9232/6503$3.htmld"));
                }
                else if (item.Title == "Work Day")
                {
                    Launcher.OpenAsync(new Uri("https://wd3.myworkday.com/wday/authgwy/flyscoot/login.htmld?returnTo=%2fflyscoot%2fd%2finst%2f779%24610920%2frel-task%2f2997%244086.htmld"));
                }

                if (item.Title == "Logout")
                {
                    IsPresented = false;

                    AuthenticationContext ac = new AuthenticationContext(ClientConfiguration.NokScoot.ActiveDirectoryTenant);
                    ac.TokenCache.Clear();

                    App.GraphAuthentication = null;
                    App.CurrentUser = null;
                    App.SharePointAuthentication = null;

                    App.DAUtil.DeleteMasterInfo(App.GRAPH_AUTH_RESULT_KEY);
                    App.DAUtil.DeleteMasterInfo(App.SHAREPOINT_AUTH_RESULT_KEY);
                    App.DAUtil.DeleteMasterInfo(App.USER_INFO_KEY);

                    DependencyService.Get<IMessage>().ShortAlert("Logged out successfully.");

                    DependencyService.Get<IAuthenticator>().Logout();

                    MessagingCenter.Send<object>(this, App.EVENT_LAUNCH_MULTIFACTOR_PAGE);

                    return;
                }

                if (!OAuthHelper.IsLoggedIn())
                {
                    MessagingCenter.Send<object>(this, App.EVENT_LAUNCH_MULTIFACTOR_PAGE);
                    return;
                }

                IsPresented = false;
                if (item.Title == "Station Information")
                {
                    SetNavigationPage(new ViewStationInformation());
                }
                else if (item.Title == "Offline Drafts")
                {
                    SetNavigationPage(new DraftsPage());
                }
                else if (item.Title == "Reporting")
                {
                    SetNavigationPage(new ReportsPage());
                }
                else if (item.Title == "My Tasks")
                {
                    SetNavigationPage(new Tasks());
                }
                else if (item.Title == "Documents")
                {
                    SetNavigationPage(new DocumentLibrary());
                }
                else if (item.Title == "Editable Drafts")
                {
                    SetNavigationPage(new EditableDrafts());
                }
                else if (item.Title == "Notifications")
                {
                    SetNavigationPage(new Notifications());
                }
                else if (item.TargetType != null)
                {
                    SetNavigationPage((Page)Activator.CreateInstance(item.TargetType));
                }

            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    DependencyService.Get<IMessage>().ShortAlert(string.Format("Error: {0}", ex.Message));
                });
            }
        }

        public void SetNavigationPage(Page page)
        {
            var nav = new NavigationPage(page);
            nav.BarBackgroundColor = Color.FromHex("FDCE02");
            nav.BarTextColor = Color.Black;

            Detail = nav;
        }
    }


    public class MasterPageItem
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public Type TargetType { get; set; }
        public bool IsSubMenuVisible { get; set; }
        public List<MasterPageItem> SubMenuDataSource { get; set; }
    }
}
