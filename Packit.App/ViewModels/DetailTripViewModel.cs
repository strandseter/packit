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
using Packit.Extensions;
using Packit.Model.Models;
using System.Collections.Generic;
using System.Net.Http;

namespace Packit.App.ViewModels
{
    public class DetailTripViewModel : ViewModel
    {
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        private readonly WeatherDataAccess weatherDataAccess = new WeatherDataAccess();
        private readonly IBasicDataAccess<Backpack> backpackDataAccess = new BasicDataAccessFactory<Backpack>().Create();
        private readonly IBasicDataAccess<Item> itemDataAccess = new BasicDataAccessFactory<Item>().Create();
        private readonly IBasicDataAccess<Trip> tripDataAccess = new BasicDataAccessFactory<Trip>().Create();
        private readonly IBasicDataAccess<Check> checksDataAccess = new BasicDataAccessFactory<Check>().Create();
        private readonly IRelationDataAccess<Backpack, Item> backpackItemDataAccess = new RelationDataAccessFactory<Backpack, Item>().Create();
        private readonly IRelationDataAccess<Trip, Backpack> tripBackpackDataAccess = new RelationDataAccessFactory<Trip, Backpack>().Create();

        private ICommand loadedCommand;
        private bool isVisible;

        private Trip tripClone;

        public bool IsVisible
        {
            get => isVisible;
            set => Set(ref isVisible, value);
        }

        public ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new RelayCommand(async () => await LoadDataAsync()));
        public ICommand EditTripCommand { get; set; }
        public ICommand DeleteTripCommand { get; set; }
        public ICommand CancelTripCommand { get; set; }
        public ICommand AddBackpacksCommand { get; set; }
        public ICommand RemoveBackpackCommand { get; set; }
        public ICommand DeleteBackpackCommand { get; set; }
        public ICommand ShareBackpackCommand { get; set; }
        public ICommand AddItemToBackpackCommand { get; set; }
        public ICommand RemoveItemFromBackpackCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }
        public ICommand ItemCheckedCommand { get; set; }

        public TripImageWeatherLink TripImageWeatherLink { get; set; }
        public ObservableCollection<BackpackWithItemsWithImages> Backpacks { get; } = new ObservableCollection<BackpackWithItemsWithImages>();

        public DetailTripViewModel()
        {
            CancelTripCommand = new RelayCommand(async () =>
            {
                var test = 0;
            });

            DeleteTripCommand = new RelayCommand(async () =>
            {
                if (await tripDataAccess.DeleteAsync(TripImageWeatherLink.Trip))
                    NavigationService.GoBack();
            });

            EditTripCommand = new RelayCommand(async () =>
            {
                if (!IsVisible)
                    CloneTrip();
                    
                if (IsVisible)
                    await Update();

                IsVisible = !IsVisible;
            });

            AddItemToBackpackCommand = new RelayCommand<BackpackWithItemsWithImages>(param =>
            {
                NavigationService.Navigate(typeof(SelectItemsPage), new BackpackWithItemsTripImageWeatherWrapper() { Backpack = param, Trip = TripImageWeatherLink });
            });

            RemoveItemFromBackpackCommand = new RelayCommand<ItemImageBackpackWrapper>(async param =>
            {
                if (await backpackItemDataAccess.DeleteEntityFromEntityAsync(param.BackpackWithItemsWithImages.Backpack.BackpackId, param.ItemImageLink.Item.ItemId))
                {
                    param.BackpackWithItemsWithImages.ItemImageLinks.Remove(param.ItemImageLink);

                    if (param.ItemImageLink.Item.Check != null)
                    {
                        if (await checksDataAccess.DeleteAsync(param.ItemImageLink.Item.Check))
                            param.ItemImageLink.Item.Check.IsChecked = false;
                    }
                }
            });

            DeleteItemCommand = new RelayCommand<ItemImageBackpackWrapper>(async param =>
            {
                if(await itemDataAccess.DeleteAsync(param.ItemImageLink.Item))
                    param.BackpackWithItemsWithImages.ItemImageLinks.Remove(param.ItemImageLink);
            });

            AddBackpacksCommand = new RelayCommand<ItemBackpackWrapper>(param =>
            {
                NavigationService.Navigate(typeof(SelectBackpacksPage), new BackpackWithItemsWithImagesTripWrapper() { BackpackWithItemsWithImages = Backpacks, TripImageWeatherLink = TripImageWeatherLink});
            });

            RemoveBackpackCommand = new RelayCommand<BackpackWithItemsWithImages>(async param =>
            {
                if (await tripBackpackDataAccess.DeleteEntityFromEntityAsync(TripImageWeatherLink.Trip.TripId, param.Backpack.BackpackId))
                    Backpacks.Remove(param);
            });

            DeleteBackpackCommand = new RelayCommand<BackpackWithItemsWithImages>(async param =>
            {
                if (await backpackDataAccess.DeleteAsync(param.Backpack))
                    Backpacks.Remove(param);
            });

            ShareBackpackCommand = new RelayCommand<BackpackWithItemsWithImages>(async param =>
            {
                param.Backpack.IsShared = true;

                if (!await backpackDataAccess.UpdateAsync(param.Backpack))
                    param.Backpack.IsShared = false;
            });

            ItemCheckedCommand = new RelayCommand<ItemBackpackBoolWrapper>(async param => await CheckItemAsync(param));
        }

        private async Task Update()
        {
            await UpdateTripAsync();
            await UpdateWeatherAsync();
        }

        private async Task CheckItemAsync(ItemBackpackBoolWrapper param)
        {
            if (param.IsChecked)
            {
                var check = new Check()
                {
                    IsChecked = true,
                    ItemId = param.Item.ItemId,
                    BackpackId = param.BackpackWithItemsWithImages.Backpack.BackpackId,
                    TripId = TripImageWeatherLink.Trip.TripId,
                    UserId = 4
                };

                if (await checksDataAccess.AddAsync(check))
                {
                    param.Item.Check = check;
                    param.Item.Check.IsChecked = true;
                }
            }

            if (!param.IsChecked)
            {
                if (await checksDataAccess.DeleteAsync(param.Item.Check))
                    param.Item.Check.IsChecked = false;
            }
        }

        private async Task UpdateTripAsync()
        {
            if (StringIsEqual(TripImageWeatherLink.Trip.Title, tripClone.Title) && StringIsEqual(TripImageWeatherLink.Trip.Destination, tripClone.Destination))
                return;

            TripImageWeatherLink.Trip.Backpacks.Clear();

            if (!await tripDataAccess.UpdateAsync(TripImageWeatherLink.Trip))
            {
                await PopupService.ShowCouldNotSaveChangesAsync(tripClone.Title);
                TripImageWeatherLink.Trip = tripClone;
            }
        }

        private async Task UpdateWeatherAsync()
        {
            if (StringIsEqual(TripImageWeatherLink.Trip.Destination, tripClone.Destination))
                return;
            //TODO: Error handling
            await LoadWeatherReportAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                LoadBackpacks();
                LoadItemsInBackpacks();
                await LoadWeatherReportAsync();
                await LoadItemImagesAsync();
            }
            catch (HttpRequestException ex)
            {
                await PopupService.ShowCouldNotLoadAsync(NavigationService.GoBack, "Weather");
            }
        }

        private void LoadBackpacks()
        {
            foreach(var bt in TripImageWeatherLink.Trip.Backpacks)
                Backpacks.Add(new BackpackWithItemsWithImages(bt.Backpack));
        }

        private void LoadItemsInBackpacks()
        {
            foreach(var bwi in Backpacks)
            {
                foreach(var itemBackpack in bwi.Backpack.Items)
                {
                    foreach(var check in itemBackpack.Item.Checks)
                    {
                        if(bwi.Backpack.BackpackId == check.BackpackId && itemBackpack.Item.ItemId == check.ItemId && TripImageWeatherLink.Trip.TripId == check.TripId)
                        {
                            itemBackpack.Item.Check = check;
                            itemBackpack.Item.Check.IsChecked = true;
                        }
                    }
                    bwi.ItemImageLinks.Add(new ItemImageLink() { Item = itemBackpack.Item });
                }
            }
        }

        private async Task LoadItemImagesAsync()
        {
            foreach (var bwi in Backpacks)
            {
                foreach (var itemImageLink in bwi.ItemImageLinks)
                {
                    itemImageLink.Image = await imagesDataAccess.GetImageAsync(itemImageLink.Item.ImageStringName, "ms-appx:///Assets/grey.jpg");
                }
            }
        }

        private async Task LoadWeatherReportAsync()
        {
            TripImageWeatherLink.WeatherReport = await weatherDataAccess.GetCurrentWeatherReportAsync(TripImageWeatherLink.Trip.Destination);
            TripImageWeatherLink.WeatherReport.Weathers[0].IconImage = await weatherDataAccess.GetCurrentWeatherIconAsync(TripImageWeatherLink.WeatherReport.Weathers[0].Icon);
        }

        public void Initialize(TripImageWeatherLink trip) => TripImageWeatherLink = trip;
        private void CloneTrip() => tripClone = (Trip)TripImageWeatherLink.Trip.Clone();
    }
}
