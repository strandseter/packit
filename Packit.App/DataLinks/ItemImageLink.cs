using Packit.App.Helpers;
using Packit.Model;
using Packit.Model.NotifyPropertyChanged;
using System;
using System.ComponentModel;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.DataLinks
{
    public class ItemImageLink : Observable
    {
        private BitmapImage image;
        private Model.Item item;

        public BitmapImage Image
        {
            get => image;
            set => Set(ref image, value);
        }

        public Model.Item Item
        {
            get => item;
            set => Set(ref item, value);
        }
    }
}
