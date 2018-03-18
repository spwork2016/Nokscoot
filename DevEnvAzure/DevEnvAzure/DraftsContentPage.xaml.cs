using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var listitem = (from itm in App.fullDataTablecollection
                            where itm.Value.ToString() == t.CommandParameter.ToString()
                            select itm).SingleOrDefault();
            List<DatatableData> x = new List<DatatableData>();
            x.Add(listitem);
            EmployeeViewdetail.ItemsSource = x;
            BindingContext = this;
        }
    }
}
