using Packit.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.DataLinks
{
    public class BackpackWithItems
    {
        public Backpack Backpack { get; set; }
        public ObservableCollection<Item> Items { get; } = new ObservableCollection<Item>();

        public BackpackWithItems() { }

        public BackpackWithItems(Backpack backpack) => Backpack = backpack;
    }
}
