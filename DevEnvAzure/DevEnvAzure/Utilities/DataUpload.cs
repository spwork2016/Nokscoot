using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;
using static DevEnvAzure.SPUtility;

namespace DevEnvAzure
{
    public class DataUpload
    {
        const string SPRootURL = "https://sptechnophiles.sharepoint.com/_api/web/lists/";

        public static async void CreateItemsOffline(List<OfflineItem> lstEmp)
        {
            var client = await OAuthHelper.GetHTTPClient();

            try
            {
                int count = 0;
                foreach (var emp in lstEmp)
                {
                    string url = GetListURL((ReportType)emp.ReportType);
                    var contents = new StringContent(emp.Value);
                    //contents.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;odata=verbose");
                    var postResult = await client.PostAsync(url, contents);
                    if (postResult.IsSuccessStatusCode)
                    {
                        count++;
                        App.DAUtil.Delete(emp);
                        App.offlineItems.Remove(emp);
                    }
                }

                if (count > 0)
                {
                    DependencyService.Get<IMessage>().ShortAlert(count + " item(s) updated successfully");
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert("Item stored in local storage");
                }

            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert("Upload Error");
            }
        }
    }
}
