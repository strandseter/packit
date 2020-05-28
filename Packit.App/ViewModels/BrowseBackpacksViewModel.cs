// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-28-2020
//
// Last Modified By : ander
// Last Modified On : 05-28-2020
// ***********************************************************************
// <copyright file="BrowseBackpacksViewModel.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Toolkit.Uwp.Helpers;
using Packit.App.DataAccess;
using Packit.App.DataAccess.Http;
using Packit.App.DataLinks;
using Packit.App.Factories;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.Model;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Packit.App.ViewModels
{
    /// <summary>
    /// Class BrowseBackpacksViewModel.
    /// Implements the <see cref="Packit.App.ViewModels.ViewModel" />
    /// </summary>
    /// <seealso cref="Packit.App.ViewModels.ViewModel" />
    public class BrowseBackpacksViewModel : ViewModel
    {
        /// <summary>
        /// The custom backpack data access
        /// </summary>
        private readonly ICustomBackpackDataAccess customBackpackDataAccess = new CustomBackpackDataAccessHttp();
        /// <summary>
        /// The backpack data access
        /// </summary>
        private readonly IBasicDataAccess<Backpack> backpackDataAccess = new BasicDataAccessFactory<Backpack>().Create();
        /// <summary>
        /// The backpack item data access
        /// </summary>
        private readonly IRelationDataAccess<Backpack, Item> backpackItemDataAccess = new RelationDataAccessFactory<Backpack, Item>().Create();
        /// <summary>
        /// The item data access
        /// </summary>
        private readonly IBasicDataAccess<Item> itemDataAccess = new BasicDataAccessFactory<Item>().Create();
        /// <summary>
        /// The loaded command
        /// </summary>
        private ICommand loadedCommand;

        /// <summary>
        /// Gets the loaded command.
        /// </summary>
        /// <value>The loaded command.</value>
        public ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new NetworkErrorHandlingRelayCommand<BrowseBackpacksPage>(async () => await LoadDataAsync(), PopUpService));
        /// <summary>
        /// Gets or sets the steal command.
        /// </summary>
        /// <value>The steal command.</value>
        public ICommand StealCommand { get; set; }
        /// <summary>
        /// Gets or sets the stop sharing command.
        /// </summary>
        /// <value>The stop sharing command.</value>
        public ICommand StopSharingCommand { get; set; }

        /// <summary>
        /// Gets all backpacks with items.
        /// </summary>
        /// <value>All backpacks with items.</value>
        public ObservableCollection<BackpackWithItems> AllBackpacksWithItems { get; } = new ObservableCollection<BackpackWithItems>();
        /// <summary>
        /// Gets the user backpacks with items.
        /// </summary>
        /// <value>The user backpacks with items.</value>
        public ObservableCollection<BackpackWithItems> UserBackpacksWithItems { get; } = new ObservableCollection<BackpackWithItems>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowseBackpacksViewModel"/> class.
        /// </summary>
        /// <param name="popUpService">The pop up service.</param>
        public BrowseBackpacksViewModel(IPopUpService popUpService)
            :base(popUpService)
        {
            StealCommand = new NetworkErrorHandlingRelayCommand<BackpackWithItems, BrowseBackpacksPage>(async param =>
            {
                await Task.WhenAll(StealBackpack(param), DisableCommand());
            }, PopUpService, param => param != null);

            StopSharingCommand = new NetworkErrorHandlingRelayCommand<BackpackWithItems, BrowseBackpacksPage>(async param =>
            {
                await Task.WhenAll(StopSharing(param), DisableCommand());
            }, PopUpService, param => param != null);
        }

        /// <summary>
        /// Stops the sharing.
        /// </summary>
        /// <param name="backpackWithItems">The backpack with items.</param>
        /// <exception cref="ArgumentNullException">backpackWithItems</exception>
        private async Task StopSharing(BackpackWithItems backpackWithItems)
        {
            if (backpackWithItems == null)
                throw new ArgumentNullException(nameof(backpackWithItems));

            backpackWithItems.Backpack.IsShared = false;

            if (await backpackDataAccess.UpdateAsync(backpackWithItems.Backpack))
            {
                UserBackpacksWithItems.Remove(backpackWithItems);
            }
            else
                await PopUpService.ShowCouldNotSaveAsync(backpackWithItems.Backpack.Title);
        }

        /// <summary>
        /// Steals the backpack.
        /// </summary>
        /// <param name="backpackWithItems">The backpack with items.</param>
        /// <exception cref="ArgumentNullException">backpackWithItems</exception>
        private async Task StealBackpack(BackpackWithItems backpackWithItems)
        {
            if (backpackWithItems == null)
                throw new ArgumentNullException(nameof(backpackWithItems));

            var isSuccess = true;

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
                await PopUpService.ShowWasSuccessAsync(backpackWithItems.Backpack.Title, "added");
            else
                await PopUpService.ShowCouldNotSaveAsync(backpackWithItems.Backpack.Title);
        }

        /// <summary>
        /// load data as an asynchronous operation.
        /// </summary>
        private async Task LoadDataAsync()
        {
            await LoadAllBackpacks();
            await LoadUserBackpacks();
        }

        /// <summary>
        /// Loads all backpacks.
        /// </summary>
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

        /// <summary>
        /// Loads the user backpacks.
        /// </summary>
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

        /// <summary>
        /// Loads the backpacks.
        /// </summary>
        /// <param name="backpacks">The backpacks.</param>
        /// <param name="collection">The collection.</param>
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
