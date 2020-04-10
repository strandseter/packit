using Packit.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App
{
    public class ItemImageLink : INotifyPropertyChanged
    {
        private BitmapImage image;

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

        public Item Item { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
