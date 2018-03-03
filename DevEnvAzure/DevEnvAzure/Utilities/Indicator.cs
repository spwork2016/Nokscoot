using Xamarin.Forms;

namespace DevEnvAzure
{
    public static class Indicator
    {
        public static void AddIndicator(this ContentPage page)
        {
            var ctn = page.Content;

            //var grid = new Grid();
            //grid.Children.Add(content);
            //var gridProgress = new Grid { BackgroundColor = Color.FromHex("#64FFE0B2"), Padding = new Thickness(50) };
            //gridProgress.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            //gridProgress.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            //gridProgress.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            //gridProgress.SetBinding(VisualElement.IsVisibleProperty, "IsWorking");
            //var activity = new ActivityIndicator
            //{
            //    IsEnabled = true,
            //    IsVisible = false,
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    IsRunning = true
            //};
            //gridProgress.Children.Add(activity, 0, 1);
            //grid.Children.Add(gridProgress);
            //page.Content = grid;

            var overlay = new AbsoluteLayout();
            var content = new StackLayout();
            var loadingIndicator = new ActivityIndicator();
            AbsoluteLayout.SetLayoutFlags(content, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(content, new Rectangle(0f, 0f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            AbsoluteLayout.SetLayoutFlags(loadingIndicator, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(loadingIndicator, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            overlay.Children.Add(content);
            overlay.Children.Add(loadingIndicator);
        }
    }
}
