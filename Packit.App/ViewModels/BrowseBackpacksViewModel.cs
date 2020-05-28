using Microsoft.Toolkit.Uwp.Helpers;
using Packit.App.DataAccess;
using Packit.App.DataAccess.Http;
using Packit.App.DataLinks;
using Packit.App.Factories;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.Extensions;
using Packit.Model;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Packit.App.ViewModels
{
    public class BrowseBackpacksViewModel : ViewModel
    {
        private readonly ICustomBackpackDataAccess customBackpackDataAccess = new CustomBackpackDataAccessHttp();
        private readonly IBasicDataAccess<Backpack> backpackDataAccess = new BasicDataAccessFactory<Backpack>().Create();
        private readonly IRelationDataAccess<Backpack, Item> backpackItemDataAccess = new RelationDataAccessFactory<Backpack, Item>().Create();
        private readonly IBasicDataAccess<Item> itemDataAccess = new BasicDataAccessFactory<Item>().Create();
        private ICommand loadedCommand;

        public ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new NetworkErrorHandlingRelayCommand<BrowseBackpacksPage>(async () => await LoadDataAsync(), PopUpService));
        public ICommand StealCommand { get; set; }
        public ICommand StopSharingCommand { get; set; }

        public ObservableCollection<BackpackWithItems> AllBackpacksWithItems { get; } = new ObservableCollection<BackpackWithItems>();
        public ObservableCollection<BackpackWithItems> UserBackpacksWithItems { get; } = new ObservableCollection<BackpackWithItems>();

        public BrowseBackpacksViewModel(IPopUpService popUpService)
            :base(popUpService)
        {
            StealCommand = new NetworkErrorHandlingRelayCommand<BackpackWithItems, BrowseBackpacksPage>(async param =>
            {
                await Task.WhenAll(StealBackpack(param), DisableCommand());
            }, PopUpService, param => param != null);

            StopSharingCommand = new NetworkErrorHandlingRelayCommand<BackpackWithItems, BrowseBackpacksPage>(async param =>
            {
                
            }, PopUpService, param => param != null);
        }

        private async Task StopSharing(BackpackWithItems backpackWithItems)
        {
            if (backpackWithItems == null)
                throw new ArgumentNullException(nameof(backpackWithItems));

            if (await backpackDataAccess.UpdateAsync(backpackWithItems.Backpack))
            {
                UserBackpacksWithItems.Remove(backpackWithItems);
                AllBackpacksWithItems.Remove(backpackWithItems);
            }
            else
                await PopUpService.ShowCouldNotSaveAsync(backpackWithItems.Backpack.Title);
        }

        private async Task StealBackpack(BackpackWithItems backpackWithItems)
        {
            var isSuccess = true;

            if (backpackWithItems == null)
                throw new ArgumentNullException(nameof(backpackWithItems));

            var newBackpack = new Backpack()
            {
                Title = backpackWithItems.Backpack.Title,
                Description = backpackWithItems.Backpack.Description
            };

            if (!await backpackDataAccess.AddAsync(newBackpack))
                isSuccess = false;


            foreach (var item in backpackWithItems.Items)
            {
                var newitem = new Item()
                {
                    Title = item.Title,
                    Description = item.Description
                };

                if (!await itemDataAccess.AddAsync(newitem))
                {
                    isSuccess = false;
                    break;
                }

                if (!await backpackItemDataAccess.AddEntityToEntityAsync(newBackpack.BackpackId, newitem.ItemId))
                {
                    isSuccess = false;
                    break;
                }
            }

            if (isSuccess)
                await PopUpService.ShowWasAddedAsync(backpackWithItems.Backpack.Title);
            else
                await PopUpService.ShowCouldNotSaveAsync(backpackWithItems.Backpack.Title);
        }

        private async Task LoadDataAsync()
        {
            await LoadAllBackpacks();
            await LoadUserBackpacks();
        }

        private async Task LoadAllBackpacks()
        {
            var backpacks = await customBackpackDataAccess.GetAllSharedBackpacksAsync();

            await Task.Run(async () =>
            {
                await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
                {
                    LoadBackpacks(backpacks, AllBackpacksWithItems);
                });
            });
        }

        private async Task LoadUserBackpacks()
        {
            var backpacks = await customBackpackDataAccess.GetAllSharedBackpacksByUserAsync();

            await Task.Run(async () =>
            {
                await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
                {
                    LoadBackpacks(backpacks, UserBackpacksWithItems);
                });
            });
        }

        private static void LoadBackpacks(Backpack[] backpacks, ObservableCollection<BackpackWithItems> collection)
        {
            foreach (var backpack in backpacks)
            {
                var backpackWithItems = new BackpackWithItems(backpack);

                foreach (var itemBackpack in backpack.Items)
                {
                    backpackWithItems.Items.Add(itemBackpack.Item);
                }
                collection.Add(backpackWithItems);
            }
        }
    }
}
