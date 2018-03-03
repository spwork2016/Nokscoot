using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Forms;

namespace DevEnvAzure
{
   public class DataUpload
    {
        const string SPRootURL = "https://nok365.sharepoint.com/sites/Nokscoot/SSQServices/WorkBench/_api/web/lists/";

        private static HttpClient GetHTTPClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(App.AuthenticationResponse.token_type, App.AuthenticationResponse.access_token);
            MediaTypeWithQualityHeaderValue mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            mediaType.Parameters.Add(new NameValueHeaderValue("odata", "verbose"));
            client.DefaultRequestHeaders.Accept.Add(mediaType);

            return client;
        }

        private static bool CheckConnection()
        {
            var networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.CheckNetworkConnection();
            return networkConnection.IsConnected;
        }

        public static async void CreateItemsOffline(List<Employee> lstEmp)
        {
            var client = GetHTTPClient();

            try
            {
                int count = 0;
                foreach (var emp in lstEmp)
                {


                    var body = "{\"__metadata\":{\"type\":\"SP.Data.TestFormListItem\"},\"Employee_x0020_Details\":\"" + emp.vEmpDetails + "\",\"DepartmentId\":\"" + emp.vEmpDept + "\",\"Salary\":\"" + emp.vEmpSal + "\",\"Active_x0020_Employee\":\"" + emp.vEmpActive + "\",\"Joining_x0020_Date\":\"" + emp.vEmpDate + "\",\"Employee_x0020_Age\":\"" + emp.vEmpAge + "\",\"Employee_x0020_Name\":\"" + emp.vEmpName + "\",\"Gender\": \"" + emp.vEmpGender + "\"}";
                    var contents = new StringContent(body);
                    contents.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;odata=verbose");

                    var postResult = await client.PostAsync(SPRootURL + "GetByTitle('TestForm')/items", contents);
                    var result = postResult.EnsureSuccessStatusCode();
                    if (result.IsSuccessStatusCode)
                    {
                        count++;
                        App.DAUtil.DeleteEmployee(emp);
                        App.employees.Remove(emp);
                    }
                }

                if (count > 0)
                {
                    DependencyService.Get<IMessage>().LongAlert(count + " item(s) updated successfully");
                }
                else
                {
                    DependencyService.Get<IMessage>().LongAlert("List data stored in local storage");
                }

            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert("Upload Error");
            }
        }
    }
}
