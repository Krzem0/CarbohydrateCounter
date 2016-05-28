using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace CarbohydrateCounter
{
    public partial class ProductPanorama : PhoneApplicationPage
    {
        public ProductPanorama()
        {
            InitializeComponent();
        }

        private void NavigateToProductList()
        {
            NavigationService.Navigate(new Uri("/ProductsList.xaml", UriKind.Relative));
        }

        private void Bakery_Click(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State.Add("ListName","pieczywo");
            NavigateToProductList();
        }

        private void Pasta_Click(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State.Add("ListName", "makaron_platki");
            NavigateToProductList();
        }

        private void Vegetables_Click(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State.Add("ListName", "warzywa");
            NavigateToProductList();
        }

        private void Flower_Click(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State.Add("ListName", "maka_kasza");
            NavigateToProductList();
        }

        private void Milk_Click(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State.Add("ListName", "mleko_napojemleczne");
            NavigateToProductList();
        }

        private void Fruits_Click(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State.Add("ListName", "owoce");
            NavigateToProductList();
        }

        private void Juices_Click(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State.Add("ListName", "sokiowocowe_napoje");
            NavigateToProductList();
        }

        private void Cakes_Click(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State.Add("ListName", "herbatniki_ciasta");
            NavigateToProductList();
        }

        private void Confectionery_Click(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State.Add("ListName", "slodycze");
            NavigateToProductList();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (((App)App.Current).GoToFirstScreen && NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            if (((App)App.Current).ProductAddGoBack && NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            base.OnNavigatedTo(e);
        }
    }
}