using Flooring.Models;
using Flooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.BLL
{
    public class ProductManager
    {
        IProductsRepository _productsRepository;

        public ProductManager(IProductsRepository repo)
        {
            _productsRepository = repo;
        }

        public List<Products> GetListOfAllProducts()
        {
            return _productsRepository.ListOfAllProducts();
        }

       public bool CheckForProduct(string productType)
       {
            return GetListOfAllProducts().Any(x => x.ProductType == productType);
       }
    }
}
