using PMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PMS.DAL.Repository
{
    public interface IProductRepository
    {
        List<ProductData> GetAllProducts(int id);
        ProductData GetProduct(int id);
        string CreateProduct(ProductData model);
        string UpdateProduct(ProductData model);
        string DeleteProduct(int id);
    }
}
