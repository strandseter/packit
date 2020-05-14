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
        private readonly IBasicDataAccess<Check> checksDataAccess = new BasicDataAccessFactory<Check>().Create();
        private readonly IRelationDataAccess<Backpack, Item> backpackItemDataAccess = new RelationDataAccessFactory<Backpack, Item>().Create();
        private readonly IRelationDataAccess<Trip, Backpack> tripBackpackDataAccess = new RelationDataAccessFactory<Trip, Backpack>().Create();
        private ICommand loadedCommand;
        private bool isVisible;
        private IList<BackpackWithItems> backpacksClone;

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

        //private async Task UpdateItemsAsync()
        //{
        //    for (int i = 0; i < ItemImageLinks.Count; i++)
        //    {
        //        if (!StringIsEqual(ItemImageLinks[i].Item.Title, itemImageLinksClone[i].Item.Title) || (!StringIsEqual(ItemImageLinks[i].Item.Title, itemImageLinksClone[i].Item.Title)))
        //            if (!await itemsDataAccess.UpdateAsync(ItemImageLinks[i].Item))
        //                isVisible = true;
        //    }
        //}

        private bool StringIsEqual(string firstString, string secondString) => firstString.Equals(secondString, StringComparison.CurrentCulture);

        private async Task UpdateItemsAsync()
        {
     
        }

        private async Task UpdateBackpacksAsync()
        {
            for (int i = 0; i < Backpacks.Count; i++)
            {
                if(!StringIsEqual(Backpacks[i].Backpack.Title, backpacksClone[i].Backpack.Title))
                {
                    if(!await backpackDataAccess.UpdateAsync(Backpacks[i].Backpack))
                    {
                        isVisible = true;
                    }
                }
            }
        }

        private async Task UpdateTripAsync()
        {

        }

        public DetailTripViewModel()
        {
            EditTripCommand = new RelayCommand(async () =>
            {

                if (!IsVisible)
                    CopyBackpackWithItemsList();

                if (IsVisible)
                    await UpdateBackpacksAsync();

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

        private void CopyBackpackWithItemsList()
        {
            backpacksClone = Backpacks.ToList().DeepClone();
        }

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
