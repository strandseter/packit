using System;
using System.Windows.Input;
using Packit.App.DataAccess;
using Packit.App.Factories;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.Model;
using Packit.Model.NotifyPropertyChanged;

namespace Packit.App.ViewModels
{
    public class NewBackpackViewModel : Observable
    {
        private readonly IBasicDataAccess<Backpack> backpackDataAccess = new BasicDataAccessFactory<Backpack>().Create();

        public ICommand CancelCommand { get; set; }
        public ICommand NextCommand { get; set; }

        public Backpack NewBackpack { get; set; } = new Backpack() { Title = "", Description = "" }; 

        public NewBackpackViewModel()
        {
            CancelCommand = new RelayCommand(() => NavigationService.GoBack());

            NextCommand = new RelayCommand(async () => {

                if (await backpackDataAccess.AddAsync(NewBackpack))
                {
                    NavigationService.Navigate(typeof(SelectItemsPage), NewBackpack);
                }
            });
        }
    }
}
