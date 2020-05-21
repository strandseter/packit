using Packit.App.DataLinks;
using Packit.App.Helpers;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Packit.App.Wrappers
{
    public class ItemBackpackWrapper : DependencyObject
    {
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register(
                "Item", typeof(Item),
                typeof(ItemBackpackWrapper), null);

        public static readonly DependencyProperty BackpackWithItemsWithImagesProperty =
           DependencyProperty.Register(
               "BackpackWithItemsWithImages", typeof(BackpackWithItems),
               typeof(ItemBackpackWrapper), null);

        public Item Item
        {
            get { return (Item)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        public BackpackWithItemsWithImages BackpackWithItems
        {
            get { return (BackpackWithItemsWithImages)GetValue(BackpackWithItemsWithImagesProperty); }
            set { SetValue(BackpackWithItemsWithImagesProperty, value); }
        }
    }
}
