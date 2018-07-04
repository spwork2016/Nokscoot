using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevEnvAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttachmentView : ContentView
    {
        Dictionary<string, Stream> Attachments;
        public AttachmentView()
        {
            InitializeComponent();
            Attachments = new Dictionary<string, Stream>();
        }

        public Dictionary<string, Stream> GetAttachments()
        {
            return Attachments;
        }

        public async void AskForAttachment(string selectedOption)
        {
            string fileName = $"Attachment-{DateTime.Now.ToString("yyyyMMddHHmmss")}.jpg";
            string[] options = { "Camera", "Gallery" };
            Stream stream = null;
            if (selectedOption == options[0])
            {
                stream = await SPUtility.TakePhotoAsync();
                if (stream != null)
                {
                    Attachments.Add(fileName, stream);
                }

            }
            else if (selectedOption == options[1])
            {
                stream = await SPUtility.PicPhotoAsync();
                if (stream != null)
                {
                    Attachments.Add(fileName, stream);
                }
            }

            BindAttachments();

        }

        private void BindAttachments()
        {
            stkAttachmentList.Children.Clear();
            foreach (var item in Attachments)
            {
                Label lbl = new Label() { Text = item.Key, FontSize = 14, HorizontalOptions = LayoutOptions.Start };
                Button btn = new Button()
                {
                    Image = "delete.png",
                    HeightRequest = 14,
                    WidthRequest = 14,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    CommandParameter = item.Key
                };

                btn.Clicked += OnAttachmentDelete;

                StackLayout stk = new StackLayout
                {
                    Children = { lbl, btn },
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.StartAndExpand
                };

                stkAttachmentList.Children.Add(stk);
            }
        }

        private void OnAttachmentDelete(object sender, EventArgs e)
        {
            if (Attachments.Count == 0) return;
            string key = Convert.ToString(((Button)sender).CommandParameter);

            if (string.IsNullOrEmpty(key)) return;

            Attachments.Remove(key);
            BindAttachments();
        }
    }
}