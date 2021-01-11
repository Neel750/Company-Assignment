using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMS.DAL.Repository;
using PMS.Model;
using System;
using System.Linq;

namespace PMS.Tests
{
    [TestClass]
    public class ProductRepositoryTest
    {
        [TestMethod]
        public void TestCreateProduct()
        {
            var repo = new ProductRepository();
            ProductData product = new ProductData
            {
                UserId = 2,
                ProductName = "Test",
                Category = "Test",
                SmallDescription = "Test",
                SmallImage = "Test",
                LongDescription = "Test",
                LongImage ="Test",
                Tags = "Test",
                Price = 12,
                Quantity = 12

            };

            var result = repo.CreateProduct(product);
            Assert.AreEqual("Successfully Added!", result);
        }

        [TestMethod]
        public void TestDeleteProductDeleted()
        {
            var repo = new ProductRepository();
            var result = repo.DeleteProduct(13);
            Assert.AreEqual("Deleted!", result);
        }
        
        [TestMethod]
        public void TestDeleteProductNotFound()
        {
            var repo = new ProductRepository();
            var result = repo.DeleteProduct(1);
            Assert.AreEqual("No data found", result);
        }

        [TestMethod]
        public void TestGetAllProductsFound()
        {
            var repo = new ProductRepository();
            var result = repo.GetAllProducts(1);
            Assert.IsNotNull(result);
        }
        
        [TestMethod]
        public void TestGetAllProductsNotFound()
        {
            var repo = new ProductRepository();
            var result = repo.GetAllProducts(3);
            Assert.IsTrue(result.Count()==0);
        }

        [TestMethod]
        public void TestGetProductFound()
        {
            var repo = new ProductRepository();
            var result = repo.GetProduct(12);
            Assert.AreEqual("asdadsf                                           ", result.ProductName);
        }
        
        [TestMethod]
        public void TestGetProductNotFound()
        {
            var repo = new ProductRepository();
            var result = repo.GetProduct(1);
            Assert.IsTrue(result.ProductName==null);
        }

        [TestMethod]
        public void TestUpdateProductUpdated()
        {
            var repo = new ProductRepository();
            ProductData product = new ProductData 
            {
                ProductID = 12,
                ProductName = "Test"
            };
            var result = repo.UpdateProduct(product);
            Assert.AreEqual("Updated!", result);
        }
        
        [TestMethod]
        public void TestUpdateProductNotUpdated()
        {
            var repo = new ProductRepository();
            ProductData product = new ProductData 
            {
                ProductID = 1,
                ProductName = "Test"
            };
            var result = repo.UpdateProduct(product);
            Assert.AreEqual("No data found", result);
        }
    }
}
