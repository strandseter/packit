using Packit.App.DataLinks;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Packit.App.Wrappers
{
    public class ItemBackpackBoolWrapper : DependencyObject
    {
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register(
                "Item", typeof(Item),
                typeof(ItemBackpackBoolWrapper), null);

        public static readonly DependencyProperty BackpackWithItemsWithImagesProperty =
           DependencyProperty.Register(
               "BackpackWithItemsWithImages", typeof(BackpackWithItemsWithImages),
               typeof(ItemBackpackBoolWrapper), null);

        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register(
                "IsChecked", typeof(bool),
                typeof(ItemBackpackBoolWrapper), null);

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public Item Item
        {
            get { return (Item)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        public BackpackWithItemsWithImages BackpackWithItemsWithImages
        {
            get { return (BackpackWithItemsWithImages)GetValue(BackpackWithItemsWithImagesProperty); }
            set { SetValue(BackpackWithItemsWithImagesProperty, value); }
        }
    }
}
