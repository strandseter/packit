// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="NewItemViewModel.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Packit.App.DataAccess;
using Packit.App.DataLinks;
using Packit.App.Factories;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.Model;
using Windows.Storage;

namespace Packit.App.ViewModels
{
    /// <summary>
    /// Class NewItemViewModel.
    /// Implements the <see cref="Packit.App.ViewModels.ViewModel" />
    /// </summary>
    /// <seealso cref="Packit.App.ViewModels.ViewModel" />
    public class NewItemViewModel : ViewModel
    {
        #region private fields
        /// <summary>
        /// The items data access
        /// </summary>
        private readonly IBasicDataAccess<Item> itemsDataAccess = new BasicDataAccessFactory<Item>().Create();
        /// <summary>
        /// The images data access
        /// </summary>
        private readonly ImagesDataAccessHttp imagesDataAccess = new ImagesDataAccessHttp();
        /// <summary>
        /// The local image
        /// </summary>
        private StorageFile localImage;
        /// <summary>
        /// The title is valid
        /// </summary>
        private bool titleIsValid;
        #endregion

        #region public properties
        /// <summary>
        /// Gets or sets the cancel command.
        /// </summary>
        /// <value>The cancel command.</value>
        public ICommand CancelCommand { get; set; }
        /// <summary>
        /// Gets or sets the save command.
        /// </summary>
        /// <value>The save command.</value>
        public ICommand SaveCommand { get; set; }
        /// <summary>
        /// Gets or sets the image device command.
        /// </summary>
        /// <value>The image device command.</value>
        public ICommand ImageDeviceCommand { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [title is valid].
        /// </summary>
        /// <value><c>true</c> if [title is valid]; otherwise, <c>false</c>.</value>
        public bool TitleIsValid
        {
            get => titleIsValid;
            set => Set(ref titleIsValid, value);
        }

        /// <summary>
        /// Gets or sets the item image link.
        /// </summary>
        /// <value>The item image link.</value>
        public ItemImageLink ItemImageLink { get; set; } = new ItemImageLink() { Item = new Item() { Title = "", Description = "" } };
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="NewItemViewModel"/> class.
        /// </summary>
        /// <param name="popUpService">The pop up service.</param>
        public NewItemViewModel(IPopUpService popUpService)
            :base(popUpService)
        {
            CancelCommand = new RelayCommand(() => NavigationService.GoBack());

            SaveCommand = new NetworkErrorHandlingRelayCommand<bool, ItemsPage>(async param =>
            {
                await Task.WhenAll(AddItemAsync(), DisableCommand());
               
            }, PopUpService, param => param);

            ImageDeviceCommand = new RelayCommand(async () =>
            {
                await PickLocalImageAsync();
            });
        }
        #endregion

        #region private add methods
        /// <summary>
        /// add item as an asynchronous operation.
        /// </summary>
        private async Task AddItemAsync()
        {
            if (localImage == null)
                await AddItemRequestAsync();
            else
                await AddItemAndImagerequestAsync();
        }

        /// <summary>
        /// add item request as an asynchronous operation.
        /// </summary>
        private async Task AddItemRequestAsync()
        {
            if (await itemsDataAccess.AddAsync(ItemImageLink.Item))
                NavigationService.GoBack();
            else
                await PopUpService.ShowCouldNotSaveAsync(ItemImageLink.Item.Title);
        }

        /// <summary>
        /// add item and imagerequest as an asynchronous operation.
        /// </summary>
        private async Task AddItemAndImagerequestAsync()
        {
            var randomImageName = GenerateImageName();
            ItemImageLink.Item.ImageStringName = randomImageName;

            if (await itemsDataAccess.AddAsync(ItemImageLink.Item) && await imagesDataAccess.AddImageAsync(localImage, randomImageName))
                NavigationService.GoBack();
            else
                await PopUpService.ShowCouldNotSaveAsync(ItemImageLink.Item.Title);
        }
        #endregion

        /// <summary>
        /// pick local image as an asynchronous operation.
        /// </summary>
        private async Task PickLocalImageAsync()
        {
            localImage = await FileService.GetImageFromDeviceAsync();

            if (localImage == null)
                return;

            ItemImageLink.Image = await FileService.StorageFileToBitmapImageAsync(localImage);
        }
    }
}
