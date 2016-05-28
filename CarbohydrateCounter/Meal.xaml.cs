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
    public partial class Meal : PhoneApplicationPage
    {
        private List<Product> _productList;
        private string _editBoxContent;

        public Meal()
        {
            _productList = new List<Product>();
            InitializeComponent();
            InitAppBar();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (PhoneApplicationService.Current.State.ContainsKey("ProductToAdd"))
            {
                Product product = PhoneApplicationService.Current.State["ProductToAdd"] as Product;
                _productList.Add(product);
                PhoneApplicationService.Current.State.Remove("ProductToAdd");
            }
            if (((App)App.Current).ProductAddGoBack)
            {
                ((App)App.Current).ProductAddGoBack = !((App)App.Current).ProductAddGoBack;
            }
            base.OnNavigatedTo(e);
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (PhoneApplicationService.Current.State.ContainsKey("ProductToAddBool"))
            {
                PhoneApplicationService.Current.State.Remove("ProductToAddBool");
            }
            base.OnBackKeyPress(e);
        }

        private void MealProductList_Loaded(object sender, RoutedEventArgs e)
        {
            if (_productList.Count > 0)
            {
                _productList.Sort(SortByName);
                MealProductList.DataContext = null;
                MealProductList.DataContext = _productList;
                MealProductList.Visibility = Visibility.Visible;
                Warning.Visibility = Visibility.Collapsed;
                _equalButton.IsEnabled = true;
            }
            else
            {
                MealProductList.Visibility = Visibility.Collapsed;
                Warning.Visibility = Visibility.Visible;
                _equalButton.IsEnabled = false;
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            PhoneApplicationService.Current.State["ProductToAddBool"] = true;
            NavigationService.Navigate(new Uri("/ProductPanorama.xaml", UriKind.Relative));
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            foreach (var selectedItem in MealProductList.SelectedItems)
            {
                Product product = selectedItem as Product;
                _productList.Remove(product);
            }
            _productList.Sort(SortByName);
            MealProductList.DataContext = null;
            MealProductList.DataContext = _productList;
            if (_productList.Count == 0)
            {
                _equalButton.IsEnabled = false;
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            EditPanel.Visibility = EditPanel.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            EditBox.Text = ((Product) MealProductList.SelectedItems[0]).Weight.ToString();
        }

        private void EqualButton_Click(object sender, EventArgs e)
        {
            PhoneApplicationService.Current.State["ProductListToCalculate"] = _productList;
            NavigationService.Navigate(new Uri("/SummaryXProducts.xaml", UriKind.Relative));
        }

        private void MealProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            if (listBox.SelectedItems.Count > 0)
            {
                _deleteButton.IsEnabled = _editButton.IsEnabled = true;
            }
            else
            {
                _deleteButton.IsEnabled = _editButton.IsEnabled = false;
                EditPanel.Visibility = Visibility.Collapsed;
            }
        }

        #region AppBar
        private ApplicationBarIconButton _deleteButton;
        private ApplicationBarIconButton _editButton;
        private ApplicationBarIconButton _equalButton;
        private ApplicationBarIconButton _addButton;
        private void InitAppBar()
        {
            ApplicationBar applicationBar = new ApplicationBar();

            _deleteButton = new ApplicationBarIconButton(new Uri("/Images/minus.png", UriKind.Relative));
            _deleteButton.IsEnabled = false;
            _deleteButton.Text = "Usuń";
            _deleteButton.Click += DeleteButton_Click;
            applicationBar.Buttons.Add(_deleteButton);

            _editButton = new ApplicationBarIconButton(new Uri("/Images/edit.png", UriKind.Relative));
            _editButton.IsEnabled = false;
            _editButton.Text = "Edytuj";
            _editButton.Click += EditButton_Click;
            applicationBar.Buttons.Add(_editButton);

            _equalButton = new ApplicationBarIconButton(new Uri("/Images/equal.png", UriKind.Relative));
            _equalButton.IsEnabled = false;
            _equalButton.Text = "Oblicz";
            _equalButton.Click += EqualButton_Click;
            applicationBar.Buttons.Add(_equalButton);

            _addButton = new ApplicationBarIconButton(new Uri("/Images/add.png", UriKind.Relative));
            _addButton.IsEnabled = true;
            _addButton.Text = "Dodaj";
            _addButton.Click += AddButton_Click;
            applicationBar.Buttons.Add(_addButton);

            ApplicationBar = applicationBar;
        }
        #endregion

        private int SortByName(Product p1, Product p2)
        {
            return string.Compare(p1.Name, p2.Name);
        }

        private void EditBox_GotFocus(object sender, RoutedEventArgs e)
        {
            _editBoxContent = ((TextBox) sender).Text;
            ((TextBox) sender).Text = "";
        }

        private void EditBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "") textBox.Text = _editBoxContent;
            //foreach (char c in textBox.Text)
            //{
            //    if (!char.IsNumber(c))
            //    {
            //        MessageBox.Show("Dozwolone tylko wartości całkowite, bez spacji.");
            //        textBox.Focus();
            //    }
            //}
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox textBox = EditBox;
            foreach (char c in textBox.Text)
            {
                if (!char.IsNumber(c))
                {
                    MessageBox.Show("Dozwolone tylko wartości całkowite, bez spacji.");
                    textBox.Focus();
                    return;
                }
            }

            foreach (var item in MealProductList.SelectedItems)
            {
                Product product = _productList.First(i => i == item);
                product.Weight = int.Parse(EditBox.Text);
            }
            _productList.Sort(SortByName);
            MealProductList.DataContext = null;
            MealProductList.DataContext = _productList;
        }
    }
}