using Packit.App.DataLinks;
using Packit.App.ViewModels;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Packit.App.Views
{
    public sealed partial class ListviewDropdownControl : UserControl
    {
        public string TripTitle
        {
            get { return (string)GetValue(TripTitleProperty); }
            set { SetValue(TripTitleProperty, value); }
        }

        public TripBackpackItemLinkOld TripBackpackItemLink
        {
            get { return (TripBackpackItemLinkOld)GetValue(TripBackpackItemLinkProperty); }
            set { SetValue(TripBackpackItemLinkProperty, value); }
        }

        public TripBackpackLink TripBackpackLink
        {
            get { return (TripBackpackLink)GetValue(TripBackLinkProperty); }
            set { SetValue(TripBackLinkProperty, value); }
        }

        //public ICommand TestCommand
        //{
        //    get { return (ICommand)GetValue(TestCommandProperty); }
        //    set { SetValue(TestCommandProperty, value); }
        //}

        public static readonly DependencyProperty TripBackLinkProperty =
            DependencyProperty.Register("TripBackpackLink", typeof(TripBackpackLink), typeof(ListviewDropdownControl), null);

        public static readonly DependencyProperty TripBackpackItemLinkProperty =
            DependencyProperty.Register("TripBackpackItemLink", typeof(TripBackpackItemLinkOld), typeof(ListviewDropdownControl), null);

        public static readonly DependencyProperty TripTitleProperty =
            DependencyProperty.Register("TripTitle", typeof(string),
               typeof(ListviewDropdownControl), null);

        //public static readonly DependencyProperty TestCommandProperty =
        //    DependencyProperty.Register("TestCommandProperty", typeof(ICommand), typeof(ListviewDropdownControl), null);

        public ListviewDropdownControl() => InitializeComponent();

        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e) => dropDown.Visibility = Visibility.Visible;

        private void Button_Click(object sender, RoutedEventArgs e) => dropDown.Visibility = Visibility.Collapsed;
    }
}
