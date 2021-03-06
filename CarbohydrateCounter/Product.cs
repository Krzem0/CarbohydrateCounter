﻿using System;
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
    public class Product : IProduct
    {
        public int Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public int Intercharger
        {
            get; set;
        }

        public int Calories
        {
            get; set;
        }

        public int Weight { get; set; }
    }
}
