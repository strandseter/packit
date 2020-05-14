using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Packit.App.DataAccess;
using Packit.App.DataLinks;
using Packit.App.Factories;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.Model;
using Windows.Storage;

namespace Packit.App.ViewModels
{
    public class NewTripViewModel : Observable
    {
        private readonly IBasicDataAccess<Trip> tripsDataAccess = new BasicDataAccessFactory<Trip>().Create();
        private readonly IBasicDataAccess<Backpack> backpacksDataAcess = new BasicDataAccessFactory<Backpack>().Create();
        private readonly IRelationDataAccess<Trip, Backpack> tripBackpackDataAccess = new RelationDataAccessFactory<Trip, Backpack>().Create();
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        private ICommand loadedCommand;
        private StorageFile localImage;

        public Trip Trip { get; set; } = new Trip();

        public ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new RelayCommand(async () => await LoadDataAsync()));
        public ICommand CandcelCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand AddBackpackCommand { get; set; }
        public ICommand RemoveBackpackCommand { get; set; }
        public ObservableCollection<BackpackWithItems> AvailableBackpacksWithitems { get; } = new ObservableCollection<BackpackWithItems>();
        public ObservableCollection<BackpackWithItems> SelectedBackpacksWithitems { get; } = new ObservableCollection<BackpackWithItems>();

        public NewTripViewModel()
        {
            CandcelCommand = new RelayCommand(() => NavigationService.GoBack());

            AddBackpackCommand = new RelayCommand<BackpackWithItems>(param =>
            {
                SelectedBackpacksWithitems.Add(param);
                AvailableBackpacksWithitems.Remove(param);
            },  param => param != null);

            RemoveBackpackCommand = new RelayCommand<BackpackWithItems>(param =>
            {
                AvailableBackpacksWithitems.Add(param);
                SelectedBackpacksWithitems.Remove(param);
            }, param => param != null);

            SaveCommand = new RelayCommand(async () =>
            {
                if (await tripsDataAccess.AddAsync(Trip))
                {
                    foreach (var backpackWithItems in SelectedBackpacksWithitems)
                    {
                        
                    }
                }

            });
        }

        private async Task LoadDataAsync()
        {
            await LoadBackpacksAsync();
            LoadItemsInBackpacks();
        }

        private async Task LoadBackpacksAsync()
        {
            var backpacks = await backpacksDataAcess.GetAllWithChildEntitiesAsync();

            foreach (var backpack in backpacks)
                AvailableBackpacksWithitems.Add(new BackpackWithItems(backpack));
        }

        private void LoadItemsInBackpacks()
        {
            foreach (var bwi in AvailableBackpacksWithitems)
                foreach (var itemBackpack in bwi.Backpack.Items)
                    bwi.Items.Add(itemBackpack.Item);
        }
    }
}
