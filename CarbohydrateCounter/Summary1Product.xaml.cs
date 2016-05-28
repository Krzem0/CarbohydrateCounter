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
    public partial class Summary1Product : PhoneApplicationPage
    {
        private IProduct _product;
        public Summary1Product()
        {
            InitializeComponent();
            _product = (Product)PhoneApplicationService.Current.State["Product"];
            PhoneApplicationService.Current.State.Remove("Product");
            Name.Text = _product.Name;
            Weight.Text = (String)PhoneApplicationService.Current.State["Weight"];
            CC.Text = CalculationClass.CalculateCC((String)PhoneApplicationService.Current.State["Weight"], _product).ToString("0.00");
            Calories.Text = CalculationClass.CalculateCalories((String)PhoneApplicationService.Current.State["Weight"], _product).ToString("0.00");
            PhoneApplicationService.Current.State.Remove("Weight");
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            ((App) App.Current).GoToFirstScreen = true;
            NavigationService.GoBack();
        }
    }
}