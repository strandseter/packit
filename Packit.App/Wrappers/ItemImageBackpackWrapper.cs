using Packit.App.DataLinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Packit.App.Wrappers
{
    public class ItemImageBackpackWrapper : DependencyObject
    {
        public static readonly DependencyProperty ItemImageLinkProperty =
            DependencyProperty.Register(
                "ItemImageLink", typeof(ItemImageLink),
                typeof(ItemImageBackpackWrapper), null);

        public static readonly DependencyProperty BackpackWithItemsWithImagesProperty =
           DependencyProperty.Register(
               "BackpackWithItemsWithImages", typeof(BackpackWithItemsWithImages),
               typeof(ItemImageBackpackWrapper), null);

        public ItemImageLink ItemImageLink
        {
            get { return (ItemImageLink)GetValue(ItemImageLinkProperty); }
            set { SetValue(ItemImageLinkProperty, value); }
        }

        public BackpackWithItemsWithImages BackpackWithItemsWithImages
        {
            get { return (BackpackWithItemsWithImages)GetValue(BackpackWithItemsWithImagesProperty); }
            set { SetValue(BackpackWithItemsWithImagesProperty, value); }
        }
    }
}
