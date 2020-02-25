using Packit.Model;
using System;
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
        static readonly Uri itemsUri = new Uri("http://localhost:52286/api/Items");
        readonly HttpClient _httpClient = new HttpClient();

        public ItemsPage()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void ItemPage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadItems();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var item = new Item() { Title = "Yeet" };
            PostItem(item);
            LoadItems();
        }

        private async void LoadItems()
        {
            var result = await _httpClient.GetAsync(itemsUri);
            var json = await result.Content.ReadAsStringAsync();
            var items = DeserializeObject<Item[]>(json);

            ItemsAdaptive1.ItemsSource = items;
        }

        private async void PostItem(Item item)
        {
            var json = SerializeObject(item);
            _ = await _httpClient.PostAsync(itemsUri, new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
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
