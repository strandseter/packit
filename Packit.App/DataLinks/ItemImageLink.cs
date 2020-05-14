using Packit.App.Helpers;
using Packit.Model;
using System;
using System.ComponentModel;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.DataLinks
{
    public class ItemImageLink : Observable
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
    }
}
