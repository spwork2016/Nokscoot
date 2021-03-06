﻿using DevEnvAzure.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DraftsExpandContentView : ContentView
    {
        public string _reportType;
        public DraftsExpandContentView(string reportType)
        {
            InitializeComponent();
            _reportType = reportType;
            //  App.safetyReport = new ObservableCollection<FlightSafetyReportModel>(App.DAUtil.GetAllEmployees<FlightSafetyReportModel>("SafetyReportModel"));
            DataBind(reportType);

        }

        private void DataBind(string reportType)
        {
            switch (reportType)
            {
                case "safety":
                    ItemsListView.ItemsSource = App.safetyReport;
                    ItemsListView.BindingContext = App.safetyReport;
                    ItemsListView.HeightRequest = App.safetyReport.Count * 45;
                    break;
                case "ground":
                    ItemsListView.ItemsSource = App.groundSafety;
                    ItemsListView.BindingContext = App.groundSafety;
                    ItemsListView.HeightRequest = App.groundSafety.Count * 45;
                    break;

                case "cabin":
                    ItemsListView.ItemsSource = App.cabinSafety;
                    ItemsListView.BindingContext = App.cabinSafety;
                    ItemsListView.HeightRequest = App.cabinSafety.Count * 45;
                    break;

                case "security":
                    ItemsListView.ItemsSource = App.security;
                    ItemsListView.BindingContext = App.security;
                    ItemsListView.HeightRequest = App.security.Count * 45;
                    break;

                case "illness":
                    ItemsListView.ItemsSource = App.injuryIllness;
                    ItemsListView.BindingContext = App.injuryIllness;
                    ItemsListView.HeightRequest = App.injuryIllness.Count * 45;
                    break;
                case "fatigue":
                    ItemsListView.ItemsSource = App.fatigue;
                    ItemsListView.BindingContext = App.fatigue;
                    ItemsListView.HeightRequest = App.fatigue.Count * 45;
                    break;
                case "kaizen":
                    ItemsListView.ItemsSource = App.kaizen;
                    ItemsListView.BindingContext = App.kaizen;
                    ItemsListView.HeightRequest = App.kaizen.Count * 45;
                    break;
                case "fcVoyage":
                    ItemsListView.ItemsSource = App.fcVoyage;
                    ItemsListView.BindingContext = App.fcVoyage;
                    ItemsListView.HeightRequest = App.fcVoyage.Count * 45;
                    break;
                case "stsnInfo":
                    ItemsListView.ItemsSource = App.statInfo;
                    ItemsListView.BindingContext = App.statInfo;
                    ItemsListView.HeightRequest = App.statInfo.Count * 45;
                    break;
            }
        }

        private async void navigateToReport(int id)
        {
            switch (_reportType)
            {
                case "safety":
                    FlightSafetyReportModel listitem = (from itm in App.safetyReport
                                                        where itm.Id == id
                                                        select itm).FirstOrDefault();
                    await Navigation.PushAsync(new SSIRShortForm(listitem, "safety"));
                    break;
                case "ground":
                    GroundSafetyReport listitem1 = (from itm in App.groundSafety
                                                    where itm.Id == id
                                                    select itm).FirstOrDefault();
                    await Navigation.PushAsync(new SSIRShortForm(listitem1, "ground"));
                    break;
                case "cabin":

                    CabibSafetyReport listitem2 = (from itm in App.cabinSafety
                                                   where itm.Id == id
                                                   select itm).FirstOrDefault();
                    await Navigation.PushAsync(new SSIRShortForm(listitem2, "cabin"));
                    break;
                case "security":
                    SecurityModel listitem3 = (from itm in App.security
                                               where itm.Id == id
                                               select itm).FirstOrDefault();
                    await Navigation.PushAsync(new SSIRShortForm(listitem3, "security"));
                    break;
                case "illness":
                    InjuryIllnessReport listitem4 = (from itm in App.injuryIllness
                                                     where itm.Id == id
                                                     select itm).FirstOrDefault();
                    await Navigation.PushAsync(new SSIRShortForm(listitem4, "Injury"));
                    break;
                case "fatigue":
                    FatigueReport listitem5 = (from itm in App.fatigue
                                               where itm.Id == id
                                               select itm).FirstOrDefault();
                    await Navigation.PushAsync(new SSIRShortForm(listitem5, "fatigue"));
                    break;
                case "kaizen":
                    KaizenReportModel listitem6 = (from itm in App.kaizen
                                                   where itm.Id == id
                                                   select itm).FirstOrDefault();
                    await Navigation.PushAsync(new KaizenReport(listitem6, "kaizen"));
                    break;
                case "fcVoyage":
                    FlightCrewVoyageRecordModel listitem7 = (from itm in App.fcVoyage
                                                             where itm.Id == id
                                                             select itm).FirstOrDefault();
                    await Navigation.PushAsync(new FlightCrewVoyageRecord(listitem7, "fcVoyage"));
                    break;
                case "stsnInfo":
                    StationInformationModel listitem8 = (from itm in App.statInfo
                                                         where itm.Id == id
                                                         select itm).FirstOrDefault();
                    await Navigation.PushAsync(new StationInformation(listitem8));
                    break;
            }
        }

        private async void SafetyReport_Clicked(object sender, EventArgs e)
        {
            try
            {
                var item = (Button)sender;
                navigateToReport(Convert.ToInt32(item.CommandParameter));
            }
            catch (Exception ex)
            {

            }
        }

        private void MenuItem_Delete_Clicked(object sender, EventArgs e)
        {
            var cp = ((MenuItem)sender).CommandParameter;
            if (cp == null) return;
            string reportName = cp.ToString();

            //TODO - delete appropriate report
            App.DAUtil.Delete(cp);
            if (reportName == "DevEnvAzure.Models.FlightSafetyReportModel")
            {
                App.safetyReport = new ObservableCollection<FlightSafetyReportModel>(App.DAUtil.GetAll<FlightSafetyReportModel>("FlightSafetyReportModel"));
                MessagingCenter.Send<DraftsExpandContentView>(this, "safety");
            }
            if (reportName == "DevEnvAzure.Models.SecurityModel")
            {
                App.security = new ObservableCollection<SecurityModel>(App.DAUtil.GetAll<SecurityModel>("SecurityModel"));
                MessagingCenter.Send<DraftsExpandContentView>(this, "security");
            }
            if (reportName == "DevEnvAzure.Models.CabibSafetyReport")
            {
                App.cabinSafety = new ObservableCollection<CabibSafetyReport>(App.DAUtil.GetAll<CabibSafetyReport>("CabibSafetyReport"));
                MessagingCenter.Send<DraftsExpandContentView>(this, "cabinsafety");
            }
            if (reportName == "DevEnvAzure.Models.GroundSafetyReport")
            {
                App.groundSafety = new ObservableCollection<GroundSafetyReport>(App.DAUtil.GetAll<GroundSafetyReport>("GroundSafetyReport"));
                MessagingCenter.Send<DraftsExpandContentView>(this, "groundsafety");
            }
            if (reportName == "DevEnvAzure.Models.InjuryIllnessReport")
            {
                App.injuryIllness = new ObservableCollection<InjuryIllnessReport>(App.DAUtil.GetAll<InjuryIllnessReport>("InjuryIllnessReport"));
                MessagingCenter.Send<DraftsExpandContentView>(this, "injuryillness");
            }
            if (reportName == "DevEnvAzure.Models.FatigueReport")
            {
                App.fatigue = new ObservableCollection<FatigueReport>(App.DAUtil.GetAll<FatigueReport>("FatigueReport"));
                MessagingCenter.Send<DraftsExpandContentView>(this, "fatigue");
            }
            if (reportName == "DevEnvAzure.Models.FlightCrewVoyageRecordModel")
            {
                App.fcVoyage = new ObservableCollection<FlightCrewVoyageRecordModel>(App.DAUtil.GetAll<FlightCrewVoyageRecordModel>("FlightCrewVoyageRecordModel"));
                MessagingCenter.Send<DraftsExpandContentView>(this, "facvoyage");
            }
            if (reportName == "DevEnvAzure.Models.KaizenReportModel")
            {
                App.kaizen = new ObservableCollection<KaizenReportModel>(App.DAUtil.GetAll<KaizenReportModel>("KaizenReportModel"));
                MessagingCenter.Send<DraftsExpandContentView>(this, "kaizen");
            }
            if (reportName == "DevEnvAzure.Models.StationInformationModel")
            {
                App.statInfo = new ObservableCollection<StationInformationModel>(App.DAUtil.GetAll<StationInformationModel>("StationInformationModel"));
                MessagingCenter.Send<DraftsExpandContentView>(this, "stninfo");
            }
            DataBind(_reportType);
            DependencyService.Get<IMessage>().ShortAlert("Draft deleted");
        }

        private async void ItemsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            System.Reflection.PropertyInfo pi = e.Item.GetType().GetProperty("Id");
            if (pi != null)
            {
                int id = (int)(pi.GetValue(e.Item, null));
                navigateToReport(id);
            }
        }
    }
}