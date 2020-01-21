using DevEnvAzure.Models;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DraftsContentPage : ContentPage
    {
        public DraftsContentPage(Button t)
        {
            InitializeComponent();
            var listitem = (from itm in App.offlineItems
                            where itm.Value.ToString() == t.CommandParameter.ToString()
                            select itm).SingleOrDefault();
            List<OfflineItem> x = new List<OfflineItem>();
            x.Add(listitem);
            EmployeeViewdetail.ItemsSource = x;
            BindingContext = this;
        }

        private void EmployeeViewdetail_ItemTapped()
        {

        }
    }
}
