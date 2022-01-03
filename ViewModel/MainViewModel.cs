using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Windows.UI.Xaml;

using ytArchiver.Objects;

namespace ytArchiver.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Visibility _downloading;

        public Visibility Downloading
        {
            get => _downloading;

            set
            {
                _downloading = value;
                OnPropertyChanged();
            }
        }

        private string _videoURL;

        public string VideoURL
        {
            get => _videoURL;

            set
            {
                _videoURL = value;

                OnPropertyChanged();

                EnableDownload = !string.IsNullOrEmpty(value);
            }
        }

        private bool _enableDownload;

        public bool EnableDownload
        {
            get => _enableDownload;
            
            set
            {
                _enableDownload = value;

                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            Downloading = Visibility.Collapsed;
        }

        public async Task<bool> DownloadAsync()
        {
            Downloading = Visibility.Visible;

            var videoItem = new YTVideoItem(VideoURL);

            if (videoItem.Status == Enums.VideoStatus.ERROR)
            {
                VideoURL = string.Empty;

                Downloading = Visibility.Collapsed;

                return false;
            }

            VideoURL = string.Empty;

            EnableDownload = false;

            var result = await videoItem.Download();

            Downloading = Visibility.Collapsed;

            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}