using DevEnvAzure.Interfaces;
using DevEnvAzure.Models;
using PCLStorage;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttachmentView : ContentView
    {

        List<Attachment> Attachments;
        public AttachmentView()
        {
            InitializeComponent();
            Attachments = new List<Attachment>();
        }

        public async Task<bool> CheckAttachments(string attachmentInfo)
        {
            if (string.IsNullOrEmpty(attachmentInfo)) return true;

            string[] paths = SPUtility.GetPathsFromAttachemntInfo(attachmentInfo);
            var attachments = new List<Attachment>();

            foreach (string path in paths)
            {
                IFolder folder = await FileSystem.Current.LocalStorage.GetFolderAsync(Path.GetDirectoryName(path));
                var isFileExists = await folder.CheckExistsAsync(Path.GetFileName(path));

                if (isFileExists == ExistenceCheckResult.FileExists)
                {
                    var file = await FileSystem.Current.GetFileFromPathAsync(path);
                    attachments.Add(new Attachment { FileName = Path.GetFileName(path), FilePath = path });
                }
            }

            Attachments = attachments;
            BindAttachments();

            return paths.Length == Attachments.Count;
        }


        public List<Attachment> GetAttachments()
        {
            return Attachments;
        }

        public string GetAttachmentInfoAsString()
        {
            if (Attachments.Count == 0) return null;

            return String.Join(SPUtility.SEPARATOR, Attachments.Select(x => x.FilePath).ToList());
        }

        public async Task<Attachment> AskForAttachment(string selectedOption)
        {
            string fileName = $"Attachment-{DateTime.Now.ToString("yyyyMMddHHmmss")}.jpg";
            string[] options = { "Camera", "Gallery" };
            Attachment att = null;
            if (selectedOption == options[0])
            {
                att = await SPUtility.TakePhotoAsync();
                if (att != null)
                {
                    Attachments.Add(att);
                }

            }
            else if (selectedOption == options[1])
            {
                att = await SPUtility.PicPhotoAsync();
                if (att != null)
                {
                    Attachments.Add(att);
                }
            }

            BindAttachments();

            return att;
        }

        private void BindAttachments()
        {
            stkAttachmentList.Children.Clear();
            foreach (var item in Attachments)
            {
                Button btnAttachment = new Button() { Text = item.FileName, FontSize = 14, HorizontalOptions = LayoutOptions.Start };
                btnAttachment.CommandParameter = item.FileName;
                btnAttachment.Clicked += BtnAttachment_Clicked;

                Button btnDelete = new Button()
                {
                    Image = "delete.png",
                    HeightRequest = 14,
                    WidthRequest = 14,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    CommandParameter = item.FileName
                };

                btnDelete.Clicked += OnAttachmentDelete;

                StackLayout stk = new StackLayout
                {
                    Children = { btnAttachment, btnDelete },
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.StartAndExpand
                };

                stkAttachmentList.Children.Add(stk);
            }
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private async void BtnAttachment_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Attachments.Count == 0) return;
                string fileName = Convert.ToString(((Button)sender).CommandParameter);

                if (string.IsNullOrEmpty(fileName)) return;

                string filePath = Attachments.Find(x => x.FileName == fileName).FilePath;
                await Navigation.PushPopupAsync(new ImageViewerPopup(filePath));
            }
            catch (Exception)
            {

            }
        }

        private void OnAttachmentDelete(object sender, EventArgs e)
        {
            if (Attachments.Count == 0) return;
            string fileName = Convert.ToString(((Button)sender).CommandParameter);

            if (string.IsNullOrEmpty(fileName)) return;

            Attachments.Remove(Attachments.Find((obj) => obj.FileName == fileName));
            BindAttachments();
        }
    }
}