
using DevEnvAzure.DataContracts;
using DevEnvAzure.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Forms;

namespace DevEnvAzure
{
    public class NumericValidationTriggerAction : TriggerAction<Entry>
    {
        protected override void Invoke(Entry entry)
        {
            double result;
            bool isValid = Double.TryParse(entry.Text, out result);
            entry.TextColor = isValid ? Color.Default : Color.Red;
            if (!isValid)
            {
                DependencyService.Get<IMessage>().LongAlert("Please provide a number value");
                entry.Text = "";
                entry.Focus();
            }
        }

    }
    public partial class HomePage : ContentPage, INotifyPropertyChanged
    {
        const string SPRootURL = "https://sptechnophiles.sharepoint.com/_api/web/lists/";
        private bool isLoading;
        public event PropertyChangedEventHandler PropertyChangede;
        public void RaisePropertyChanged(string propName)
        {
            if (PropertyChangede != null)
            {
                PropertyChangede(this, new PropertyChangedEventArgs(propName));
            }
        }
        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                Plugin.Connectivity.CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
            }
            catch
            {

            }
            //  UpdateStatus();
        }
        public bool IsLoading
        {
            get
            {
                return this.isLoading;
            }

            set
            {
                this.isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
        private void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            if (e.IsConnected)
            {
               var eValue = App.DAUtil.GetAllEmployees<Employee>("Employee");
                if (eValue != null && eValue.Count > 0)
                {
                   DataUpload.CreateItemsOffline(eValue);
                }
                DependencyService.Get<IMessage>().LongAlert("Network Connection detected");
            }
            // var answer =  DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
        }

        public HomePage()//(string username)
        {
            try
            {
                // IsLoading = false;
                //  BindingContext = this;
                InitializeComponent();
                FetchListItemsofdept();
                if (CheckConnection())
                {
                    var eValue = App.DAUtil.GetAllEmployees<Employee>("Employee");
                    if (eValue.Count > 0)
                    {
                        DataUpload.CreateItemsOffline(eValue);
                    }
                }
                //   IsLoading = false;
                // BindingContext = this;

            }
            catch (Exception ex)
            {

            }
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

        protected async void FetchListItemsofdept()
        {
            try
            {
                var client = GetHTTPClient();
                if (client == null) { return; }

                var result = await client.GetStringAsync(SPRootURL + "GetByTitle('Departments')/items");

                //  var data = result.Length;
                if (result != null)
                {
                    var spData = JsonConvert.DeserializeObject<SPData>(result, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
                    foreach (var val in spData.d.results)
                    {
                        deptemp.Items.Add(val.DepartmentName);
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = "Unable to fetch list items. " + ex.Message;
            }
        }

        protected async void FetchListItems()
        {
            var client = GetHTTPClient();
            if (client == null) { return; }
            try
            {
                var result = await client.GetStringAsync(SPRootURL + "GetByTitle('TestForm')/items");
                var data = result.Length;
                var spData = JsonConvert.DeserializeObject<SPData>(result, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
            }
            catch (Exception ex)
            {
                var msg = "Unable to fetch list items. " + ex.Message;

            }
        }

        private bool CheckConnection()
        {
            return CrossConnectivity.Current.IsConnected;

            //var networkConnection = DependencyService.Get<INetworkConnection>();
            //networkConnection.CheckNetworkConnection();
            //return networkConnection.IsConnected;
        }

        protected async void CreateItems()
        {
            var empName = empN.Text;
            var gender = gendEmp.Items[gendEmp.SelectedIndex];
            var empage = EAge.Text;
            var date = dtPicker.Date;
            var dptname = deptemp.SelectedIndex + 1;
            var actemp = actEmp.Items[actEmp.SelectedIndex];
            var empdetails = "developer";
            var sal = ESal.Text;
            if (actemp == "Yes")
            {
                actemp = "true";
            }
            else
            {
                actemp = "false";
            }

            try
            {
                if (CheckConnection())
                {
                    var client = GetHTTPClient();
                    //var body = "{\"__metadata\":{\"type\":\"SP.Data.TestFormListItem\"},\"Employee_x0020_Details\":\"" + empdetails + "\",\"DepartmentId\":\"" + dptname + "\",\"Salary\":\"" + sal +
                    //"\",\"Active_x0020_Employee\":\"" + actemp + "\",\"Joining_x0020_Date\":\"" + date + "\",\"Employee_x0020_Age\":\"" + empage + 
                    //"\",\"Employee_x0020_Name\":\"" + empName + "\",\"Gender\": \"" + gender + "\"}";
                    var data = new EmployeeInformation();
                    data.Employee_x0020_Name = empName;
                    data.Employee_x0020_Details = empdetails;
                    data.DepartmentId = dptname;
                    data.Salary = Convert.ToInt32(sal);
                    data.Active_x0020_Employee = actemp == "Yes" ? true : false;
                    data.Joining_x0020_Date = date;
                    data.Employee_x0020_Age = Convert.ToInt32(empage);

                    var body = JsonConvert.SerializeObject(data, Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
                    var contents = new StringContent(body);
                    contents.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;odata=verbose");
                    //contents.Headers.Add("Accept", "application/json");

                    var postResult = client.PostAsync("https://sptechnophiles.sharepoint.com/_api/web/lists/GetByTitle('TestForm')/items", contents).Result;
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
                        var vEmployee = new Employee()
                        {
                            vEmpName = empName,
                            vEmpAge = empage,
                            vEmpDept = dptname.ToString(),
                            vEmpDetails = empdetails,
                            vEmpGender = gender,
                            vEmpSal = sal,
                            vEmpDate = date,
                            vEmpActive = actemp
                        };
                        App.employees.Add(vEmployee);
                        App.DAUtil.SaveEmployee(vEmployee);
                        DependencyService.Get<IMessage>().LongAlert("List data stored in local storage");
                    }
                }
                else
                {
                    var vEmployee = new Employee()
                    {
                        vEmpName = empName,
                        vEmpAge = empage,
                        vEmpDept = dptname.ToString(),
                        vEmpDetails = empdetails,
                        vEmpGender = gender,
                        vEmpSal = sal,
                        vEmpDate = date,
                        vEmpActive = actemp
                    };
                    App.employees.Add(vEmployee);
                    App.DAUtil.SaveEmployee(vEmployee);
                    var vList = App.DAUtil.GetAllEmployees<Employee>("Employee");
                    DependencyService.Get<IMessage>().LongAlert("List data stored in local storage");
                }

                await Navigation.PushAsync(new DraftsPage());
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

        protected async void CreateList()
        {
            try
            {


                //var client = new HttpClient();
                //var request = new HttpRequestMessage(HttpMethod.Get, "https://sptechnophiles.sharepoint.com/_api/web/lists");
                //request.Headers.Authorization = new AuthenticationHeaderValue(App.AuthenticationResult.AccessTokenType, App.AuthenticationResult.AccessToken);
                //var response = await client.SendAsync(request);

                //var content = await response.Content.ReadAsStringAsync();


                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(App.AuthenticationResponse.token_type, App.AuthenticationResponse.access_token);
                // client.DefaultRequestHeaders.Add("Authorization", App.AuthenticationResult.AccessTokenType + App.AuthenticationResult.AccessToken);
                var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
                mediaType.Parameters.Add(new NameValueHeaderValue("odata", "verbose"));
                client.DefaultRequestHeaders.Accept.Add(mediaType);

                var body = "{\"__metadata\":{\"type\":\"SP.List\"},\"AllowContentTypes\":true,\"BaseTemplate\":107,\"ContentTypesEnabled\":true,\"Description\":\"Tasks by Xamarin.Android\",\"Title\":\"TasksByAndroid\"}";

                var contents = new StringContent(body);
                contents.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;odata=verbose");


                var postResult = await client.PostAsync("https://sptechnophiles.sharepoint.com/_api/web/lists", contents);
                var result = postResult.EnsureSuccessStatusCode();
                //   Toast.MakeText(this, "List created successfully! Seeding tasks.", ToastLength.Long).Show();

                // return true;
            }
            catch (Exception ex)
            {
                // Toast.MakeText(this, "List already exists! Fetching tasks.", ToastLength.Long).Show();
                //  return false;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            CreateItems();
        }
    }
}
