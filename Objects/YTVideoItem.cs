using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

using ytArchiver.Enums;

using VideoLibrary;

using Windows.Storage;

namespace ytArchiver.Objects
{
    public class YTVideoItem : INotifyPropertyChanged
    {
        public string Title { get; set; }

        private VideoStatus _status { get; set; }

        public VideoStatus Status
        {
            get
            {
                return _status;
            }

            set
            {
                _status = value;

                OnPropertyChanged();
            }
        }

        public string Resolution { get; set; }

        public string FileName { get; set; }

        private YouTubeVideo ytInfo { get; set; }

        public YTVideoItem() { }

        public YTVideoItem(string youTubeURL)
        {
            IEnumerable<YouTubeVideo> videoInfos;

            try
            {
                videoInfos = YouTube.Default.GetAllVideos(youTubeURL);

                if (videoInfos == null)
                {
                    Status = VideoStatus.ERROR;
                }
            } catch (Exception)
            {
                Status = VideoStatus.ERROR;

                return;
            }

            ytInfo = videoInfos.FirstOrDefault(a => a.Resolution == 1080);

            Title = ytInfo.Title;
            Resolution = ytInfo.Resolution.ToString();
            FileName = ytInfo.FullName;

            Status = VideoStatus.QUEUED;
        }

        public async void Download()
        {
            Status = VideoStatus.DOWNLOADING;

            var videoLibrary = await KnownFolders.GetFolderForUserAsync(null, KnownFolderId.VideosLibrary);

            var file = await videoLibrary.CreateFileAsync(FileName, CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteBytesAsync(file, ytInfo.GetBytes());

            Status = VideoStatus.DOWNLOADED;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}