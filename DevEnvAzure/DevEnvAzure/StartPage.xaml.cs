using DevEnvAzure.Model;
using System;
using System.Collections.Generic;
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
        public string profilePicUrl { get; set; }

        public StartPage()
        {
            InitializeComponent();
            menuList = new List<MasterPageItem>();

            // Creating our pages for menu navigation
            // Here you can define title for item, 
            // icon on the left side, and page that you want to open after selection
            //var page1 = new MasterPageItem() { Title = "Login", Icon = "LoginIco.png", TargetType = typeof(Login) };
            var page2 = new MasterPageItem() { Title = "Employee Information", Icon = "EMP_info.png", TargetType = typeof(HomePage) };
            var page3 = new MasterPageItem() { Title = "Employee Drafts", Icon = "EMP_info.png", TargetType = typeof(DraftsPage) };
            var logout = new MasterPageItem() { Title = "Logout", Icon = "logout.png" };
            var page4 = new MasterPageItem() { Title = "Safety", Icon = "EMP_info.png", TargetType = typeof(ReportsPage) };
            var editableDraftsPage = new MasterPageItem() { Title = "Editable Drafts", Icon = "EMP_info.png", TargetType = typeof(EditableDrafts) };
            menuList.Add(page2);
            menuList.Add(page4);
            menuList.Add(page3);
            menuList.Add(editableDraftsPage);
            menuList.Add(logout);

            // Setting our list to be ItemSource for ListView in MainPage.xaml
            navigationDrawerList.ItemsSource = menuList;

            // Initial navigation, this can be used for our home page
            Detail = new NavigationPage((Page)Activator.CreateInstance(App.AuthenticationResponse != null ? typeof(MainPage) : typeof(Login)));
            //    Detail = new NavigationPage(new Login());
            //    IsPresented = false;

            profilePicUrl = "LoginIco.png";
        }

        protected override async void OnAppearing()
        {
            if (App.CurrentUser == null)
            {
                await OAuthHelper.GetUserInfo();
                //profilePicUrl = App.CurrentUser.PictureUrl;
                //profilePic.Source = new UriImageSource
                //{
                //    Uri = new Uri(App.CurrentUser.PictureUrl),
                //    CachingEnabled = true,
                //    CacheValidity = new TimeSpan(5, 0, 0, 0)
                //};

            }

            loggrInUser.Text = App.CurrentUser?.Name;
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
            IsPresented = false;

            if (item.Title == "Logout")
            {
                DependencyService.Get<IMessage>().LongAlert("Logged out successfully.");
                App.AuthenticationResponse = null;
                Navigation.PushModalAsync(new Login());
                return;
            }

            if (!OAuthHelper.IsLoggedIn())
            {
                Navigation.PushModalAsync(new Login());
                return;
            }

            Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
        }

        //void Login_clicked(object sender, System.EventArgs e)
        //{
        //    Detail = new NavigationPage(new Login());
        //    IsPresented = false;
        //}
        // void HomePage_Clicked(object sender, System.EventArgs e)
        //{
        //    Detail =  new NavigationPage(new HomePage(App.username));
        //    IsPresented = false;
        //}
    }

    public class MasterPageItem
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public Type TargetType { get; set; }
    }
}
