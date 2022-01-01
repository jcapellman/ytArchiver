using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.Storage;
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

        internal void RemoveItem(YTVideoItem item)
        {
            VideoItems.Remove(item);
        }

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

        internal async void RunQueue()
        {
            await System.Threading.Tasks.Task.Run(() => 
            {
                while (true)
                {
                    foreach (var item in VideoItems.Where(a => a.Status == Enums.VideoStatus.QUEUED))
                    {
                        item.Download();
                    }

                    System.Threading.Thread.Sleep(500);
                }
            });
        }

        public bool AddDownloadToQueue()
        {
            var videoItem = new YTVideoItem(VideoURL);

            if (videoItem.Status == Enums.VideoStatus.ERROR)
            {
                return false;
            }

            VideoItems.Add(videoItem);

            VideoURL = string.Empty;

            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}