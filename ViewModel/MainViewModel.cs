using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using ytArchiver.Objects;

namespace ytArchiver.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private BackgroundWorker _worker = new BackgroundWorker();

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

            _worker.RunWorkerCompleted += _worker_RunWorkerCompleted;
            _worker.DoWork += _worker_DoWork;
            //_worker.RunWorkerAsync();
        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (VideoItems.Count == 0)
            {
                return;
            }

            foreach (var item in VideoItems)
            {
                item.Download();
            }
        }

        private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            System.Threading.Thread.Sleep(500);

            _worker.RunWorkerAsync();
        }

        public bool AddDownloadToQueue()
        {
            var videoItem = new YTVideoItem(VideoURL, System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyVideos));

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