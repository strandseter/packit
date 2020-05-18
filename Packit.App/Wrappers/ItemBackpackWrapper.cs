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
                "Item", typeof(Model.Item),
                typeof(ItemBackpackWrapper), null);

        public static readonly DependencyProperty BackpackWithItemsProperty =
           DependencyProperty.Register(
               "BackpackWithItems", typeof(BackpackWithItems),
               typeof(ItemBackpackWrapper), null);

        public Model.Item Item
        {
            get { return (Model.Item)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        public BackpackWithItems BackpackWithItems
        {
            get { return (BackpackWithItems)GetValue(BackpackWithItemsProperty); }
            set { SetValue(BackpackWithItemsProperty, value); }
        }
    }
}
