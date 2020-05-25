using Packit.App.DataAccess;
using Packit.App.DataLinks;
using Packit.App.Services;
using Packit.App.Wrappers;
using Packit.Exceptions;
using Packit.Model;
using Packit.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.Helpers
{
    public class ItemChecker
    {
        private Item item;
        private Backpack backpack;
        private Trip trip;
        private bool isChecked;
        private IPopUpService popUpService;
        private readonly IBasicDataAccess<Check> checksDataAccess;

        public ItemChecker(Item item, Backpack backpack, Trip trip, bool isChecked, IPopUpService popUpService, IBasicDataAccess<Check> checksDataAccess)
        {
            this.item = item;
            this.backpack = backpack;
            this.trip = trip;
            this.isChecked = isChecked;
            this.popUpService = popUpService;
            this.checksDataAccess = checksDataAccess;
        }

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
