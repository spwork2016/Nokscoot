using System;
using System.IO;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace DevEnvAzure
{
    public partial class ImageViewerPopup : PopupPage
    {
        public ImageViewerPopup(string filePath)
        {
            InitializeComponent();
            image.Source = ImageSource.FromFile(filePath);
        }

        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }

        private async void OnDelete(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "DELETE_ATTACHEMNT");
            await PopupNavigation.PopAsync();
        }
    }
}
