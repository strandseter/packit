// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-25-2020
//
// Last Modified By : ander
// Last Modified On : 05-25-2020
// ***********************************************************************
// <copyright file="ItemChecker.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Packit.App.DataAccess;
using Packit.App.Services;
using Packit.Exceptions;
using Packit.Model;
using Packit.Model.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Packit.App.Helpers
{
    /// <summary>
    /// Class ItemChecker.
    /// </summary>
    public class ItemChecker
    {
        /// <summary>
        /// The item
        /// </summary>
        private readonly Item item;
        /// <summary>
        /// The backpack
        /// </summary>
        private readonly Backpack backpack;
        /// <summary>
        /// The trip
        /// </summary>
        private readonly Trip trip;
        /// <summary>
        /// The is checked
        /// </summary>
        private readonly bool isChecked;
        /// <summary>
        /// The pop up service
        /// </summary>
        private readonly IPopUpService popUpService;
        /// <summary>
        /// The checks data access
        /// </summary>
        private readonly IBasicDataAccess<Check> checksDataAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemChecker"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="backpack">The backpack.</param>
        /// <param name="trip">The trip.</param>
        /// <param name="isChecked">if set to <c>true</c> [is checked].</param>
        /// <param name="popUpService">The pop up service.</param>
        /// <param name="checksDataAccess">The checks data access.</param>
        public ItemChecker(Item item, Backpack backpack, Trip trip, bool isChecked, IPopUpService popUpService, IBasicDataAccess<Check> checksDataAccess)
        {
            this.item = item;
            this.backpack = backpack;
            this.trip = trip;
            this.isChecked = isChecked;
            this.popUpService = popUpService;
            this.checksDataAccess = checksDataAccess;
        }

        /// <summary>
        /// Handles the item check.
        /// </summary>
        public async Task HandleItemCheck()
        {
            var check = new Check()
            {
                IsChecked = true,
                ItemId = item.ItemId,
                BackpackId = backpack.BackpackId,
                TripId = trip.TripId
            };

            try
            {
                if (isChecked)
                {
                    item.Check = check;

                    if (await checksDataAccess.AddAsync(check))
                        item.Check.IsChecked = true;
                    else
                    {
                        item.Check.IsChecked = false;
                        await popUpService.ShowInternetConnectionErrorAsync();
                    }
                }

                if (!isChecked)
                {
                    if (await checksDataAccess.DeleteAsync(item.Check))
                        item.Check.IsChecked = false;
                    else
                    {
                        item.Check.IsChecked = true;
                        await popUpService.ShowInternetConnectionErrorAsync();
                    }
                }
            }
            catch (HttpRequestException)
            {
                if (isChecked)
                {
                    item.Check.IsChecked = false;
                    throw;
                }
                else
                {
                    item.Check.IsChecked = true;
                    throw;
                }

            }
            catch (NetworkConnectionException)
            {
                if (isChecked)
                {
                    item.Check.IsChecked = false;
                    throw;
                }
                else
                {
                    item.Check.IsChecked = true;
                    throw;
                }
            }
        }
    }
}
