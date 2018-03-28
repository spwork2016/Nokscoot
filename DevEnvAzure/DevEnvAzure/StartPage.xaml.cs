using DevEnvAzure.Model;
using DevEnvAzure.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : MasterDetailPage
    {
        public List<MasterPageItem> menuList { get; set; }
        public string UserTitle { get; set; }

        public StartPage()
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
            var StationInformationPage = new MasterPageItem() { Title = "Station Information", Icon = "stationinfo.png", TargetType = typeof(StationInformation) };
            var editableDraftsPage = new MasterPageItem() { Title = "Editable Drafts", Icon = "editabledrafts.png", TargetType = typeof(EditableDrafts) };
            var notificationsPage = new MasterPageItem() { Title = "Notifications", Icon = "notifications.png" };

            menuList.Add(homePage);
            menuList.Add(reportingPage);

            menuList.Add(notificationsPage);
            menuList.Add(tasksPage);
            menuList.Add(docsPage);
            menuList.Add(StationInformationPage);

            menuList.Add(new MasterPageItem() { Title = "One Note", Icon = "onenote.png" });
            menuList.Add(new MasterPageItem() { Title = "One Drive", Icon = "drive.png" });
            menuList.Add(new MasterPageItem() { Title = "Work Day", Icon = "workday.png" });
            menuList.Add(new MasterPageItem() { Title = "SABA", Icon = "saba.png" });

            menuList.Add(editableDraftsPage);
            menuList.Add(offDrafts);
            menuList.Add(logout);

            // Setting our list to be ItemSource for ListView in MainPage.xaml
            navigationDrawerList.ItemsSource = menuList;

            // Initial navigation, this can be used for our home page
            Detail = new NavigationPage((Page)Activator.CreateInstance(App.AuthenticationResponse != null ? typeof(MainPage) : typeof(Login)));


        }

        protected override async void OnAppearing()
        {
            if (App.AuthenticationResponse != null)
            {
                var users = await SPUtility.GetUsersForPicker();
                App.peoplePickerDataSource = new List<PeoplePicker>(users);
            }

            loggrInUser.Text = App.CurrentUser?.Name;
            if (App.CurrentUser != null && App.CurrentUser.PictureBytes != null)
                profilePic.Source = ImageSource.FromStream(() => new MemoryStream(App.CurrentUser?.PictureBytes));
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;

            if (item.Title == "Logout")
            {
                IsPresented = false;
                DependencyService.Get<IMessage>().LongAlert("Logged out successfully.");
                App.AuthenticationResponse = null;
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Login)));

                //Navigation.PushModalAsync(new Login());
                return;
            }

            if (!OAuthHelper.IsLoggedIn())
            {
                Navigation.PushModalAsync(new Login());
                return;
            }

            if (item.SubMenuDataSource != null)
            {
                //   item.IsSubMenuVisible = !item.IsSubMenuVisible;
            }
            else if (item.TargetType != null)
            {
                IsPresented = false;
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
            }
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
