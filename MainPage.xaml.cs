using System;

using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

using ytArchiver.ViewModel;

namespace ytArchiver
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel ViewModel => (MainViewModel)DataContext;

        public MainPage()
        {
            this.InitializeComponent();

            DataContext = new MainViewModel();
        }

        private async void btnAddToQueue_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var messageStr = "Unknown";

            var status = await ViewModel.DownloadAsync();

            switch (status)
            {
                case Enums.VideoStatus.DOWNLOADED:
                    messageStr = "Downloaded successfully";
                    break;
                case Enums.VideoStatus.INVALID_URL:
                    messageStr = "Invalid YT URL provided, please enter a new URL";
                    break;
                case Enums.VideoStatus.ERROR:
                    messageStr = "There was an unexpected error while downloading";
                    break;
            }

            var dialog = new MessageDialog(messageStr);

            await dialog.ShowAsync();
        }
    }
}