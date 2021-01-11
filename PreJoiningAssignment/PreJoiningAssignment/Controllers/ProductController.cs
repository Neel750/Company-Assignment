using PMS.BAL.Interface;
using PMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PMS.WebAPI.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductManager _productManager;

        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public IHttpActionResult GetAllProducts(int id, string Sorting_Order=null, string searchBy=null, string search=null)
        {
            return Ok(_productManager.GetAllProducts(id,Sorting_Order,searchBy,search));
        }

        public IHttpActionResult GetProduct(int id)
        {
            return Ok(_productManager.GetProduct(id));
        }

        public IHttpActionResult PostCreateProduct([FromBody] ProductData model)
        {
           return Ok(_productManager.CreateProduct(model));
        }

        public IHttpActionResult PutUpdateProduct(ProductData model)
        {
            return Ok(_productManager.UpdateProduct(model));
        }

        public IHttpActionResult DeleteProduct(int id)
        {
            return Ok(_productManager.DeleteProduct(id));
        }   
    }
}
