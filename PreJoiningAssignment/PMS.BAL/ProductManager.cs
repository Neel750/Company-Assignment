using PMS.BAL.Interface;
using PMS.DAL.Repository;
using PMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PMS.BAL
{
    public class ProductManager : IProductManager
    {
        private readonly IProductRepository _productRepository;
        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public string CreateProduct(ProductData model)
        {
            return _productRepository.CreateProduct(model);
        }

        public string DeleteProduct(int id)
        {
            return _productRepository.DeleteProduct(id);
        }

        public IEnumerable<ProductData> GetAllProducts(int id, string Sorting_Order, string searchBy, string search)
        {
            IEnumerable<ProductData> products = _productRepository.GetAllProducts(id);//Gets All Products
            switch (searchBy)//Implement Search By Name, By Category, By Tags
            {
                case "Name":
                    products = products.Where(x => x.ProductName.Contains(search)).ToList();
                    break;
                case "Category":
                    products = products.Where(x => x.Category.Contains(search)).ToList();
                    break;
                case "Tag":
                    products = products.Where(x => x.Tags.Contains(search)).ToList();
                    break;
                default:
                    break;
            }
            switch (Sorting_Order)//Implementing Sorting By Name, By Price
            {
                case "Name_Description"://name descending
                    products = products.OrderByDescending(pro => pro.ProductName);
                    break;
                case "Price_Low_To_High"://price ascending
                    products = products.OrderBy(pro => pro.Price);
                    break;
                case "Price_High_To_Low"://price descending
                    products = products.OrderByDescending(pro => pro.Price);
                    break;
                default://name ascending
                    products = products.OrderBy(pro => pro.ProductName);
                    break;
            }
            return products;
        }

        public ProductData GetProduct(int id)
        {
            return _productRepository.GetProduct(id);
        }

        public string UpdateProduct(ProductData model)
        {
            return _productRepository.UpdateProduct(model);
        }
    }
}
