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
                var att = new Attachment(path);
                if (await att.Exists())
                {
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

                // // Commented as the delete func moved to pop-over - just as a backup, I have commented this
                //Button btnDelete = new Button()
                //{
                //    Text = "X",
                //    FontSize = 14,
                //    HeightRequest = 20,
                //    WidthRequest = 50,
                //    BorderColor = Color.FromHex("FFFFFF"),
                //    BackgroundColor = Color.FromHex("FFFFFF"),
                //    HorizontalOptions = LayoutOptions.EndAndExpand,
                //    CommandParameter = item.FileName
                //};

                //btnDelete.Clicked += OnAttachmentDelete;

                StackLayout stk = new StackLayout
                {
                    Children = { btnAttachment },
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.StartAndExpand
                };

                stkAttachmentList.Children.Add(stk);
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
                MessagingCenter.Subscribe<ImageViewerPopup>(this, "DELETE_ATTACHEMNT", (fName) =>
                {
                    Attachments.Remove(Attachments.Find((obj) => obj.FileName == fileName));
                    BindAttachments();
                });

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