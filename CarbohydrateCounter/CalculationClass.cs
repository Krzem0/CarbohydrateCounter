using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace CarbohydrateCounter
{
    public static class CalculationClass
    {
        public static float CalculateCC(string weight, IProduct product)
        {
            float cc = int.Parse(weight) / (float)product.Intercharger;
            return cc;
        }

        public static float CalculateCalories(string weight, IProduct product)
        {
            float cc = CalculateCC(weight, product);
            return cc*product.Calories;
        }

        public static float CalculateCCFromList(List<Product> products)
        {
            float ccSum = 0;
            foreach (var product in products)
            {
                ccSum += CalculateCC(product.Weight.ToString(), product);
            }
            return ccSum;
        }

        public static float CalculateCaloriesFromList(List<Product> products)
        {
            float calSum = 0;
            foreach (var product in products)
            {
                calSum += CalculateCC(product.Weight.ToString(), product)*product.Calories;
            }
            return calSum;
        }

        public static float CalculateWeightFromList(List<Product> products)
        {
            float weight = products.Aggregate<Product, float>(0, (current, product) => current + product.Weight);
            return weight;
        }
    }
}
