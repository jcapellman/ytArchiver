using System;

using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using ytArchiver.Objects;
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

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.RunQueue();

            base.OnNavigatedTo(e);
        }

        private async void btnAddToQueue_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var status = ViewModel.AddDownloadToQueue();

            if (!status)
            {
                var dialog = new MessageDialog("Failed to add the video to the queue");

                await dialog.ShowAsync();
            }
        }

        private void btnRemoveQueue_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var item = (YTVideoItem)((Button)sender).DataContext;

            ViewModel.RemoveItem(item);
        }
    }
}