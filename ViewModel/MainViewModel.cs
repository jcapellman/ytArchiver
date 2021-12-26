using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using ytArchiver.Objects;

namespace ytArchiver.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<YTVideoItem> _videoItems;

        public ObservableCollection<YTVideoItem> VideoItems
        {
            get => _videoItems;

            set
            {
                _videoItems = value;

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
            VideoItems = new ObservableCollection<YTVideoItem>();
        }

        public bool AddDownloadToQueue()
        {
            var videoItem = new YTVideoItem(VideoURL, System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyVideos));

            if (videoItem.Status == Enums.VideoStatus.ERROR)
            {
                return false;
            }

            VideoItems.Add(videoItem);

            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}