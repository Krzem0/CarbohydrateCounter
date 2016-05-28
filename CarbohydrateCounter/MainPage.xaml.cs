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
using Microsoft.Phone.Controls;

namespace CarbohydrateCounter
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ProductPanorama.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (((App)App.Current).GoToFirstScreen && !NavigationService.CanGoBack)
                ((App) App.Current).GoToFirstScreen = !((App) App.Current).GoToFirstScreen;
            base.OnNavigatedTo(e);
        }

        private void MealButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Meal.xaml", UriKind.Relative));
        }
    }
}