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
        public  string _classname;
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
            catch(Exception ex)
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
                        Title = "FLIGHT " + _classname.ToUpper() + " REPORT";
                        break;
                    case "ground":
                        Title =  _classname.ToUpper() + " SAFETY REPORT";
                        break;
                    case "cabin":
                        Title = _classname.ToUpper() + " SAFETY REPORT";
                        break;
                    case "Injury":
                        Title = _classname.ToUpper() + " ILLNESS REPORT";
                        break;
                    default:
                        Title = _classname.ToUpper() + " REPORT";
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
            if(Convert.ToString(EventTitleEntry.Text).Length==0 || dtevntPicker.Date==null || Convert.ToString(MORpicker.SelectedItem).Length==0)
            {
                if(Convert.ToString(EventTitleEntry.Text).Length == 0)
                {
                    EventTitleEntry.BackgroundColor = Color.Red;
                    EventTitleEntry.Focus();
                }
                if(Convert.ToString(MORpicker.SelectedItem).Length == 0)
                {
                    MORpicker.BackgroundColor = Color.Red;
                    MORpicker.Focus();
                }
                DependencyService.Get<IMessage>().ShortAlert("Please Fill All Required Fields");
            }
            else
            {
                EventTitleEntry.BackgroundColor = Color.White;
                MORpicker.BackgroundColor = Color.White;
                switch (_classname)
                {
                    case "safety":

                        idval = new Random().Next(1, 1000);
                        FlightSafetyReportModel sf = (FlightSafetyReportModel)_viewobject;
                        sf.ReportType = "Safety" + idval.ToString();
                        MORTypeID = MORpicker.SelectedIndex;
                        CreateItems(jsonInitObj.getflightSafetyJson(sf));
                        // App.DAUtil.SaveEmployee((SafetyReportModel)_viewobject);
                        //  App.DAUtil.SaveEmployee<SafetyReportModel>(sf);
                        break;
                    case "security":

                        idval = new Random().Next(1, 1000);
                        SecurityModel sd = (SecurityModel)_viewobject;
                        sd.ReportType = "Security" + idval.ToString();
                        MORTypeID = MORpicker.SelectedIndex;
                        CreateItems(jsonInitObj.getSecurity(sd));
                        // App.DAUtil.SaveEmployee<SecurityModel>(sd);
                        break;
                    case "ground":
                        // CreateItems();
                        idval = new Random().Next(1, 1000);
                        GroundSafetyReport gd = (GroundSafetyReport)_viewobject;
                        gd.ReportType = "GroundSafety" + idval.ToString();


                        CreateItems(jsonInitObj.getGroundSafety(gd));
                        //   App.DAUtil.SaveEmployee<GroundSafetyReport>(gd);
                        break;
                    case "fatigue":
                        //   CreateItems();
                        idval = new Random().Next(1, 1000);
                        FatigueReport ft = (FatigueReport)_viewobject;
                        ft.ReportType = "Fatigue" + idval.ToString();

                        CreateItems(jsonInitObj.getFatigue(ft));
                        // App.DAUtil.SaveEmployee<FatigueReport>(ft);
                        break;
                    case "Injury":
                        //  CreateItems();
                        idval = new Random().Next(1, 1000);
                        InjuryIllnessReport injr = (InjuryIllnessReport)_viewobject;
                        injr.ReportType = "InjuryIllness" + idval.ToString();

                        CreateItems(jsonInitObj.getInjuryJson(injr));
                        //  App.DAUtil.SaveEmployee<InjuryIllnessReport>(injr);
                        break;
                    case "cabin":
                        //   CreateItems();
                        idval = new Random().Next(1, 1000);
                        CabibSafetyReport cd = (CabibSafetyReport)_viewobject;
                        cd.ReportType = "Cabin" + idval.ToString();
                        CreateItems(jsonInitObj.getCabinSfetyJson(cd));
                        // App.DAUtil.SaveEmployee<CabibSafetyReport>(cd);
                        break;
                }
            }

        }
     
        private void savedrafts_btn_Clicked(object sender, EventArgs e)
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

            // var v = safetyObject.EventTitle;
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
        protected  void CreateItems<U>(U reportObject) where U : class
        {
            try
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
                  
                    // var body = "{\"__metadata\":{\"type\":\"SP.Data.Operational_x005f_Hazard_x005f_Event_x005f_Register_x005f_04042018ListItem\"},\"Title_x0020_of_x0020_Event_Hazar\":\"" + empdetails + "\"}";
                   // SecurityModel sd = (SecurityModel)_viewobject;
                    //sd.ReportType = "Security" + idval.ToString();
                  
                   // jsonInitObj.getSecurity(reportObject);
                    // sd.DateOfEvent = null ;
                  
                    //contents.Headers.Add("Accept", "application/json");

                    var postResult = client.PostAsync("https://sptechnophiles.sharepoint.com/_api/web/lists/GetByTitle('Operational_Hazard_Event_Register_04042018')/items", contents).Result;
                    //var result = postResult.EnsureSuccessStatusCode();

                    if (!postResult.IsSuccessStatusCode)
                    {
                        // Unwrap the response and throw as an Api Exception:
                        var ex = OAuthHelper.CreateExceptionFromResponseErrors(postResult);

                    }
                    if (postResult.IsSuccessStatusCode)
                    {
                        DependencyService.Get<IMessage>().LongAlert("List updated successfully");
                        this.Navigation.PopToRootAsync();
                    }
                    else
                    {
                        //App.employees.Add(_viewobject);
                      //  FullReportTableModel fullRep = new FullReportTableModel();
                        DatatableData dt = new DatatableData();
                       // dt.Value = contents;
                        App.DAUtil.SaveEmployee<DatatableData>(dt);
                        DependencyService.Get<IMessage>().LongAlert("List data stored in local storage");
                    }
                }
                else
                {
                  
                    DatatableData dt = new DatatableData();
                    dt.Value = body;// contents.ToString();
                    App.DAUtil.SaveEmployee<DatatableData>(dt);

                    var vList = App.DAUtil.GetAllEmployees<DatatableData>("DatatableData1");
                    DependencyService.Get<IMessage>().LongAlert("List data stored in local storage");
                }
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
            if(FlightPhasepicker.SelectedIndex>0)
            airregis = FlightPhasepicker.Items.ElementAt(FlightPhasepicker.SelectedIndex);
        }
        
    }
}