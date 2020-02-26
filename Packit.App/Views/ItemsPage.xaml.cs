/*Guide
* 1. Using Statements
* 2. Constructors
* 3. Getters
* 4. Setters
* 5. Overridden Methods
 */

using Packit.App.Services;
using Packit.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.Web.Http;
using static Newtonsoft.Json.JsonConvert;

namespace Packit.App.Views
{
    public sealed partial class ItemsPage : Page, INotifyPropertyChanged, IWindowDataTransfer
    {
        private static readonly Uri itemsUri = new Uri("http://localhost:52286/api/Items");
        private readonly HttpClient httpClient = new HttpClient();
        private static ObservableCollection<Item> ItemsList = new ObservableCollection<Item>();

        public ItemsPage()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void ItemPage_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadItems().ConfigureAwait(true);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow<NewEditItemPage>(null);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow<NewEditItemPage>(null);
        }

        private async Task LoadItems()
        {
            var result = await httpClient.GetAsync(itemsUri);
            var json = await result.Content.ReadAsStringAsync();
            var items = DeserializeObject<Item[]>(json);
            ItemsList = new ObservableCollection<Item>(items);
            ItemsAdaptive1.ItemsSource = ItemsList;
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

        public void ChangeWindow<T>(object o)
        {
            Frame.Navigate(typeof(T), ItemsAdaptive1.SelectedItem, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromBottom });
        }
    }
}
