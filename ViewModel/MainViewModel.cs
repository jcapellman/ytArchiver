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

        private Visibility _infoVisibility;

        public Visibility InfoVisibility
        {
            get => _infoVisibility;

            set
            {
                _infoVisibility = value;

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

        private YTVideoItem _videoItem;

        public YTVideoItem VideoItem
        {
            get => _videoItem;

            set
            {
                _videoItem = value;
                OnPropertyChanged();
            }
        }

        private bool _DownloadSuccessful;

        public bool DownloadSuccessful
        {
            get => _DownloadSuccessful;

            set
            {
                _DownloadSuccessful = value;

                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            Downloading = Visibility.Collapsed;
            InfoVisibility = Visibility.Collapsed;
            DownloadSuccessful = false;
        }

        public async Task<Enums.VideoStatus> DownloadAsync()
        {
            DownloadSuccessful = false;

            Downloading = Visibility.Visible;

            VideoItem = new YTVideoItem(VideoURL);

            if (VideoItem.Status == Enums.VideoStatus.ERROR || VideoItem.Status == Enums.VideoStatus.INVALID_URL)
            {
                VideoURL = string.Empty;

                Downloading = Visibility.Collapsed;

                return VideoItem.Status;
            }

            InfoVisibility = Visibility.Visible;

            VideoURL = string.Empty;

            EnableDownload = false;

            var result = await VideoItem.Download();

            Downloading = Visibility.Collapsed;

            DownloadSuccessful = true;

            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}