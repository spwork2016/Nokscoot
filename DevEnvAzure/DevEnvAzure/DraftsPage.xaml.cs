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
           
            EmployeeView.ItemsSource = App.employees;
            BindingContext = this;
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
