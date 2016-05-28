using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CarbohydrateCounter
{
    public class LinqToXMLDB
    {
        private List<IProduct> _products;

        public LinqToXMLDB(string FileName)
        {
            _products = LoadProducts(FileName);
        }

        public List<IProduct> Products { get { return _products; } }

        private List<IProduct> LoadProducts(string FileName)
        {
            List<IProduct> products = new List<IProduct>();
            XElement file = XElement.Load(@".\DB\"+FileName+".xml");

            foreach (XElement node in file.Nodes())
            {
                IProduct iProduct = new Product();
                List<XNode> product = node.Nodes().ToList();
                iProduct.Id = int.Parse(((XElement)product[0]).Value);
                iProduct.Name = ((XElement) product[1]).Value.Trim();
                iProduct.Intercharger = int.Parse(((XElement)product[2]).Value);
                iProduct.Calories = int.Parse(((XElement)product[3]).Value);
                products.Add(iProduct);
            }
            return products;
        }
    }
}
