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
using Microsoft.Phone.Shell;

namespace CarbohydrateCounter
{
    public partial class ProductsList : PhoneApplicationPage
    {
        private string _productListName;
        public ProductsList()
        {
            _productListName = (String)PhoneApplicationService.Current.State["ListName"];
            PhoneApplicationService.Current.State.Remove("ListName");
            InitializeComponent();
            ProductTitle.Text = _productListName.Replace('_', ' ');
        }

        private void ProductList_Loaded(object sender, RoutedEventArgs e)
        {
            LinqToXMLDB db = new LinqToXMLDB(_productListName);
            List<IProduct> list = db.Products;
            list.Sort(SortByName);
            ProductList.DataContext = list;
        }

        private void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count>0)
            {
                IProduct product = e.AddedItems[0] as Product;
                PhoneApplicationService.Current.State.Add("Product", product);
                NavigationService.Navigate(new Uri("/CarbohydrateForm.xaml", UriKind.Relative));
            }
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

        private int SortByName(IProduct p1, IProduct p2)
        {
            return string.Compare(p1.Name, p2.Name);
        }
    }
}