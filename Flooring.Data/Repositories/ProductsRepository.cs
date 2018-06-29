using Flooring.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Flooring.Models;
using Flooring.Models.Interfaces;

namespace Flooring.Data
{
    public class ProductsRepository : IProductsRepository
    {
        //public static List<Products> allProductsList = List(Settings.productsFilePath);

        public List<Products> ListOfAllProducts()
        {
            List<Products> products = new List<Products>();
            using (StreamReader sr = new StreamReader(Settings.productsFilePath))
            {
                sr.ReadLine();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Products newProduct = new Products();

                    string[] columns = line.Split(',');
                    newProduct.ProductType = columns[0].ToUpper();
                    newProduct.CostPerSqFt = decimal.Parse(columns[1]);
                    newProduct.LaborCostPerSqFt = decimal.Parse(columns[2]);
                    products.Add(newProduct);
                }
                return products;
            }
        }

        public Products LoadProducts(string productType)
        {
            Products customerProduct = ListOfAllProducts().Where(x => x.ProductType == productType).FirstOrDefault();
            if(customerProduct != null)
            {
                return customerProduct;
            }
            Console.WriteLine("That product was not found");
            return customerProduct;
        }

    }
}
