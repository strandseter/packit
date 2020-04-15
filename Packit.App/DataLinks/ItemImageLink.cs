using Packit.Model;
using System.ComponentModel;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App
{
    public class ItemImageLink : INotifyPropertyChanged
    {
        private BitmapImage image;
        private Item item;

        public BitmapImage Image
        {
            get => image;
            set
            {
                if (value == image) return;
                image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public Item Item
        {
            get => item;
            set
            {
                if (value == item) return;
                item = value;
                OnPropertyChanged(nameof(Item));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
