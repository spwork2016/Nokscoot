using DevEnvAzure.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DraftsPage : ContentPage
    {
        //public static ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
        public DraftsPage()
        {
            InitializeComponent();
            load_saveddrafts();
            EmployeeView.ItemsSource = App.fullDataTablecollection;
            BindingContext = this;
        }

        public void load_saveddrafts()
        {
            try
            {

                var d = App.DAUtil.GetAllEmployees<DatatableData>("DatatableData1");
                App.fullDataTablecollection = new ObservableCollection<DatatableData>(App.DAUtil.GetAllEmployees<DatatableData>("DatatableData1"));
               
            }
            catch (Exception ex)
            {

            }
        }
        private async void Details_OnClicked(object sender, EventArgs e)
        {
            try
            {
                var item = (Button)sender;
                await Navigation.PushAsync( new DraftsContentPage(item));
            }
            catch (Exception ex)
            {
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
               // var item = (Button)sender; 
               // await Navigation.PushAsync(new SafetyReport());
            }
            catch (Exception ex)
            {
            }
        }
        //public static Page TestPage(Button t)
        //{
        //    var obj = t.CommandParameter;
        //    return new ContentPage
        //    {
        //        Content = new Label
        //        {

        //            VerticalOptions = LayoutOptions.CenterAndExpand,
        //            HorizontalOptions = LayoutOptions.CenterAndExpand
        //        }
        //    };
        //}
    }
}
