using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
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
                    if (string.IsNullOrEmpty(url))
                    {
                        if (string.IsNullOrEmpty(emp.Error))
                        {
                            emp.Error = "Invalid url";
                            App.DAUtil.Update(emp);
                        }

                        continue;
                    }

                    if (emp.InProgress) continue;

                    var contents = new StringContent(emp.Value);
                    emp.InProgress = true;
                    App.DAUtil.Update(emp);

                    contents.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;odata=verbose");
                    var postResult = await client.PostAsync(url, contents);
                    if (postResult.IsSuccessStatusCode)
                    {
                        count++;
                        App.DAUtil.Delete(emp);
                        App.offlineItems.Remove(emp);
                    }
                    else
                    {
                        emp.Error = postResult.ReasonPhrase + "-" + await postResult.Content.ReadAsStringAsync();
                        emp.InProgress = false;
                        App.DAUtil.Update(emp);

                        continue;
                    }
                }

                //if (count > 0)
                //{
                //    DependencyService.Get<IMessage>().ShortAlert(count + " item(s) updated successfully");
                //}
                //else
                //{
                //    DependencyService.Get<IMessage>().ShortAlert("Item stored in local storage");
                //}

            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert("Upload Error");
            }
        }
    }
}
