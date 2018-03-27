using System;
using DevEnvAzure.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace DevEnvAzure
{
    public partial class MultiSelectMenuPage1 : PopupPage
    {
        public static string approachvalue;
        public MultiSelectMenuPage1()
        {
            InitializeComponent();
        }

        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }
        private async void OnSave(object sender, EventArgs e)
        {
            approachvalue = "";
            if (autolblws.IsToggled)
            {
                approachvalue += autolbl.Text + ",";
            }
            if (ilslblsw.IsToggled)
            {
                approachvalue += ilslbl.Text + ",";
            }
            if (autolblws.IsToggled)
            {
                approachvalue += autolbl.Text + ",";
            }
            if (precisw.IsToggled)
            {
                approachvalue += precilbl.Text + ",";
            }
            if (vclblsw.IsToggled)
            {
                approachvalue +=  vclbl.Text + ",";
            }
            if (rnplblsw.IsToggled)
            {
                approachvalue +=  rnplbl.Text + ",";
            }
            if (rnpapvlblsw.IsToggled)
            {
                approachvalue += rnpapvlbl.Text ;
            }
            MessagingCenter.Send<MultiSelectMenuPage1,string>(this, "Hi",approachvalue);
            await PopupNavigation.PopAsync();

        }
    }
}













//using Xamarin.Forms;
//using Xamarin.Forms.Xaml;
//using Rg.Plugins.Popup.Pages;
//using Rg.Plugins.Popup.Services;
//namespace DevEnvAzure
//{
//    [XamlCompilation(XamlCompilationOptions.Compile)]
//    public partial class MultiSelectMenuPage : PopupPage
//    {
//        public MultiSelectMenuPage()
//        {
       
          
//            InitializeComponent();
//          //  EmployeeView.ItemsSource = App.multiview;
//           // BindingContext = this;
//        }
//    }
//}