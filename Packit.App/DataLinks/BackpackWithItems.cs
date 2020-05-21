using Packit.App.Helpers;
using Packit.Model;
using Packit.Model.NotifyPropertyChanged;
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
            set => Set(ref backpack, value);
        }
        public ObservableCollection<Item> Items
        {
            get => items;
            set => Set(ref items, value);
        }
        public BackpackWithItems(Backpack backpack)
        {
            Backpack = backpack;
            Items = new ObservableCollection<Item>();
        }
    }
}
