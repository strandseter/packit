using Microsoft.Toolkit.Uwp.Helpers;
using Packit.App.DataAccess;
using Packit.App.DataAccess.Http;
using Packit.App.DataLinks;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Packit.App.ViewModels
{
    public class BrowseBackpacksViewModel : ViewModel
    {
        private readonly ICustomBackpackDataAccess backpackDataAccess = new CustomBackpackDataAccessHttp();
        private ICommand loadedCommand;

        public ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new NetworkErrorHandlingRelayCommand<BrowseBackpacksPage>(async () => await LoadDataAsync(), PopUpService));

        public ObservableCollection<BackpackWithItems> AllBackpacksWithItems { get; } = new ObservableCollection<BackpackWithItems>();
        public ObservableCollection<BackpackWithItems> UserBackpacksWithItems { get; } = new ObservableCollection<BackpackWithItems>();

        public BrowseBackpacksViewModel(IPopUpService popUpService)
            :base(popUpService)
        {
        }

        private async Task LoadDataAsync()
        {
            await LoadAllBackpacks();
            await LoadUserBackpacks();
        }

        private async Task LoadAllBackpacks()
        {
            var backpacks = await backpackDataAccess.GetAllSharedBackpacksAsync();

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
            var backpacks = await backpackDataAccess.GetAllSharedBackpacksByUserAsync();

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
