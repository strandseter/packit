using System;
using Packit.App.DataLinks;
using Packit.App.Helpers;
using Packit.App.DataAccess.Http;
using Packit.App.ThirdPartyApiModels;
using Packit.App.ThirdPartyApiModels.Openweathermap;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Packit.Model;
using Packit.App.Services;
using Packit.App.Views;
using Packit.App.DataAccess;
using Packit.App.Factories;
using Packit.App.Wrappers;
using System.Linq;

namespace Packit.App.ViewModels
{
    public class DetailTripViewModel : Observable
    {
        private readonly WeatherDataAccess weatherDataAccess = new WeatherDataAccess();
        private readonly IBasicDataAccess<Backpack> backpackDataAccess = new BasicDataAccessFactory<Backpack>().Create();
        private readonly IBasicDataAccess<Item> itemDataAccess = new BasicDataAccessFactory<Item>().Create();
        private readonly IRelationDataAccess<Backpack, Item> backpackItemDataAccess = new RelationDataAccessFactory<Backpack, Item>().Create();
        private readonly IRelationDataAccess<Trip, Backpack> tripBackpackDataAccess = new RelationDataAccessFactory<Trip, Backpack>().Create();
        private ICommand loadedCommand;
        private bool isVisible;

        public bool IsVisible
        {
            get => isVisible; 
            set
            {
                if (value == isVisible) return;
                isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }

        public ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new RelayCommand(LoadData));
        public ICommand ItemCommand { get; set; }
        public ICommand EditTripCommand { get; set; }
        public ICommand RemoveBackpackCommand { get; set; }
        public ICommand DeleteBackpackCommand { get; set; }
        public ICommand AddBackpackCommand { get; set; }
        public ICommand ShareBackpackCommand { get; set; }
        public ICommand AddItemToBackpackCommand { get; set; }
        public ICommand RemoveItemFromBackpackCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }
        public TripImageWeatherLink TripWithImageWeather { get; set; }
        public WeatherReport WeatherReport { get; set; }
        public ObservableCollection<BackpackWithItems> Backpacks { get; } = new ObservableCollection<BackpackWithItems>();

        public DetailTripViewModel()
        {
            EditTripCommand = new RelayCommand(() => IsVisible = !IsVisible);

            AddItemToBackpackCommand = new RelayCommand<BackpackWithItems>(param =>
            {
                NavigationService.Navigate(typeof(SelectItemsPage), new BackpackTripWrapper() { Backpack = param, Trip = TripWithImageWeather });
            });

            RemoveItemFromBackpackCommand = new RelayCommand<ItemBackpackWrapper>(async param =>
            {
                if(await backpackItemDataAccess.DeleteEntityFromEntityAsync(param.BackpackWithItems.Backpack.BackpackId, param.Item.ItemId))
                    param.BackpackWithItems.Items.Remove(param.Item);
            });

            DeleteItemCommand = new RelayCommand<ItemBackpackWrapper>(async param =>
            {
                if(await itemDataAccess.DeleteAsync(param.Item))
                    param.BackpackWithItems.Items.Remove(param.Item);
            });

            AddBackpackCommand = new RelayCommand<ItemBackpackWrapper>(param =>
            {
                var dfgfdg = param;
            });

            RemoveBackpackCommand = new RelayCommand<BackpackWithItems>(async param =>
            {
                if (await tripBackpackDataAccess.DeleteEntityFromEntityAsync(TripWithImageWeather.Trip.TripId, param.Backpack.BackpackId))
                    Backpacks.Remove(param);
            });

            DeleteBackpackCommand = new RelayCommand<BackpackWithItems>(async param =>
            {
                if (await backpackDataAccess.DeleteAsync(param.Backpack))
                    Backpacks.Remove(param);
            });

            ShareBackpackCommand = new RelayCommand<BackpackWithItems>(async param =>
            {
                param.Backpack.IsShared = true;

                if (!await backpackDataAccess.UpdateAsync(param.Backpack))
                    param.Backpack.IsShared = false;
            });
        }

        private async void LoadData()
        {
            LoadBackpacks();
            LoadItemsInBackpacks();
            await LoadWeatherReportAsync();
        }

        private void LoadBackpacks()
        {
            foreach(var bt in TripWithImageWeather.Trip.Backpacks)
                Backpacks.Add(new BackpackWithItems(bt.Backpack));
        }

        private void LoadItemsInBackpacks()
        {
            foreach(var bwi in Backpacks)
            {
                foreach(var item in bwi.Backpack.Items)
                    bwi.Items.Add(item.Item);
            }
        }

        private async Task LoadWeatherReportAsync()
        {
            TripWithImageWeather.WeatherReport = await weatherDataAccess.GetCurrentWeatherReportAsync(TripWithImageWeather.Trip.Destination);
            //WeatherReport.Weathers[0].IconImage = await weatherDataAccess.GetCurrentWeatherIconAsync(WeatherReport.Weathers[0].Icon);
        }

        public void Initialize(TripImageWeatherLink trip) => TripWithImageWeather = trip;
    }
}
