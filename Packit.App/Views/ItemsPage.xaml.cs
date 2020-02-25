using Packit.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;
using static Newtonsoft.Json.JsonConvert;

namespace Packit.App.Views
{
    public sealed partial class ItemsPage : Page, INotifyPropertyChanged
    {
        private static readonly Uri itemsUri = new Uri("http://localhost:52286/api/Items");
        private readonly HttpClient httpClient = new HttpClient();
        private static ObservableCollection<Item> ItemsList = new ObservableCollection<Item>();

        public ItemsPage()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void ItemPage_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeItems();
            PopulateGridView();
        }

        private void PopulateGridView()
        {
            ItemsAdaptive1.ItemsSource = ItemsList;
        }


        private async void InitializeItems()
        {
            var result = await httpClient.GetAsync(itemsUri);
            var json = await result.Content.ReadAsStringAsync();
            var items = DeserializeObject<Item[]>(json);
            foreach (Item item in items)
                ItemsList.Add(item);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            var item = new Item() { Title = "Yeet" };
            ItemsList.Add(item);
        }

        private async void PostItem(Item item)
        {
            var json = SerializeObject(item);

            try
            {
                _ = await httpClient.PostAsync(itemsUri, new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
            }
            catch (ArgumentNullException ex)
            {

            }
            catch(InvalidOperationException ex)
            {

            }
            catch(Exception ex)
            {

            }
        }


        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
