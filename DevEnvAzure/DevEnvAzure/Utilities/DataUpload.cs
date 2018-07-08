using DevEnvAzure.Model;
using DevEnvAzure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;
using static DevEnvAzure.SPUtility;

namespace DevEnvAzure
{
    public class DataUpload
    {
        public static async Task<int> CreateItemsOffline(List<OfflineItem> items)
        {
            var client = await OAuthHelper.GetHTTPClient();

            try
            {
                int count = 0;
                foreach (OfflineItem oItem in items)
                {
                    string url = GetListURL((ReportType)oItem.ReportType);
                    if (string.IsNullOrEmpty(url))
                    {
                        if (string.IsNullOrEmpty(oItem.Error))
                        {
                            oItem.Error = "Invalid url";
                            App.DAUtil.Update(oItem);
                        }

                        continue;
                    }

                    if (oItem.InProgress) continue;

                    var contents = new StringContent(oItem.Value);
                    oItem.InProgress = true;
                    App.DAUtil.Update(oItem);

                    contents.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;odata=verbose");
                    var postResult = await client.PostAsync(url, contents);
                    if (postResult.IsSuccessStatusCode)
                    {
                        string[] paths = SPUtility.GetPathsFromAttachemntInfo(oItem.Attachments);
                        foreach (string path in paths)
                        {
                            if (string.IsNullOrEmpty(path)) continue;

                            Attachment attachment = new Attachment(path);

                            var spData = JsonConvert.DeserializeObject<SPData>(postResult.Content.ReadAsStringAsync().Result,
                               new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
                            int itemId = spData.d.Id;

                            string attachmentURL = string.Format("{0}({1})/AttachmentFiles/add(FileName='{2}')",
                                        SPUtility.GetListURL(SPUtility.ReportType.Kaizen), itemId, attachment.FileName);

                            Stream stream = await attachment.GetStream();
                            if (stream == null)
                            {
                                // What to do if a file not found?
                                continue;
                            }

                            var attachemntResponse = await SPUtility.SaveAttachment(attachmentURL, stream);
                            if (!attachemntResponse.IsSuccessStatusCode)
                            {
                                // what if something fails?
                                //var msg = await attachemntResponse.Content.ReadAsStringAsync();
                            }

                        }

                        count++;
                        App.DAUtil.Delete(oItem);
                        App.offlineItems.Remove(oItem);
                    }
                    else
                    {
                        oItem.Error = postResult.ReasonPhrase + "-" + await postResult.Content.ReadAsStringAsync();
                        oItem.InProgress = false;
                        App.DAUtil.Update(oItem);

                        continue;
                    }
                }

                return count;
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().LongAlert($"Upload Error - {ex.Message}");
            }

            return 0;
        }
    }
}
