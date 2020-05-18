using Packit.Model;
using Packit.Model.NotifyPropertyChanged;
using System.Collections.ObjectModel;

namespace Packit.App.DataLinks
{
    public class BackpackWithItemsWithImages : Observable
    {
        private ObservableCollection<ItemImageLink> items;
        private Backpack backpack;

        public Backpack Backpack
        {
            get => backpack;
            set => Set(ref backpack, value);
        }
        public ObservableCollection<ItemImageLink> ItemImageLinks
        {
            get => items;
            set => Set(ref items, value);
        }

        public BackpackWithItemsWithImages(Backpack backpack)
        {
            Backpack = backpack;
            ItemImageLinks = new ObservableCollection<ItemImageLink>();
        }
    }
}
