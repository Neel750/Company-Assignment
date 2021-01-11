using PMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PMS.BAL.Interface
{
    public interface IProductManager
    {
        IEnumerable<ProductData> GetAllProducts(int id, string Sorting_Order, string searchBy, string search);
        ProductData GetProduct(int id);
        string CreateProduct(ProductData model);
        string UpdateProduct(ProductData model);
        string DeleteProduct(int id);
    }
}
