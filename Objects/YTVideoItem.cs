using System;
using System.Collections.Generic;
using System.Linq;

using ytArchiver.Enums;

using VideoLibrary;

using Windows.Storage;
using System.Threading.Tasks;

namespace ytArchiver.Objects
{
    public class YTVideoItem
    {
        public string Title { get; set; }

        public VideoStatus Status { get; set; }

        public string Resolution { get; set; }

        public string FileName { get; set; }

        private YouTubeVideo ytInfo { get; set; }

        public YTVideoItem() { }

        public YTVideoItem(string youTubeURL)
        {
            List<YouTubeVideo> videoInfos;

            try
            {
                videoInfos = YouTube.Default.GetAllVideos(youTubeURL).ToList();

                if (videoInfos == null)
                {
                    Status = VideoStatus.ERROR;
                }
            } catch (ArgumentException)
            {
                Status = VideoStatus.INVALID_URL;

                return;
            }

            ytInfo = videoInfos.OrderByDescending(a => a.Resolution).FirstOrDefault(a => a.AudioFormat != AudioFormat.Unknown);

            Title = ytInfo.Title;
            Resolution = ytInfo.Resolution.ToString();
            FileName = ytInfo.FullName;

            Status = VideoStatus.QUEUED;
        }

        public async Task<Enums.VideoStatus> Download()
        {
            Status = VideoStatus.DOWNLOADING;

            var videoLibrary = await KnownFolders.GetFolderForUserAsync(null, KnownFolderId.VideosLibrary);

            var file = await videoLibrary.CreateFileAsync(FileName, CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteBytesAsync(file, ytInfo.GetBytes());

            Status = VideoStatus.DOWNLOADED;

            return Status;
        }
    }
}