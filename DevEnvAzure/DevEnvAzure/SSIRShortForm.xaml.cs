using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
using System.IO;
using System.Collections.Generic;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SSIRShortForm : ContentPage
    {
        public dynamic _viewobject = null;
        public string _classname;
        public static int MORTypeID;
        Jsonpropertyinitialise jsonInitObj = new Jsonpropertyinitialise();
        AttachmentView _attachementView;
        List<OperatingPlan> OperatingPlans = null;
        public SSIRShortForm(object viewObject, string modelname)
        {
            try
            {
                BindingContext = viewObject;
                _classname = modelname;
                _viewobject = viewObject;
                InitializeComponent();

                _attachementView = new AttachmentView();
                stkAttachment.Children.Add(_attachementView);

                BindFlightNumbers();
                BindAricraftRegs();

                BindMORPicker();
                SetSubmitterInfo(viewObject);
            }
            catch (Exception ex)
            {

            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            string attachmentInfo = string.Empty;

            try
            {
                switch (_classname)
                {
                    case "safety":
                        attachmentInfo = ((Models.FlightSafetyReportModel)_viewobject).Attachments;
                        Title = "Flight Safety";
                        break;
                    case "ground":
                        attachmentInfo = ((Models.GroundSafetyReport)_viewobject).Attachments;
                        Title = "Ground Safety";
                        break;
                    case "cabin":
                        attachmentInfo = ((Models.CabibSafetyReport)_viewobject).Attachments;
                        Title = "Cabin Safety";
                        break;
                    case "Injury":
                        attachmentInfo = ((Models.InjuryIllnessReport)_viewobject).Attachments;
                        Title = "Injury Illness";
                        break;
                    case "security":
                        attachmentInfo = ((Models.SecurityModel)_viewobject).Attachments;
                        Title = "Security";
                        break;
                    case "fatigue":
                        attachmentInfo = ((Models.FatigueReport)_viewobject).Attachments;
                        Title = "Fatigue";
                        break;
                    default:
                        Title = char.ToUpper(_classname[0]) + _classname.Substring(1);
                        break;
                }

                var allFilesExists = await _attachementView.CheckAttachments(attachmentInfo);
                if (!allFilesExists)
                {
                    await DisplayAlert("Warning", SPUtility.ATTACHMENT_FILES_NOT_FOUND, "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }


        protected async void BindAricraftRegs()
        {
            string reg = GetPropValue(_viewobject, "AircraftRegistration");
            var regs = await SPUtility.GetAirCraftRegistrations();
            AircraftRegistrationpicker.ItemsSource = regs;
            AircraftRegistrationpicker.SelectedItem = reg;
        }

        ContentView _fullviewobj = null;
        private void Check_Clicked(object sender, XLabs.EventArgs<bool> e)
        {
            if (_fullviewobj == null)
            {
                switch (_classname)
                {
                    case "safety":
                        _fullviewobj = new FlightSafetyReportView((FlightSafetyReportModel)_viewobject);
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
        private async void Save_clicked(object sender, XLabs.EventArgs<bool> e)
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
                try
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
                            DataContracts.FlightSecuritySharepointData spData = null;
                            try
                            {
                                spData = jsonInitObj.getSecurity(sd);
                            }
                            catch (Exception ex)
                            {
                                await DisplayAlert("Please fill valid data", ex.Message, "Ok");
                                return;
                            }

                            CreateItems(spData, SPUtility.ReportType.Security);
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
                catch (Exception ex)
                {
                    await DisplayAlert("Please fill valid data", ex.Message, "Ok");
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
                        sf.Created = DateTime.Now;

                        if (FlightSafetyReportView.PeoplePickerCommander != null)
                            sf.CommandersEmail = FlightSafetyReportView.PeoplePickerCommander.Id.ToString();

                        if (FlightSafetyReportView.PeoplePickercrew1email != null)
                            sf.FlightCrew1 = FlightSafetyReportView.PeoplePickercrew1email.Id.ToString();

                        if (FlightSafetyReportView.PeoplePickercrew2email != null)
                            sf.FlightCrew2 = FlightSafetyReportView.PeoplePickercrew2email.Id.ToString();

                        sf.Attachments = _attachementView.GetAttachmentInfoAsString();
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
                        sd.Created = DateTime.Now;
                        sd.Attachments = _attachementView.GetAttachmentInfoAsString();
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
                        gd.Created = DateTime.Now;
                        gd.Attachments = _attachementView.GetAttachmentInfoAsString();
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
                        ft.Created = DateTime.Now;
                        ft.Attachments = _attachementView.GetAttachmentInfoAsString();
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
                        injr.Created = DateTime.Now;
                        injr.Attachments = _attachementView.GetAttachmentInfoAsString();
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
                        cd.Created = DateTime.Now;
                        cd.Attachments = _attachementView.GetAttachmentInfoAsString();
                        cd = App.DAUtil.SaveOrUpdate(cd);
                        if (cd != null)
                        {
                            _viewobject = cd;
                        }

                        break;
                }
                DependencyService.Get<IMessage>().ShortAlert(_viewobject.MOR);
                //DependencyService.Get<IMessage>().ShortAlert("Item drafted");
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
                    //eventAttachEntry.Text = y;
                }
                else
                {
                    //eventAttachEntry.Text = "";
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
            return SPUtility.IsConnected();
        }

        protected void CreateItems<U>(U reportObject, SPUtility.ReportType reportType) where U : class
        {
            return; 
            try
            {
                ToggleBusy(true);

                Device.BeginInvokeOnMainThread(async () =>
                {
                    var client = await OAuthHelper.GetHTTPClient();
                    var data = reportObject;

                    string body = "";
                    try
                    {
                        body = JsonConvert.SerializeObject(data, Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", ex.Message, "Ok");
                    }

                    if (string.IsNullOrEmpty(body))
                    {
                        await DisplayAlert("Error", "There is no content to send! - serializtion failed", "Ok");
                        return;
                    }

                    var contents = new StringContent(body);
                    contents.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;odata=verbose");
                    if (CheckConnection())
                    {
                        var postResult = await client.PostAsync(SPUtility.GetListURL(reportType), contents);

                        if (postResult.IsSuccessStatusCode)
                        {
                            lblLoading.Text = "Item created successfully." + Environment.NewLine;

                            var spData = JsonConvert.DeserializeObject<SPData>(postResult.Content.ReadAsStringAsync().Result,
                                new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
                            int itemId = spData.d.Id;

                            await Task.Delay(500);
                            await SendAttachments(itemId, reportType);

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
                        SPUtility.SaveOfflineItem(body, reportType, _attachementView.GetAttachmentInfoAsString());

                        await DisplayAlert("", "Item stored in local storage", "Ok");
                        MessagingCenter.Send<object>(this, "StartService");
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

        private string GetPropValue(object obj, string prop)
        {
            if (obj != null)
            {
                System.Reflection.PropertyInfo pi = _viewobject.GetType().GetProperty(prop);
                if (pi != null)
                {
                    return (string)(pi.GetValue(obj, null));
                }
            }

            return string.Empty;
        }

        private void SetSubmitterInfo(object obj)
        {
            if (obj != null && App.CurrentUser != null)
            {
                System.Reflection.PropertyInfo pi = obj.GetType().GetProperty("NameStaffNumber");
                if (pi != null)
                {
                    pi.SetValue(obj, App.CurrentUser.Name);
                    NameEntry.Text = App.CurrentUser.Name;
                }

                pi = obj.GetType().GetProperty("SubmitterEmail");
                if (pi != null)
                {
                    pi.SetValue(obj, App.CurrentUser.Email);
                    EmailEntry.Text = App.CurrentUser.Email;
                }
            }
        }

        private void BindMORSavedInfo()
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
        }

        protected async void BindFlightNumbers()
        {
            OperatingPlans = await SPUtility.GetOperatingPlans();
            string selectedItem = "";
            if (_viewobject != null)
            {
                System.Reflection.PropertyInfo pi = _viewobject.GetType().GetProperty("FlightNumber");
                if (pi != null)
                {
                    selectedItem = Convert.ToString(pi.GetValue(_viewobject));
                }
            }

            FlightNumberPicker.ItemsSource = OperatingPlans.Select(x => x.FlighNumber).ToArray();
            FlightNumberPicker.SelectedItem = selectedItem;
        }

        protected async void BindMORPicker()
        {
            if (!SPUtility.IsConnected())
            {
                BindMORSavedInfo();
                return;
            }

            var client = await OAuthHelper.GetHTTPClient();
            if (client == null) { return; }
            try
            {
                string url = SPUtility.GetListURL(SPUtility.ReportType.MORType);
                var result = await client.GetStringAsync(url);

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
                BindMORSavedInfo();
                var msg = "Unable to fetch MOR items: " + ex.Message;
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

        private void ToggleBusy(bool flag)
        {
            activityStack.IsVisible = flag;
            activityIndicator.IsRunning = flag;
        }

        private async void attachments_Clicked(object sender, EventArgs e)
        {
            string selectedOption = await DisplayActionSheet("Attachment", "Cancel", null, ClientConfiguration.Default.AttachmentOptions);
            try
            {
                await _attachementView.AskForAttachment(selectedOption);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private async Task SendAttachments(int itemId, SPUtility.ReportType reportType)
        {
            try
            {
                var attachments = _attachementView.GetAttachments();
                int filesSent = 0;
                if (attachments.Count > 0)
                {
                    foreach (var item in attachments)
                    {
                        string attachmentURL = string.Format("{0}({1})/AttachmentFiles/add(FileName='{2}')",
                            SPUtility.GetListURL(reportType), itemId, item.FileName);

                        item.SaveToURL = attachmentURL;

                        lblLoading.Text += "Sending - " + item.FileName + Environment.NewLine;

                        var attachemntResponse = await item.PostAttachment();
                        if (!attachemntResponse.IsSuccessStatusCode)
                        {
                            var msg = await attachemntResponse.Content.ReadAsStringAsync();
                            lblLoading.Text += "Failed - " + item.FileName + " - " + msg + Environment.NewLine;
                        }
                        else
                        {
                            filesSent++;
                            lblLoading.Text += "Sent - " + item.FileName + Environment.NewLine;
                            // let sharepoint to complete its task before sending a new one
                            await Task.Delay(1000);
                        }
                    }
                }

                if (filesSent == attachments.Count)
                {
                    await DisplayAlert("Success", "Item created successfully", "Ok");
                }
                else
                {
                    await DisplayAlert("Info", lblLoading.Text, "Ok");

                }

                MessagingCenter.Send(this, "home");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private void FlightNumberPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = Convert.ToString(FlightNumberPicker.SelectedItem);
            if (string.IsNullOrEmpty(selectedItem)) return;

            var selectedOperatingPlan = OperatingPlans.Find(x => x.FlighNumber == selectedItem);

            if (selectedOperatingPlan != null && _viewobject != null)
            {
                System.Reflection.PropertyInfo ds = _viewobject.GetType().GetProperty("DepartureStation");
                if (ds != null)
                {
                    string dep = selectedOperatingPlan.DepartureStation;
                    ds.SetValue(_viewobject, dep);
                    DepartStnEntry.Text = dep;
                }

                System.Reflection.PropertyInfo ars = _viewobject.GetType().GetProperty("ArrivalStation");
                if (ars != null)
                {
                    string arv = selectedOperatingPlan.ArrivalStation;
                    ars.SetValue(_viewobject, arv);
                    ArrivStnEntry.Text = arv;
                }
            }
        }
    }
}