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
using System.Linq;
using static DevEnvAzure.SPUtility;

namespace DevEnvAzure
{
    public class DataUpload
    {
        public static void RefreshofflineItem(OfflineItem item)
        {
            var found = App.offlineItems.FirstOrDefault(x => x.ContentID == item.ContentID);
            int i = App.offlineItems.IndexOf(found);
            App.offlineItems[i] = item;

            App.DAUtil.Update(item);
        }

        public static void RemoveOfflineItem(OfflineItem oItem)
        {
            App.offlineItems.Remove(oItem);
            App.DAUtil.Delete(oItem);
        }

        public static async Task<int> CreateItemsOffline(List<OfflineItem> items)
        {
            var client = await OAuthHelper.GetHTTPClient();

            try
            {
                int count = 0;
                foreach (OfflineItem oItem in items)
                {
                    try
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

                        if (!string.IsNullOrEmpty(oItem.Error)) continue;

                        var contents = new StringContent(oItem.Value);
                        oItem.InProgress = true;
                        RefreshofflineItem(oItem);

                        contents.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;odata=verbose");
                        var postResult = await client.PostAsync(url, contents);
                        if (postResult.IsSuccessStatusCode)
                        {
                            string[] paths = GetPathsFromAttachemntInfo(oItem.Attachments);
                            if (paths == null)
                            {
                                count++;
                                RemoveOfflineItem(oItem);
                                continue;
                            }

                            foreach (string path in paths)
                            {
                                if (string.IsNullOrEmpty(path)) continue;

                                try
                                {
                                    var spData = JsonConvert.DeserializeObject<SPData>(postResult.Content.ReadAsStringAsync().Result,
                                    new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });

                                    Attachment attachment = new Attachment(path);

                                    int itemId = spData.d.Id;
                                    string attachmentURL = string.Format("{0}({1})/AttachmentFiles/add(FileName='{2}')",
                                             SPUtility.GetListURL((ReportType)oItem.ReportType), itemId, attachment.FileName);

                                    attachment.SaveToURL = attachmentURL;

                                    await Task.Delay(1000);
                                    App.attachments.Add(attachment);
                                }
                                catch (Exception ex)
                                {
                                    oItem.Error = $"Error - {ex.Message}";
                                    oItem.InProgress = false;
                                    RefreshofflineItem(oItem);

                                    continue;
                                }
                            }

                            count++;
                            RemoveOfflineItem(oItem);
                        }
                        else
                        {
                            oItem.Error = postResult.ReasonPhrase + "-" + await postResult.Content.ReadAsStringAsync();
                            oItem.InProgress = false;
                            RefreshofflineItem(oItem);

                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        oItem.InProgress = false;
                        oItem.Error = $"{ex.Message + Environment.NewLine} Info - + {ex.StackTrace}";
                        RefreshofflineItem(oItem);
                    }
                }

                return count;
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().LongAlert($"Offline items failed - {ex.Message} - {ex.StackTrace}");
            }

            return 0;
        }
    }
}
