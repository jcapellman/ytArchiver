using Windows.UI.Xaml.Controls;

using ytArchiver.ViewModel;

namespace ytArchiver
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            DataContext = new MainViewModel();
        }
    }
}