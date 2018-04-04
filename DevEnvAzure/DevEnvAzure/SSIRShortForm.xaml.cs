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
        public int? SavedDraftID { get; set; }
        public SSIRShortForm(object viewObject, string modelname)
        {
            try
            {
                this.BindingContext = viewObject;
                _classname = modelname;
                _viewobject = viewObject;
                FetchListItems();
                InitializeComponent();
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
                Plugin.Connectivity.CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
            }
            catch
            {

            }
            //  UpdateStatus();
        }
        private void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            if (e.IsConnected)
            {
                var eValue = App.DAUtil.GetAll<DatatableData>("DatatableData1");
                if (eValue != null && eValue.Count > 0)
                {
                    DataUpload.CreateItemsOffline(eValue);
                }
                DependencyService.Get<IMessage>().LongAlert("Network Connection detected");
            }
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
                    MORlbl.TextColor = Color.OrangeRed;
                    MORpicker.Focus();
                }
                DependencyService.Get<IMessage>().ShortAlert("Please Fill All Required Fields");
            }
            else
            {
                EventTitle.TextColor = Color.Black;
                MORlbl.TextColor = Color.Black;
                switch (_classname)
                {
                    case "safety":
                        FlightSafetyReportModel sf = (FlightSafetyReportModel)_viewobject;
                        sf.ReportType = "Safety" + sf.Id.ToString();
                        MORTypeID = MORpicker.SelectedIndex;
                        CreateItems(jsonInitObj.getflightSafetyJson(sf));
                        App.DAUtil.Delete(sf);
                        break;
                    case "security":
                        SecurityModel sd = (SecurityModel)_viewobject;
                        sd.ReportType = "Security" + sd.Id.ToString();
                        MORTypeID = MORpicker.SelectedIndex;
                        CreateItems(jsonInitObj.getSecurity(sd));
                        App.DAUtil.Delete(sd);
                        break;
                    case "ground":
                        GroundSafetyReport gd = (GroundSafetyReport)_viewobject;
                        gd.ReportType = "GroundSafety" + gd.Id.ToString();
                        CreateItems(jsonInitObj.getGroundSafety(gd));
                        App.DAUtil.Delete(gd);
                        break;
                    case "fatigue":
                        FatigueReport ft = (FatigueReport)_viewobject;
                        ft.ReportType = "Fatigue" + ft.Id.ToString();
                        CreateItems(jsonInitObj.getFatigue(ft));
                        App.DAUtil.Delete(ft);
                        break;
                    case "Injury":
                        InjuryIllnessReport injr = (InjuryIllnessReport)_viewobject;
                        injr.ReportType = "InjuryIllness" + injr.Id.ToString();

                        CreateItems(jsonInitObj.getInjuryJson(injr));
                        App.DAUtil.Delete(injr);
                        break;
                    case "cabin":
                        CabibSafetyReport cd = (CabibSafetyReport)_viewobject;
                        cd.ReportType = "Cabin" + cd.Id.ToString();
                        CreateItems(jsonInitObj.getCabinSfetyJson(cd));
                        App.DAUtil.Delete(cd);
                        break;
                }

                MessagingCenter.Send<SSIRShortForm>(this, "Popout");
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
                        sf = sf.Id == 0 ? App.DAUtil.Save(sf) : App.DAUtil.Update(sf);
                        if (sf != null)
                        {
                            _viewobject = sf;
                        }

                        DependencyService.Get<IMessage>().ShortAlert("Safety report Drafted");
                        break;
                    case "security":
                        idval = new Random().Next(1, 1000);
                        SecurityModel sd = (SecurityModel)_viewobject;
                        sd.IsExtendedView = Formcheck.IsToggled;
                        sd.ReportType = "Security" + idval.ToString();
                        sd = sd.Id == 0 ? App.DAUtil.Save(sd) : App.DAUtil.Update(sd);
                        if (sd != null)
                        {
                            _viewobject = sd;
                        }
                        DependencyService.Get<IMessage>().ShortAlert("Security report Drafted");
                        break;
                    case "ground":
                        idval = new Random().Next(1, 1000);
                        GroundSafetyReport gd = (GroundSafetyReport)_viewobject;
                        gd.IsExtendedView = Formcheck.IsToggled;
                        gd.ReportType = "GroundSafety" + idval.ToString();
                        gd = gd.Id == 0 ? App.DAUtil.Save(gd) : App.DAUtil.Update(gd);
                        if (gd != null)
                        {
                            _viewobject = gd;
                        }
                        DependencyService.Get<IMessage>().ShortAlert("Groung Safety report Drafted");
                        break;
                    case "fatigue":
                        idval = new Random().Next(1, 1000);
                        FatigueReport ft = (FatigueReport)_viewobject;
                        ft.IsExtendedView = Formcheck.IsToggled;
                        ft.ReportType = "Fatigue" + idval.ToString();
                        ft = ft.Id == 0 ? App.DAUtil.Save(ft) : App.DAUtil.Update(ft);
                        if (ft != null)
                        {
                            _viewobject = ft;
                        }
                        DependencyService.Get<IMessage>().ShortAlert("Fatigue report Drafted");
                        break;
                    case "Injury":
                        idval = new Random().Next(1, 1000);
                        InjuryIllnessReport injr = (InjuryIllnessReport)_viewobject;
                        injr.IsExtendedView = Formcheck.IsToggled;
                        injr.ReportType = "InjuryIllness" + idval.ToString();
                        injr = injr.Id == 0 ? App.DAUtil.Save(injr) : App.DAUtil.Update(injr);
                        if (injr != null)
                        {
                            _viewobject = injr;
                        }
                        DependencyService.Get<IMessage>().ShortAlert("Injury Illness report Drafted");
                        break;
                    case "cabin":
                        idval = new Random().Next(1, 1000);
                        CabibSafetyReport cd = (CabibSafetyReport)_viewobject;
                        cd.IsExtendedView = Formcheck.IsToggled;
                        cd.ReportType = "Cabin" + idval.ToString();
                        cd = cd.Id == 0 ? App.DAUtil.Save(cd) : App.DAUtil.Update(cd);
                        if (cd != null)
                        {
                            _viewobject = cd;
                        }
                        DependencyService.Get<IMessage>().ShortAlert("Cabin report Drafted");
                        break;
                }
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
        private HttpClient GetHTTPClient()
        {
            var client = OAuthHelper.GetHTTPClient();

            if (client == null)
            {
                return null;
            }

            return client;
        }
        protected void CreateItems<U>(U reportObject) where U : class
        {
            try
            {
                ToggleBusy(true);

                Device.BeginInvokeOnMainThread(async () =>
                {
                    var client = GetHTTPClient();
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
                            DependencyService.Get<IMessage>().ShortAlert("Success");
                            MessagingCenter.Send(this, "home");
                        }
                        else
                        {
                            var ex = await postResult.Content.ReadAsStringAsync();
                            await DisplayAlert("Error", ex, "Ok");
                        }
                        ToggleBusy(false);
                    }
                    else
                    {
                        ToggleBusy(false);
                        DatatableData dt = new DatatableData();
                        dt.Value = body;
                        App.DAUtil.Save<DatatableData>(dt);

                        var vList = App.DAUtil.GetAll<DatatableData>("DatatableData1");
                        DependencyService.Get<IMessage>().ShortAlert("List data stored in local storage");
                        MessagingCenter.Send(this, "home");
                    }
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
        protected async void FetchListItems()
        {
            var client = GetHTTPClient();
            if (client == null) { return; }
            try
            {
                // https://sptechnophiles.sharepoint.com/_api/web/lists/GetByTitle('MOR%20Type%20Lead%20Time')
                var result = await client.GetStringAsync(SPRootURL + "GetByTitle('MOR%20Type%20Lead%20Time')/items");
                var result1 = await client.GetStringAsync("https://sptechnophiles.sharepoint.com/_api/web/lists/GetByTitle('Operational_Hazard_Event_Register_04042018')/items");
                var FlightCrew = await client.GetStringAsync("https://sptechnophiles.sharepoint.com/_api/web/lists/GetByTitle('Flight Crew Voyage Record')/items");
                var Kaizen = await client.GetStringAsync("https://sptechnophiles.sharepoint.com/_api/web/lists/GetByTitle('Kaizen Report')/items");
                var LineStationInfo = await client.GetStringAsync("https://sptechnophiles.sharepoint.com/_api/web/lists/GetByTitle('(Ops) Line Station Information')/items");
                if (result != null)
                {
                    //'2f41b8fe-275e-4ba6-bbd0-52e73eb55b54'
                    //SP.Data.MOR_x0020_Type_x0020__x0020_Lead_x0020_TimeListItem
                    var data = result.Length;
                    var spData = JsonConvert.DeserializeObject<SPData>(result, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });

                    foreach (var val in spData.d.results)
                    {
                        MORpicker.Items.Add(val.Title);
                    }

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
            }
            catch (Exception ex)
            {
                var msg = "Unable to fetch list items. " + ex.Message;

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