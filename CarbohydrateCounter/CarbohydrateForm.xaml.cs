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
    public partial class CarbohydrateForm : PhoneApplicationPage
    {
        private IProduct _product;
        public CarbohydrateForm()
        {
            InitializeComponent();
            _product = (Product)PhoneApplicationService.Current.State["Product"];
            PhoneApplicationService.Current.State.Remove("Product");
            ProductName.Text = _product.Name;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "0")
            {
                textBox.Text = "";
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "") textBox.Text = "0";
            //foreach (char c in textBox.Text)
            //{
            //    if (!char.IsNumber(c))
            //    {
            //        MessageBox.Show("Dozwolone tylko wartości całkowite, bez spacji.");
            //        textBox.Focus();
            //    }
            //}
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            TextBox textBox = Weight;
            foreach (char c in textBox.Text)
            {
                if (!char.IsNumber(c))
                {
                    MessageBox.Show("Dozwolone tylko wartości całkowite, bez spacji.");
                    textBox.Focus();
                    return;
                }
            }

            if (PhoneApplicationService.Current.State.ContainsKey("ProductToAddBool"))
            {
                if ((bool)PhoneApplicationService.Current.State["ProductToAddBool"] && NavigationService.CanGoBack)
                {
                    Product product = _product as Product;
                    product.Weight = int.Parse(Weight.Text);
                    PhoneApplicationService.Current.State["ProductToAdd"] = product;
                    ((App) App.Current).ProductAddGoBack = true;
                    NavigationService.GoBack();
                }
            }
            else
            {
                PhoneApplicationService.Current.State["Weight"] = Weight.Text;
                PhoneApplicationService.Current.State["Product"] = _product;
                NavigationService.Navigate(new Uri("/Summary1Product.xaml", UriKind.Relative));
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (((App)App.Current).GoToFirstScreen && NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            base.OnNavigatedTo(e);
        }
    }
}