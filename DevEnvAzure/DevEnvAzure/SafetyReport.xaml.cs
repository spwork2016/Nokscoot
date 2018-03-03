using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DevEnvAzure.Models;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SafetyReport : ContentPage
    {
        // SafetyReportModel _safetyObject = null;
        object _viewobject = null;
        public SafetyReport(object viewObject) 
        {

            //  safetyObject = new SafetyReportModel();
          //  _safetyObject = safetyObject;
            this.BindingContext = viewObject;
            _viewobject = viewObject;
            InitializeComponent();
        }

        private void Check_Clicked(object sender, EventArgs e)
        {
            // StackLayout st = new StackLayout();

            //  scrollViewer.Content = st;
            // scrollViewer.RaiseChild(st);

        }
        SafetyReportFull st = null;
        private void selectFormpicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (st == null)
            {
                st = new SafetyReportFull();
            }
            if (selectFormpicker.SelectedIndex == 1)
            {
                Stacklay.Children.Add(st);
            }
            else if (selectFormpicker.SelectedIndex == 0)
            {
                Stacklay.Children.Remove(st);
            }
        }

        private void savedrafts_btn_Clicked(object sender, EventArgs e)
        {
         //   App.safetyReport.Add(_viewobject);
            // var v = safetyObject.EventTitle;
            DependencyService.Get<IMessage>(DependencyFetchTarget.GlobalInstance).ShortAlert("Drafted.");
        }
    }
}