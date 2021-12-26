using ytArchiver.Enums;

namespace ytArchiver.Objects
{
    public class YTVideoItem
    {
        public string Title { get; set; }

        public VideoStatus Status { get; set; }

        public string Resolution { get; set; }

        public string FileName { get; set; }
    }
}