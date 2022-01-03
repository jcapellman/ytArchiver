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
            var status = await ViewModel.DownloadAsync();

            if (!status)
            {
                var dialog = new MessageDialog("Failed to download");

                await dialog.ShowAsync();
            } else
            {
                var dialog = new MessageDialog("Download successful");

                await dialog.ShowAsync();
            }
        }
    }
}