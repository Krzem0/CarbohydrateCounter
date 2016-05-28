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
    public partial class SummaryXProducts : PhoneApplicationPage
    {
        private List<Product> _productList;
        public SummaryXProducts()
        {
            InitializeComponent();
        }
        
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (PhoneApplicationService.Current.State.ContainsKey("ProductListToCalculate"))
            {
                _productList = PhoneApplicationService.Current.State["ProductListToCalculate"] as List<Product>;
                PhoneApplicationService.Current.State.Remove("ProductListToCalculate");
            }
            else
            {
                _productList = new List<Product>();
            }
            AddTextToTextBlocks();
            base.OnNavigatedTo(e);
        }

        private void AddTextToTextBlocks()
        {
            AllCC.Text = CalculationClass.CalculateCCFromList(_productList).ToString("0.00");
            AllCal.Text = CalculationClass.CalculateCaloriesFromList(_productList).ToString("0.00");
            Allweight.Text = CalculationClass.CalculateWeightFromList(_productList).ToString();
        }

        private void MealProductList_Loaded(object sender, RoutedEventArgs e)
        {
            _productList.Sort(SortByName);
            List<MyCC> myCcs = new List<MyCC>();
            foreach (Product product in _productList)
            {
                MyCC myCc = new MyCC
                                {
                                    Product = product,
                                    CC = CalculationClass.CalculateCC(product.Weight.ToString(),product).ToString("0.00")
                                };
                myCcs.Add(myCc);
            }
            MealProductList.DataContext = null;
            MealProductList.DataContext = myCcs;
        }

        private int SortByName(Product p1, Product p2)
        {
            return string.Compare(p1.Name, p2.Name);
        }

        private void MealProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MealProductList.SelectedItem = null;
        }
    }

    public class MyCC
    {
        public Product Product { get; set; }
        public string CC { get; set; }
    }
}