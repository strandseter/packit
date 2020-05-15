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

namespace Packit.App.ViewModels
{
    public class DetailTripViewModel : Observable
    {
        private readonly WeatherDataAccess weatherDataAccess = new WeatherDataAccess();
        private readonly IBasicDataAccess<Backpack> backpackDataAccess = new BasicDataAccessFactory<Backpack>().Create();
        private readonly IBasicDataAccess<Item> itemDataAccess = new BasicDataAccessFactory<Item>().Create();
        private readonly IBasicDataAccess<Trip> tripDataAccess = new BasicDataAccessFactory<Trip>().Create();
        private readonly IBasicDataAccess<Check> checksDataAccess = new BasicDataAccessFactory<Check>().Create();
        private readonly IRelationDataAccess<Backpack, Item> backpackItemDataAccess = new RelationDataAccessFactory<Backpack, Item>().Create();
        private readonly IRelationDataAccess<Trip, Backpack> tripBackpackDataAccess = new RelationDataAccessFactory<Trip, Backpack>().Create();

        private ICommand loadedCommand;
        private bool isVisible;

        private IList<BackpackWithItems> backpacksClone;
        private Trip tripClone;

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
        public ICommand ItemCheckedCommand { get; set; }

        public TripImageWeatherLink TripImageWeatherLink { get; set; }
        public WeatherReport WeatherReport { get; set; }
        public ObservableCollection<BackpackWithItems> Backpacks { get; } = new ObservableCollection<BackpackWithItems>();

        public DetailTripViewModel()
        {
            EditTripCommand = new RelayCommand(async () =>
            {

                if (!IsVisible)
                {
                    CloneBackpackWithItemsList();
                    CloneTrip();
                }
                    

                if (IsVisible)
                {
                    await UpdateBackpacksAsync();
                    await UpdateItemsAsync();
                    await UpdateTripAsync();
                }

                IsVisible = !IsVisible;
            });

            AddItemToBackpackCommand = new RelayCommand<BackpackWithItems>(param =>
            {
                NavigationService.Navigate(typeof(SelectItemsPage), new BackpackTripWrapper() { Backpack = param, Trip = TripImageWeatherLink });
            });

            RemoveItemFromBackpackCommand = new RelayCommand<ItemBackpackWrapper>(async param =>
            {
                if (await backpackItemDataAccess.DeleteEntityFromEntityAsync(param.BackpackWithItems.Backpack.BackpackId, param.Item.ItemId))
                {
                    param.BackpackWithItems.Items.Remove(param.Item);

                    if (param.Item.Check != null)
                    {
                        if (await checksDataAccess.DeleteAsync(param.Item.Check))
                        {
                            param.Item.Check.IsChecked = false;
                        }
                    }
                }
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
                if (await tripBackpackDataAccess.DeleteEntityFromEntityAsync(TripImageWeatherLink.Trip.TripId, param.Backpack.BackpackId))
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

            ItemCheckedCommand = new RelayCommand<ItemBackpackBoolWrapper>(async param =>
            {
                if (param.IsChecked)
                {
                    var check = new Check()
                    {
                        IsChecked = true,
                        ItemId = param.Item.ItemId,
                        BackpackId = param.BackpackWithItems.Backpack.BackpackId,
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
            });
        }

        private async Task UpdateItemsAsync()
        {
            for (int i = 0; i < Backpacks.Count; i++)
            {
                for (int j = 0; j < Backpacks[i].Items.Count; j++)
                {
                    if (StringIsEqual(Backpacks[i].Items[j].Title, backpacksClone[i].Items[j].Title) && StringIsEqual(Backpacks[i].Items[j].Description, backpacksClone[i].Items[j].Description))
                        continue;
                    
                    if (!await itemDataAccess.UpdateAsync(Backpacks[i].Items[j]))
                    {
                        Backpacks[i].Items[j] = backpacksClone[i].Items[j];

                        //Error box
                    }
                }
            }
        }

        private async Task UpdateBackpacksAsync()
        {
            for (int i = 0; i < Backpacks.Count; i++)
            {
                if (StringIsEqual(Backpacks[i].Backpack.Title, backpacksClone[i].Backpack.Title))
                    continue;

                Backpacks[i].Backpack.Items.Clear();

                if (!await backpackDataAccess.UpdateAsync(Backpacks[i].Backpack))
                {
                    Backpacks[i] = backpacksClone[i];

                    //Error box
                }
            }
        }

        private async Task UpdateTripAsync()
        {
            if (StringIsEqual(TripImageWeatherLink.Trip.Title, tripClone.Title) && StringIsEqual(TripImageWeatherLink.Trip.Destination, tripClone.Destination))
                return;

            TripImageWeatherLink.Trip.Backpacks.Clear();

            if (!await tripDataAccess.UpdateAsync(TripImageWeatherLink.Trip))
            {
                TripImageWeatherLink.Trip = tripClone;

                //Error box
            }
        }

        private void CloneBackpackWithItemsList() => backpacksClone = Backpacks.ToList().DeepClone();

        private void CloneTrip() => tripClone = (Trip)TripImageWeatherLink.Trip.Clone();

        private bool StringIsEqual(string firstString, string secondString) => firstString.Equals(secondString, StringComparison.CurrentCulture);

        private async void LoadData()
        {
            LoadBackpacks();
            LoadItemsInBackpacks();
            await LoadWeatherReportAsync();
        }

        private void LoadBackpacks()
        {
            foreach(var bt in TripImageWeatherLink.Trip.Backpacks)
                Backpacks.Add(new BackpackWithItems(bt.Backpack));
        }

        private void LoadItemsInBackpacks()
        {
            foreach(var bwi in Backpacks)
            {
                foreach(var itemBackpack in bwi.Backpack.Items)
                {
                    foreach(var check in itemBackpack.Item.Checks)
                    {
                        if(bwi.Backpack.BackpackId == check.BackpackId && itemBackpack.Item.ItemId == check.ItemId)
                        {
                            itemBackpack.Item.Check = check;
                            itemBackpack.Item.Check.IsChecked = true;
                        }
                    }
                    bwi.Items.Add(itemBackpack.Item);
                }
            }
        }

        private async Task LoadWeatherReportAsync()
        {
            TripImageWeatherLink.WeatherReport = await weatherDataAccess.GetCurrentWeatherReportAsync(TripImageWeatherLink.Trip.Destination);
            TripImageWeatherLink.WeatherReport.Weathers[0].IconImage = await weatherDataAccess.GetCurrentWeatherIconAsync(TripImageWeatherLink.WeatherReport.Weathers[0].Icon);
        }

        public void Initialize(TripImageWeatherLink trip) => TripImageWeatherLink = trip;
    }
}
