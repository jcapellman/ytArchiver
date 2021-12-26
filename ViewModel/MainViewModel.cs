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

        public MainViewModel()
        {
            VideoItems = new ObservableCollection<YTVideoItem>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}