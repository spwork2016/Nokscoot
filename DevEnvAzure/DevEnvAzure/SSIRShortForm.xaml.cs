using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DevEnvAzure.Model;
using DevEnvAzure.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DevEnvAzure.Utilities;
using Xamarin.Forms.Internals;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SSIRShortForm : ContentPage
    {
        public object _viewobject = null;
        public string _classname;
        public static string airregis;
        public static int MORTypeID;
        Jsonpropertyinitialise jsonInitObj = new Jsonpropertyinitialise();
        const string SPRootURL = "https://sptechnophiles.sharepoint.com/_api/web/lists/";
        public SSIRShortForm(object viewObject, string modelname)
        {
            try
            {
                this.BindingContext = viewObject;
                _classname = modelname;
                _viewobject = viewObject;
                InitializeComponent();

                BindMORPicker();
            }
            catch (Exception ex)
            {

            }
        }
        protected override void OnAppearing()
        {
            try
            {
                switch (_classname)
                {
                    case "safety":
                        Title = "Flight Safety";
                        break;
                    case "ground":
                        Title = "Ground Safety";
                        break;
                    case "cabin":
                        Title = "Cabin Safety";
                        break;
                    case "Injury":
                        Title = "Injury Illness";
                        break;
                    default:
                        Title = char.ToUpper(_classname[0]) + _classname.Substring(1);
                        break;
                }

                base.OnAppearing();
            }
            catch
            {

            }
            //  UpdateStatus();
        }

        ContentView _fullviewobj = null;
        private void Check_Clicked(object sender, XLabs.EventArgs<bool> e)
        {
            if (_fullviewobj == null)
            {
                switch (_classname)
                {
                    case "safety":
                        _fullviewobj = new FlightSafetyReportView();
                        break;
                    case "security":
                        _fullviewobj = new securityReportView();
                        break;
                    case "ground":
                        _fullviewobj = new GroundSafetyReportView();
                        break;
                    case "fatigue":
                        _fullviewobj = new FatigueReportView();
                        break;
                    case "Injury":
                        _fullviewobj = new InjuryIllnessReportView();
                        break;
                    case "cabin":
                        _fullviewobj = new CabinSafetyReportView();
                        break;
                }
            }
            if (Formcheck.IsToggled == true)
            {
                try
                {
                    Stacklay2.Children.Add(_fullviewobj);
                }
                catch
                {

                }
            }
            else
            {
                Stacklay2.Children.Remove(_fullviewobj);
            }
        }

        public static int idval;
        private void Save_clicked(object sender, XLabs.EventArgs<bool> e)
        {
            if (Convert.ToString(EventTitleEntry.Text).Length == 0 || dtevntPicker.Date == null || Convert.ToString(MORpicker.SelectedItem).Length == 0)
            {
                if (Convert.ToString(EventTitleEntry.Text).Length == 0)
                {
                    EventTitle.TextColor = Color.OrangeRed;
                    EventTitleEntry.Focus();
                }
                else if (Convert.ToString(MORpicker.SelectedItem).Length == 0)
                {
                    MOR.TextColor = Color.OrangeRed;
                    MORpicker.Focus();
                }
                DependencyService.Get<IMessage>().ShortAlert("Please fill all required fields");
            }
            else
            {
                EventTitle.TextColor = Color.Black;
                MOR.TextColor = Color.Black;
                switch (_classname)
                {
                    case "safety":
                        FlightSafetyReportModel sf = (FlightSafetyReportModel)_viewobject;
                        sf.ReportType = "Safety" + sf.Id.ToString();
                        MORTypeID = MORpicker.SelectedIndex;
                        CreateItems(jsonInitObj.getflightSafetyJson(sf), SPUtility.ReportType.FlightSafety);
                        App.DAUtil.Delete(sf);
                        break;
                    case "security":
                        SecurityModel sd = (SecurityModel)_viewobject;
                        sd.ReportType = "Security" + sd.Id.ToString();
                        MORTypeID = MORpicker.SelectedIndex;
                        CreateItems(jsonInitObj.getSecurity(sd), SPUtility.ReportType.Security);
                        App.DAUtil.Delete(sd);
                        break;
                    case "ground":
                        GroundSafetyReport gd = (GroundSafetyReport)_viewobject;
                        gd.ReportType = "GroundSafety" + gd.Id.ToString();
                        CreateItems(jsonInitObj.getGroundSafety(gd), SPUtility.ReportType.GroundSafety);
                        App.DAUtil.Delete(gd);
                        break;
                    case "fatigue":
                        FatigueReport ft = (FatigueReport)_viewobject;
                        ft.ReportType = "Fatigue" + ft.Id.ToString();
                        CreateItems(jsonInitObj.getFatigue(ft), SPUtility.ReportType.Fatigue);
                        App.DAUtil.Delete(ft);
                        break;
                    case "Injury":
                        InjuryIllnessReport injr = (InjuryIllnessReport)_viewobject;
                        injr.ReportType = "InjuryIllness" + injr.Id.ToString();

                        CreateItems(jsonInitObj.getInjuryJson(injr), SPUtility.ReportType.InjuryIllness);
                        App.DAUtil.Delete(injr);
                        break;
                    case "cabin":
                        CabibSafetyReport cd = (CabibSafetyReport)_viewobject;
                        cd.ReportType = "Cabin" + cd.Id.ToString();
                        CreateItems(jsonInitObj.getCabinSfetyJson(cd), SPUtility.ReportType.CabinSafety);
                        App.DAUtil.Delete(cd);
                        break;
                }
            }

        }

        private void savedrafts_btn_Clicked(object sender, EventArgs e)
        {
            try
            {
                switch (_classname)
                {
                    case "safety":
                        idval = new Random().Next(1, 1000);
                        FlightSafetyReportModel sf = (FlightSafetyReportModel)_viewobject;
                        sf.IsExtendedView = Formcheck.IsToggled;
                        sf.ReportType = "Safety" + idval.ToString();
                        sf.MOR = Convert.ToString(MORpicker.SelectedIndex);
                        sf = App.DAUtil.SaveOrUpdate(sf);
                        if (sf != null)
                        {
                            _viewobject = sf;
                        }
                        break;
                    case "security":
                        idval = new Random().Next(1, 1000);
                        SecurityModel sd = (SecurityModel)_viewobject;
                        sd.IsExtendedView = Formcheck.IsToggled;
                        sd.ReportType = "Security" + idval.ToString();
                        sd.MOR = Convert.ToString(MORpicker.SelectedIndex);
                        sd = App.DAUtil.SaveOrUpdate(sd);
                        if (sd != null)
                        {
                            _viewobject = sd;
                        }

                        break;
                    case "ground":
                        idval = new Random().Next(1, 1000);
                        GroundSafetyReport gd = (GroundSafetyReport)_viewobject;
                        gd.IsExtendedView = Formcheck.IsToggled;
                        gd.ReportType = "GroundSafety" + idval.ToString();
                        gd.MOR = Convert.ToString(MORpicker.SelectedIndex);
                        gd = App.DAUtil.SaveOrUpdate(gd);
                        if (gd != null)
                        {
                            _viewobject = gd;
                        }

                        break;
                    case "fatigue":
                        idval = new Random().Next(1, 1000);
                        FatigueReport ft = (FatigueReport)_viewobject;
                        ft.IsExtendedView = Formcheck.IsToggled;
                        ft.ReportType = "Fatigue" + idval.ToString();
                        ft.MOR = Convert.ToString(MORpicker.SelectedIndex);
                        ft = App.DAUtil.SaveOrUpdate(ft);
                        if (ft != null)
                        {
                            _viewobject = ft;
                        }

                        break;
                    case "Injury":
                        idval = new Random().Next(1, 1000);
                        InjuryIllnessReport injr = (InjuryIllnessReport)_viewobject;
                        injr.IsExtendedView = Formcheck.IsToggled;
                        injr.ReportType = "InjuryIllness" + idval.ToString();
                        injr.MOR = Convert.ToString(MORpicker.SelectedIndex);
                        injr = App.DAUtil.SaveOrUpdate(injr);
                        if (injr != null)
                        {
                            _viewobject = injr;
                        }

                        break;
                    case "cabin":
                        idval = new Random().Next(1, 1000);
                        CabibSafetyReport cd = (CabibSafetyReport)_viewobject;
                        cd.IsExtendedView = Formcheck.IsToggled;
                        cd.ReportType = "Cabin" + idval.ToString();
                        cd.MOR = Convert.ToString(MORpicker.SelectedIndex);
                        cd = App.DAUtil.SaveOrUpdate(cd);
                        if (cd != null)
                        {
                            _viewobject = cd;
                        }

                        break;
                }

                DependencyService.Get<IMessage>().ShortAlert("Item drafted");
            }
            catch (Exception)
            {

            }

        }

        private async void MORattach_clicked(object sender, EventArgs e)
        {
            try
            {
                FileData fileData = new FileData();
                fileData = await CrossFilePicker.Current.PickFile();
                if (fileData != null)
                {
                    var y = fileData.FileName;
                    eventAttachEntry.Text = y;
                }
                else
                {
                    eventAttachEntry.Text = "";
                }
                //  SourceImg.Source = ImageSource.FromFile(y);


            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert("Upload Error");
            }
        }
        private bool CheckConnection()
        {
            return CrossConnectivity.Current.IsConnected;
        }

        protected void CreateItems<U>(U reportObject, SPUtility.ReportType reportType) where U : class
        {
            try
            {
                ToggleBusy(true);

                Device.BeginInvokeOnMainThread(async () =>
                {
                    var client = await OAuthHelper.GetHTTPClient();
                    var data = reportObject;

                    var body = JsonConvert.SerializeObject(data, Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
                    var contents = new StringContent(body);
                    contents.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;odata=verbose");
                    if (CheckConnection())
                    {
                        var postResult = await client.PostAsync("https://sptechnophiles.sharepoint.com/_api/web/lists/GetByTitle('Operational_Hazard_Event_Register_04042018')/items", contents);

                        if (postResult.IsSuccessStatusCode)
                        {
                            await DisplayAlert("Success", "Item created successfully", "Ok");
                            MessagingCenter.Send<SSIRShortForm>(this, "home");
                        }
                        else
                        {
                            var ex = await postResult.Content.ReadAsStringAsync();
                            await DisplayAlert("Error", ex, "Ok");
                        }
                    }
                    else
                    {
                        OfflineItem dt = new OfflineItem();
                        dt.Value = body;
                        dt.Created = DateTime.Now;
                        dt.ReportType = (int)reportType;
                        App.DAUtil.Save<OfflineItem>(dt);

                        await DisplayAlert("", "Item stored in local storage", "Ok");
                        MessagingCenter.Send(this, "home");
                    }

                    ToggleBusy(false);
                });
            }
            catch (HttpRequestException ex)
            {
                DependencyService.Get<IMessage>().ShortAlert("Upload Error");
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert("Upload Error" + ex.Message);
            }
        }

        private void SetMORPickerValue()
        {
            if (_viewobject != null)
            {
                System.Reflection.PropertyInfo pi = _viewobject.GetType().GetProperty("MOR");
                if (pi != null)
                {
                    string selectedMOR = (string)(pi.GetValue(_viewobject, null));
                    if (!string.IsNullOrEmpty(selectedMOR))
                        MORpicker.SelectedIndex = Convert.ToInt32(selectedMOR);
                }
            }
        }

        protected async void BindMORPicker()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                var results = App.DAUtil.GetMasterInfoByName("MORItems");
                if (results != null)
                {
                    var spData = JsonConvert.DeserializeObject<SPData>(results.content, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });

                    foreach (var val in spData.d.results)
                    {
                        MORpicker.Items.Add(val.Title);
                    }

                    SetMORPickerValue();
                }

                return;
            }

            var client = await OAuthHelper.GetHTTPClient();
            if (client == null) { return; }
            try
            {
                var result = await client.GetStringAsync(SPRootURL + "GetByTitle('MOR%20Type%20Lead%20Time')/items");

                if (result != null)
                {
                    App.DAUtil.RefreshMasterInfo(new MasterInfo { content = result, Name = "MORItems" });
                    var spData = JsonConvert.DeserializeObject<SPData>(result, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });

                    foreach (var val in spData.d.results)
                    {
                        MORpicker.Items.Add(val.Title);
                    }

                    SetMORPickerValue();
                }
            }
            catch (Exception ex)
            {
                var msg = "Unable to fetch list items. " + ex.Message;
                await DisplayAlert("Error", msg, "Ok");
            }
        }

        public void intializeProperties()
        {
            switch (_classname)
            {

                case "safety":
                    idval = new Random().Next(1, 1000);
                    FlightSafetyReportModel sf = (FlightSafetyReportModel)_viewobject;
                    sf.ReportType = "Safety" + idval.ToString();
                    App.DAUtil.Save<FlightSafetyReportModel>(sf);
                    break;
                case "security":
                    idval = new Random().Next(1, 1000);
                    SecurityModel sd = (SecurityModel)_viewobject;
                    sd.ReportType = "Security" + idval.ToString();
                    App.DAUtil.Save<SecurityModel>(sd);
                    break;
                case "ground":
                    idval = new Random().Next(1, 1000);
                    GroundSafetyReport gd = (GroundSafetyReport)_viewobject;
                    gd.ReportType = "GroundSafety" + idval.ToString();
                    App.DAUtil.Save<GroundSafetyReport>(gd);
                    break;
                case "fatigue":
                    idval = new Random().Next(1, 1000);
                    FatigueReport ft = (FatigueReport)_viewobject;
                    ft.ReportType = "Fatigue" + idval.ToString();
                    App.DAUtil.Save<FatigueReport>(ft);
                    break;
                case "Injury":
                    idval = new Random().Next(1, 1000);
                    InjuryIllnessReport injr = (InjuryIllnessReport)_viewobject;
                    injr.ReportType = "InjuryIllness" + idval.ToString();
                    App.DAUtil.Save<InjuryIllnessReport>(injr);
                    break;
                case "cabin":
                    idval = new Random().Next(1, 1000);
                    CabibSafetyReport cd = (CabibSafetyReport)_viewobject;
                    cd.ReportType = "Cabin" + idval.ToString();
                    App.DAUtil.Save<CabibSafetyReport>(cd);
                    break;
            }

        }

        private void aircraftRegis_Changed(object sender, EventArgs e)
        {
            if (FlightPhasepicker.SelectedIndex > 0)
                airregis = FlightPhasepicker.Items.ElementAt(FlightPhasepicker.SelectedIndex);
        }

        private void ToggleBusy(bool flag)
        {
            activityStack.IsVisible = flag;
            activityIndicator.IsRunning = flag;
        }
    }
}