using Packit.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using static Newtonsoft.Json.JsonConvert;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Packit.TestGui
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        static readonly Uri itemsUri = new Uri("http://localhost:52286/api/Items");
        readonly HttpClient _httpClient = new HttpClient();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ItemPage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadItems();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ////var item = new Item() { Title = "Yeet" };
            //PostItem(item);
            //LoadItems();
        }

        private async void LoadItems()
        {
            var result = await _httpClient.GetAsync(itemsUri);
            var json = await result.Content.ReadAsStringAsync();
            var items = DeserializeObject<Item[]>(json);

            ItemListView.ItemsSource = items;
        }

        private async void PostItem(Item item)
        {
            var json = SerializeObject(item);
            _ = await _httpClient.PostAsync(itemsUri, new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
        }
    }
}
