using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarbohydrateCounter
{
    public interface IProduct
    {
        // Id produktu
        int Id { get; set; }

        // Nazwa produktu
        string Name { get; set; }

        // Wymiennik
        int Intercharger { get; set; }

        // Kalorie
        int Calories { get; set; }
    }
}
