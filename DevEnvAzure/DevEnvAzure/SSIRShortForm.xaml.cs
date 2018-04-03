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
                FetchListItems();
                InitializeComponent();
                //  App.DAUtil.GetAllEmployees<>
                // lblname.Text = modelname.ToUpper() + "REPORT";

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
                var eValue = App.DAUtil.GetAllEmployees<DatatableData>("DatatableData1");
                if (eValue != null && eValue.Count > 0)
                {
                    DataUpload.CreateItemsOffline(eValue);
                }
                DependencyService.Get<IMessage>().LongAlert("Network Connection detected");
            }
            // var answer =  DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
        }
        //  _classname secViewfull = null;
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
                        sf.ReportType = "Safety" + idval.ToString();
                        //  sf.AircraftRegis = null;
                        //App.safetyReport.Add(sf);
                        App.DAUtil.SaveEmployee<FlightSafetyReportModel>(sf);
                        DependencyService.Get<IMessage>().ShortAlert("Safety report Drafted");
                        break;
                    case "security":
                        idval = new Random().Next(1, 1000);
                        SecurityModel sd = (SecurityModel)_viewobject;
                        sd.ReportType = "Security" + idval.ToString();
                        App.DAUtil.SaveEmployee<SecurityModel>(sd);
                        DependencyService.Get<IMessage>().ShortAlert("Security report Drafted");
                        break;
                    case "ground":
                        idval = new Random().Next(1, 1000);
                        // App.groundSafety.Add((GroundSafetyReport)_viewobject);
                        GroundSafetyReport gd = (GroundSafetyReport)_viewobject;
                        gd.ReportType = "GroundSafety" + idval.ToString();
                        App.DAUtil.SaveEmployee<GroundSafetyReport>(gd);
                        DependencyService.Get<IMessage>().ShortAlert("Groung Safety report Drafted");
                        break;
                    case "fatigue":
                        idval = new Random().Next(1, 1000);
                        // App.fatigue.Add((FatigueReport)_viewobject);
                        FatigueReport ft = (FatigueReport)_viewobject;
                        ft.ReportType = "Fatigue" + idval.ToString();
                        App.DAUtil.SaveEmployee<FatigueReport>(ft);
                        DependencyService.Get<IMessage>().ShortAlert("Fatigue report Drafted");
                        break;
                    case "Injury":
                        idval = new Random().Next(1, 1000);
                        InjuryIllnessReport injr = (InjuryIllnessReport)_viewobject;
                        injr.ReportType = "InjuryIllness" + idval.ToString();
                        App.DAUtil.SaveEmployee<InjuryIllnessReport>(injr);
                        DependencyService.Get<IMessage>().ShortAlert("Injury Illness report Drafted");
                        break;
                    case "cabin":
                        idval = new Random().Next(1, 1000);
                        CabibSafetyReport cd = (CabibSafetyReport)_viewobject;
                        cd.ReportType = "Cabin" + idval.ToString();
                        App.DAUtil.SaveEmployee<CabibSafetyReport>(cd);
                        DependencyService.Get<IMessage>().ShortAlert("Cabin report Drafted");
                        break;
                }
                MessagingCenter.Send<SSIRShortForm>(this, "draftspopout");
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
                //  var x = fileData.DataArray;
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
                //ExceptionHandler.ShowException(ex.Message);
            }
        }
        private bool CheckConnection()
        {
            return CrossConnectivity.Current.IsConnected;

            //var networkConnection = DependencyService.Get<INetworkConnection>();
            //networkConnection.CheckNetworkConnection();
            //return networkConnection.IsConnected;
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

                    // StringContent contents = null;
                    var client = GetHTTPClient();
                    var data = reportObject;// _viewobject;

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
                            await this.Navigation.PopToRootAsync();
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
                        dt.Value = body;// contents.ToString();
                        App.DAUtil.SaveEmployee<DatatableData>(dt);

                        var vList = App.DAUtil.GetAllEmployees<DatatableData>("DatatableData1");
                        DependencyService.Get<IMessage>().LongAlert("List data stored in local storage");
                        await Navigation.PopToRootAsync();
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
                    //App.safetyReport.Add(sf);
                    App.DAUtil.SaveEmployee<FlightSafetyReportModel>(sf);
                    // App.safetyReport.Add((SafetyReportModel)_viewobject);
                    //  App.DAUtil.SaveEmployee((SafetyReportModel)_viewobject);
                    break;
                case "security":
                    idval = new Random().Next(1, 1000);
                    SecurityModel sd = (SecurityModel)_viewobject;
                    sd.ReportType = "Security" + idval.ToString();
                    //   App.security.Add(sd);
                    App.DAUtil.SaveEmployee<SecurityModel>(sd);
                    //  _fullviewobj = new securityReportView();
                    break;
                case "ground":
                    idval = new Random().Next(1, 1000);
                    // App.groundSafety.Add((GroundSafetyReport)_viewobject);
                    GroundSafetyReport gd = (GroundSafetyReport)_viewobject;
                    gd.ReportType = "GroundSafety" + idval.ToString();
                    //  App.groundSafety.Add(gd);
                    App.DAUtil.SaveEmployee<GroundSafetyReport>(gd);
                    //   App.DAUtil.SaveEmployee((GroundSafetyReport)_viewobject);
                    // _fullviewobj = new GroundSafetyReportView();
                    break;
                case "fatigue":
                    idval = new Random().Next(1, 1000);
                    // App.fatigue.Add((FatigueReport)_viewobject);
                    FatigueReport ft = (FatigueReport)_viewobject;
                    ft.ReportType = "Fatigue" + idval.ToString();
                    //  App.fatigue.Add(ft);
                    App.DAUtil.SaveEmployee<FatigueReport>(ft);
                    // App.DAUtil.SaveEmployee((FatigueReport)_viewobject);
                    //  _fullviewobj = new FatigueReportView();
                    break;
                case "Injury":
                    idval = new Random().Next(1, 1000);
                    //  App.injuryIllness.Add((InjuryIllnessReport)_viewobject);
                    InjuryIllnessReport injr = (InjuryIllnessReport)_viewobject;
                    injr.ReportType = "InjuryIllness" + idval.ToString();
                    //  App.injuryIllness.Add(injr);
                    App.DAUtil.SaveEmployee<InjuryIllnessReport>(injr);
                    //App.DAUtil.SaveEmployee((InjuryIllnessReport)_viewobject);
                    // _fullviewobj = new InjuryIllnessReportView();
                    break;
                case "cabin":
                    idval = new Random().Next(1, 1000);
                    CabibSafetyReport cd = (CabibSafetyReport)_viewobject;
                    cd.ReportType = "Cabin" + idval.ToString();
                    //   App.cabinSafety.Add(cd);
                    App.DAUtil.SaveEmployee<CabibSafetyReport>(cd);
                    //  App.cabinSafety.Add((CabibSafetyReport)_viewobject);
                    //  App.DAUtil.SaveEmployee(cd);
                    // _fullviewobj = new CabinSafetyReportView();
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