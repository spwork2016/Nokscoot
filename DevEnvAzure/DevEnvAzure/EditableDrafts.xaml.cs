using DevEnvAzure.Models;
using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditableDrafts : ContentPage
    {
        public EditableDrafts()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            Load_saveddrafts();
        }

        public void Load_saveddrafts()
        {
            try
            {
                App.security = new ObservableCollection<SecurityModel>(App.DAUtil.GetAll<SecurityModel>("SecurityModel"));
                lblSecurityReportCount.Text = string.Format("({0})", App.security.Count.ToString());
                stkSecurityReports.IsVisible = App.security.Count == 0 ? false : true;
                MessagingCenter.Subscribe<DraftsExpandContentView>(this, "security", (sendern) =>
                {
                    lblSecurityReportCount.Text = string.Format("({0})", App.safetyReport.Count.ToString());
                    stkSecurityReports.IsVisible = App.cabinSafety.Count == 0 ? false : true;
                    SetNoDataInfo();
                });


                App.safetyReport = new ObservableCollection<FlightSafetyReportModel>(App.DAUtil.GetAll<FlightSafetyReportModel>("FlightSafetyReportModel"));
                lblSafetyReportCount.Text = string.Format("({0})", App.safetyReport.Count.ToString());
                stkSafetyReports.IsVisible = App.safetyReport.Count > 0;
                MessagingCenter.Subscribe<DraftsExpandContentView>(this, "safety", (sendern) =>
                {
                    stkSafetyReports.IsVisible = App.safetyReport.Count > 0;
                    lblSafetyReportCount.Text = string.Format("({0})", App.safetyReport.Count.ToString());
                    SetNoDataInfo();
                });

                App.cabinSafety = new ObservableCollection<CabibSafetyReport>(App.DAUtil.GetAll<CabibSafetyReport>("CabibSafetyReport"));
                lblcabinSafetyReportCount.Text = string.Format("({0})", App.cabinSafety.Count.ToString());
                stkcabinSafetyReports.IsVisible = App.cabinSafety.Count > 0;
                MessagingCenter.Subscribe<DraftsExpandContentView>(this, "cabinsafety", (sendern) =>
                {
                    lblcabinSafetyReportCount.Text = string.Format("({0})", App.safetyReport.Count.ToString());
                    stkcabinSafetyReports.IsVisible = App.cabinSafety.Count > 0;
                    SetNoDataInfo();
                });

                App.injuryIllness = new ObservableCollection<InjuryIllnessReport>(App.DAUtil.GetAll<InjuryIllnessReport>("InjuryIllnessReport"));
                lblinjuryIllnessReportCount.Text = string.Format("({0})", App.injuryIllness.Count.ToString());
                stkinjuryIllnessReports.IsVisible = App.injuryIllness.Count > 0;
                MessagingCenter.Subscribe<DraftsExpandContentView>(this, "injuryillness", (sendern) =>
                {
                    lblinjuryIllnessReportCount.Text = string.Format("({0})", App.safetyReport.Count.ToString());
                    stkinjuryIllnessReports.IsVisible = App.injuryIllness.Count > 0;
                    SetNoDataInfo();
                });

                App.groundSafety = new ObservableCollection<GroundSafetyReport>(App.DAUtil.GetAll<GroundSafetyReport>("GroundSafetyReport"));
                lblGroundSafetyReportCount.Text = string.Format("({0})", App.groundSafety.Count.ToString());
                stkGroundSafetyReports.IsVisible = App.groundSafety.Count > 0;
                MessagingCenter.Subscribe<DraftsExpandContentView>(this, "groundsafety", (sendern) =>
                {
                    lblGroundSafetyReportCount.Text = string.Format("({0})", App.safetyReport.Count.ToString());
                    stkGroundSafetyReports.IsVisible = App.groundSafety.Count > 0;
                    SetNoDataInfo();
                });

                App.fatigue = new ObservableCollection<FatigueReport>(App.DAUtil.GetAll<FatigueReport>("FatigueReport"));
                lblFatigueReportCount.Text = string.Format("({0})", App.fatigue.Count.ToString());
                stkFatigueReports.IsVisible = App.fatigue.Count > 0;
                MessagingCenter.Subscribe<DraftsExpandContentView>(this, "fatigue", (sendern) =>
                {
                    lblFatigueReportCount.Text = string.Format("({0})", App.safetyReport.Count.ToString());
                    stkFatigueReports.IsVisible = App.fatigue.Count > 0;
                    SetNoDataInfo();
                });

                App.kaizen = new ObservableCollection<KaizenReportModel>(App.DAUtil.GetAll<KaizenReportModel>("KaizenReportModel"));
                kaizencnt.Text = string.Format("({0})", App.kaizen.Count.ToString());
                stkKaizen.IsVisible = App.kaizen.Count > 0;
                MessagingCenter.Subscribe<DraftsExpandContentView>(this, "kaizen", (sendern) =>
                {
                    kaizencnt.Text = string.Format("({0})", App.safetyReport.Count.ToString());
                    stkKaizen.IsVisible = App.kaizen.Count > 0;
                    SetNoDataInfo();
                });

                App.fcVoyage = new ObservableCollection<FlightCrewVoyageRecordModel>(App.DAUtil.GetAll<FlightCrewVoyageRecordModel>("FlightCrewVoyageRecordModel"));
                fcVoyageCnt.Text = string.Format("({0})", App.fcVoyage.Count.ToString());
                sktfcVoyage.IsVisible = App.fcVoyage.Count > 0;
                MessagingCenter.Subscribe<DraftsExpandContentView>(this, "facvoyage", (sendern) =>
                {
                    fcVoyageCnt.Text = string.Format("({0})", App.safetyReport.Count.ToString());
                    sktfcVoyage.IsVisible = App.fcVoyage.Count > 0;
                    SetNoDataInfo();
                });

                App.statInfo = new ObservableCollection<StationInformationModel>(App.DAUtil.GetAll<StationInformationModel>("StationInformationModel"));
                StatnInfoCnt.Text = string.Format("({0})", App.statInfo.Count.ToString());
                sktStatnInfo.IsVisible = App.statInfo.Count == 0 ? false : true;
                MessagingCenter.Subscribe<DraftsExpandContentView>(this, "stninfo", (sendern) =>
                {
                    StatnInfoCnt.Text = string.Format("({0})", App.safetyReport.Count.ToString());
                    sktStatnInfo.IsVisible = App.statInfo.Count > 0;
                    SetNoDataInfo();
                });

                SetNoDataInfo();
            }
            catch (Exception ex)
            {

            }
        }

        private void SetNoDataInfo()
        {
            int totalCount = 0;

            totalCount += App.statInfo.Count;
            totalCount += App.kaizen.Count;
            totalCount += App.fatigue.Count;
            totalCount += App.security.Count;
            totalCount += App.groundSafety.Count;
            totalCount += App.injuryIllness.Count;
            totalCount += App.cabinSafety.Count;
            totalCount += App.safetyReport.Count;
            totalCount += App.fcVoyage.Count;

            stkNodata.IsVisible = totalCount == 0;
        }

        private void ExpandCollapsePanel(object sender, StackLayout layout, string rptType)
        {
            var btn = (Button)sender;
            if (btn.Text == "+")
            {
                btn.Text = "-";
                removeChildren();
                layout.Children.Add(new DraftsExpandContentView(rptType));
            }
            else
            {
                btn.Text = "+";
                layout.Children.RemoveAt(0);
            }
        }

        private void Safety_Expand(object sender, EventArgs e)
        {
            ExpandCollapsePanel(sender, SafetyLayout, "safety");
        }

        private void GroundSafety_Expand(object sender, EventArgs e)
        {
            ExpandCollapsePanel(sender, groundSafetyLayout, "ground");
        }

        private void Security_Expand(object sender, EventArgs e)
        {
            ExpandCollapsePanel(sender, securityLayout, "security");
        }

        private void Fatigue_Expand(object sender, EventArgs e)
        {
            ExpandCollapsePanel(sender, fatigueLayout, "fatigue");
        }

        private void Cabin_Expand(object sender, EventArgs e)
        {
            ExpandCollapsePanel(sender, cabinSafetyLayout, "cabin");
        }

        private void Injuryillness_Expand(object sender, EventArgs e)
        {
            ExpandCollapsePanel(sender, injuryillnessLayout, "illness");
        }
        private void kaizen_Expand(object sender, EventArgs e)
        {
            ExpandCollapsePanel(sender, kaizenLayout, "kaizen");
        }
        private void fcVoyage_Expand(object sender, EventArgs e)
        {
            ExpandCollapsePanel(sender, fcVoyageLayout, "fcVoyage");
        }
        private void StatnInfo_Expand(object sender, EventArgs e)
        {
            ExpandCollapsePanel(sender, StatnInfoLayout, "stsnInfo");
        }
        public void removeChildren()
        {
            if (groundSafetyLayout.Children.Count > 0)
            {
                groundSafetyLayout.Children.RemoveAt(0);
                btnGroundSafetyPanel.Text = "+";
            }
            if (securityLayout.Children.Count > 0)
            {
                securityLayout.Children.RemoveAt(0);
                btnSecurityPanel.Text = "+";
            }
            if (fatigueLayout.Children.Count > 0)
            {
                fatigueLayout.Children.RemoveAt(0);
                btnFatiguePanel.Text = "+";
            }
            if (cabinSafetyLayout.Children.Count > 0)
            {
                cabinSafetyLayout.Children.RemoveAt(0);
                btnCabinPanel.Text = "+";
            }
            if (injuryillnessLayout.Children.Count > 0)
            {
                injuryillnessLayout.Children.RemoveAt(0);
                btnInjuryIllnessPanel.Text = "+";
            }
            if (SafetyLayout.Children.Count > 0)
            {
                SafetyLayout.Children.RemoveAt(0);
                btnSafetyPanel.Text = "+";
            }
            if (fcVoyageLayout.Children.Count > 0)
            {
                fcVoyageLayout.Children.RemoveAt(0);
                btnfcVoyage.Text = "+";
            }
            if (kaizenLayout.Children.Count > 0)
            {
                kaizenLayout.Children.RemoveAt(0);
                btnkaizen.Text = "+";
            }
            if (StatnInfoLayout.Children.Count > 0)
            {
                StatnInfoLayout.Children.RemoveAt(0);
                btnStatnInfo.Text = "+";
            }
        }
    }
}