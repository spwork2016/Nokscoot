using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DevEnvAzure.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SSIRShortForm : ContentPage
    {
        object _viewobject = null;
        public  string _classname;
      
        public SSIRShortForm(object viewObject, string modelname)
        {
            this.BindingContext = viewObject;
            _classname = modelname;
            _viewobject = viewObject;
            InitializeComponent();
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
            if (Formcheck.Checked == true)
            {
                Stacklay2.Children.Add(_fullviewobj);
            }
            else
            {
                Stacklay2.Children.Remove(_fullviewobj);
            }
        }
        private void Save_clicked(object sender, XLabs.EventArgs<bool> e)
        {
            switch (_classname)
            {
                case "safety":
                    // App.safetyReport.Add((SafetyReportModel)_viewobject);
                    App.DAUtil.SaveEmployee((SafetyReportModel)_viewobject);
                    break;
                case "security":
                    //  App.security.Add((SecurityModel)_viewobject);
                    CreateItems();
                   // App.DAUtil.SaveEmployee((SecurityModel)_viewobject);
                    //  _fullviewobj = new securityReportView();
                    break;
                case "ground":
                    // App.groundSafety.Add((GroundSafetyReport)_viewobject);
                    App.DAUtil.SaveEmployee((GroundSafetyReport)_viewobject);
                    // _fullviewobj = new GroundSafetyReportView();
                    break;
                case "fatigue":
                    // App.fatigue.Add((FatigueReport)_viewobject);
                    App.DAUtil.SaveEmployee((FatigueReport)_viewobject);
                    //  _fullviewobj = new FatigueReportView();
                    break;
                case "Injury":
                    //  App.injuryIllness.Add((InjuryIllnessReport)_viewobject);
                    App.DAUtil.SaveEmployee((InjuryIllnessReport)_viewobject);
                    // _fullviewobj = new InjuryIllnessReportView();
                    break;
                case "cabin":
                    //    App.cabinSafety.Add((CabibSafetyReport)_viewobject);
                    App.DAUtil.SaveEmployee((CabibSafetyReport)_viewobject);
                    // _fullviewobj = new CabinSafetyReportView();
                    break;
            }

        }
        private void savedrafts_btn_Clicked(object sender, EventArgs e)
        {
            switch (_classname)
            {
                case "safety":
                    // App.safetyReport.Add((SafetyReportModel)_viewobject);
                    App.DAUtil.SaveEmployee((SafetyReportModel)_viewobject);
                    break;
                case "security":
                    //  App.security.Add((SecurityModel)_viewobject);
                   
                   App.DAUtil.SaveEmployee((SecurityModel)_viewobject);
                    //  _fullviewobj = new securityReportView();
                    break;
                case "ground":
                    // App.groundSafety.Add((GroundSafetyReport)_viewobject);
                    App.DAUtil.SaveEmployee((GroundSafetyReport)_viewobject);
                    // _fullviewobj = new GroundSafetyReportView();
                    break;
                case "fatigue":
                    // App.fatigue.Add((FatigueReport)_viewobject);
                    App.DAUtil.SaveEmployee((FatigueReport)_viewobject);
                    //  _fullviewobj = new FatigueReportView();
                    break;
                case "Injury":
                    //  App.injuryIllness.Add((InjuryIllnessReport)_viewobject);
                    App.DAUtil.SaveEmployee((InjuryIllnessReport)_viewobject);
                    // _fullviewobj = new InjuryIllnessReportView();
                    break;
                case "cabin":
                    //    App.cabinSafety.Add((CabibSafetyReport)_viewobject);
                    App.DAUtil.SaveEmployee((CabibSafetyReport)_viewobject);
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
                    var y = fileData.FilePath;
                    MORpicker.Text = y;
                }
                else
                {
                    MORpicker.Text = "";
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
        protected async void CreateItems()
        {
            //var empName = empN.Text;
            //var gender = gendEmp.Items[gendEmp.SelectedIndex];
            //var empage = EAge.Text;
            //var date = dtPicker.Date;
            //var dptname = deptemp.SelectedIndex + 1;
            //var actemp = actEmp.Items[actEmp.SelectedIndex];
            //var empdetails = "developer";
            //var sal = ESal.Text;
            //if (actemp == "Yes")
            //{
            //    actemp = "true";
            //}
            //else
            //{
            //    actemp = "false";
            //}

            try
            {
                if (CheckConnection())
                {
                    var empdetails = "safety";
                    var client = GetHTTPClient();
                   // var body = "{\"__metadata\":{\"type\":\"SP.Data.TestFormListItem\"},\"Type_x0020_of_x0020_Report\":\"" + empdetails + "\"}";
                    //var body = "{\"__metadata\":{\"type\":\"SP.Data.TestFormListItem\"},\"Employee_x0020_Details\":\"" + empdetails + "\",\"DepartmentId\":\"" + dptname + "\",\"Salary\":\"" + sal +
                    //"\",\"Active_x0020_Employee\":\"" + actemp + "\",\"Joining_x0020_Date\":\"" + date + "\",\"Employee_x0020_Age\":\"" + empage + 
                    //"\",\"Employee_x0020_Name\":\"" + empName + "\",\"Gender\": \"" + gender + "\"}";
                    var data = _viewobject;

                    //data.Employee_x0020_Name = empName;
                    //data.Employee_x0020_Details = empdetails;
                    //data.DepartmentId = dptname;
                    //data.Salary = Convert.ToInt32(sal);
                    //data.Active_x0020_Employee = actemp == "Yes" ? true : false;
                    //data.Joining_x0020_Date = date;
                    //data.Employee_x0020_Age = Convert.ToInt32(empage);

                    var body = JsonConvert.SerializeObject(data, Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
                    var contents = new StringContent(body);
                    contents.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;odata=verbose");
                    //contents.Headers.Add("Accept", "application/json");

                    var postResult = client.PostAsync("https://nok365.sharepoint.com/sites/Nokscoot/SSQServices/WorkBench/_api/web/lists/GetByTitle('Operational_Hazard_Event_Register 25 Jan 18')/items", contents).Result;
                    //var result = postResult.EnsureSuccessStatusCode();

                    if (!postResult.IsSuccessStatusCode)
                    {
                        // Unwrap the response and throw as an Api Exception:
                        var ex = OAuthHelper.CreateExceptionFromResponseErrors(postResult);

                    }
                    if (postResult.IsSuccessStatusCode)
                    {
                        //var vEmployee = new Employee()
                        //{
                        //    vEmpName = empName,
                        //    vEmpAge = empage,
                        //    vEmpDept = dptname.ToString(),
                        //    vEmpDetails = empdetails,
                        //    vEmpGender = gender,
                        //    vEmpSal = sal,
                        //    vEmpDate = date,
                        //    vEmpActive = actemp
                        //};
                        //DraftsPage.employees.Add(vEmployee);
                        DependencyService.Get<IMessage>().LongAlert("List updated successfully");
                    }
                    else
                    {
                        //var vEmployee = new Employee()
                        //{
                        //    vEmpName = empName,
                        //    vEmpAge = empage,
                        //    vEmpDept = dptname.ToString(),
                        //    vEmpDetails = empdetails,
                        //    vEmpGender = gender,
                        //    vEmpSal = sal,
                        //    vEmpDate = date,
                        //    vEmpActive = actemp
                        //};
                        //App.employees.Add(_viewobject);
                        App.DAUtil.SaveEmployee(_viewobject);
                        DependencyService.Get<IMessage>().LongAlert("List data stored in local storage");
                    }
                }
                else
                {
                    //var vEmployee = new Employee()
                    //{
                    //    vEmpName = empName,
                    //    vEmpAge = empage,
                    //    vEmpDept = dptname.ToString(),
                    //    vEmpDetails = empdetails,
                    //    vEmpGender = gender,
                    //    vEmpSal = sal,
                    //    vEmpDate = date,
                    //    vEmpActive = actemp
                    //};
                    //App.employees.Add(vEmployee);
                    App.DAUtil.SaveEmployee(_viewobject);
                    
                    var vList = App.DAUtil.GetAllEmployees<SecurityModel>("SecurityModel");
                    DependencyService.Get<IMessage>().LongAlert("List data stored in local storage");
                }

               // await Navigation.PushAsync(new DraftsPage());
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

       
    }
}