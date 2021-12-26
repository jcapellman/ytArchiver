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

        private void btnAddToQueue_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var status = ViewModel.AddDownloadToQueue();

            if (!status)
            {
                // TODO: Message Box error
            }
        }
    }
}