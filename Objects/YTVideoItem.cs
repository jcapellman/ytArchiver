using System.IO;
using System.Linq;

using ytArchiver.Enums;

using VideoLibrary;

namespace ytArchiver.Objects
{
    public class YTVideoItem
    {
        public string Title { get; set; }

        public VideoStatus Status { get; set; }

        public string Resolution { get; set; }

        public string FileName { get; set; }

        private YouTubeVideo ytInfo { get; set; }

        public string DownloadPath { get; set; }

        public YTVideoItem() { }

        public YTVideoItem(string youTubeURL, string downloadPath)
        {
            var videoInfos = YouTube.Default.GetAllVideos(youTubeURL);

            if (videoInfos == null)
            {
                Status = VideoStatus.ERROR;
            }

            ytInfo = videoInfos.FirstOrDefault(a => a.Resolution == 1080);

            DownloadPath = Path.Combine(downloadPath, ytInfo.FullName);

            Title = ytInfo.Title;
            Resolution = ytInfo.Resolution.ToString();
            FileName = ytInfo.FullName;

            Status = VideoStatus.QUEUED;
        }

        public void Download()
        {
            Status = VideoStatus.DOWNLOADING;

            File.WriteAllBytes(DownloadPath, ytInfo.GetBytes());

            Status = VideoStatus.DOWNLOADED;
        }
    }
}