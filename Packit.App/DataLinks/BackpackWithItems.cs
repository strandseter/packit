using Packit.App.Helpers;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.DataLinks
{
    public class BackpackWithItems : Observable
    {
        private ObservableCollection<Item> items;
        private Backpack backpack;

        public Backpack Backpack
        {
            get => backpack;
            set
            {
                if (backpack == value) return;
                backpack = value;
                OnPropertyChanged(nameof(Backpack));
            }
        }
        public ObservableCollection<Item> Items
        {
            get => items;
            set
            {
                if (items == value) return;
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
        public BackpackWithItems(Backpack backpack)
        {
            Backpack = backpack;
            Items = new ObservableCollection<Item>();
        }
    }
}
