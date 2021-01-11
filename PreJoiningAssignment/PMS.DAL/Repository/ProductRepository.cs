using AutoMapper;
using NLog;
using PMS.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PMS.DAL.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly Database.PMSEntities _dbContext;
        private readonly Logger loggerFile;
        private readonly Logger loggerDB;
        public ProductRepository()
        {
            _dbContext = new Database.PMSEntities();
            loggerFile = LogManager.GetCurrentClassLogger();//Log into File
            loggerDB = LogManager.GetLogger("databaseLogger");//Log into database
        }
        //Add Product. Accept ProductData Type As Argument Return String Message
        public string CreateProduct(ProductData model)
        {
            try
            {
                if (model != null)//Check Model Is Empty Or Not
                {
                    var entity = _dbContext.UserDatas.Find(model.UserId);//Find User
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductData, Database.ProductData>());
                    var Mapper = new Mapper(config);
                    Database.ProductData product = Mapper.Map<Database.ProductData>(model);//Map ProductData type to Database.ProductData
                    product.CreatedDate = DateTime.Now;//Created Date
                    entity.ProductDatas.Add(product);//Save into database
                    _dbContext.SaveChanges();                   
                    return "Successfully Added!";//Return Success Message
                }
                return "Model Is Null!";//Return If Model Is Empty
            }
            catch (Exception ex)
            {
                loggerFile.Error("Error While Creating Product Error = " + ex.Message);//Log error into file.
                loggerDB.Error("Error While Creating Product Error = " + ex.Message);//Log error into database.
                return ex.Message;//return exception message
            }
        }
        //Delete Product. Accept id as argument Return string message
        public string DeleteProduct(int id)
        {
            try
            {
                var entity = _dbContext.ProductDatas.Find(id);//Find Product
                if (entity != null)//If product finds.
                {
                    //entity.SmallImage = Path.Combine("H:/GateWay/PreJoiningAssignment/PreJoiningAssignment/PreJoiningAssignment/", entity.SmallImage);//combine path
                    //entity.LongImage = Path.Combine("H:/GateWay/PreJoiningAssignment/PreJoiningAssignment/PreJoiningAssignment/", entity.LongImage);//combine path
                    //System.IO.File.Delete(entity.SmallImage);//delete file
                    //System.IO.File.Delete(entity.LongImage);//delete file
                    _dbContext.ProductDatas.Remove(entity);//Remove From Database
                    _dbContext.SaveChanges();//Save database
                    return "Deleted!";//Return Message
                }
                return "No data found";//Return Not found message
            }
            catch (Exception ex)
            {
                loggerFile.Error("Error While Deleting Product Error = " + ex.Message);//Log Error Into File
                loggerDB.Error("Error While Deleting Product Error = " + ex.Message);//Log Error Into Database
                return ex.Message;//Return error message
            }
        }
        //List Out Products Of That User. Accept id as argument Return List of products
        public List<ProductData> GetAllProducts(int id)
        {
            var entity = _dbContext.UserDatas.Find(id);//Find Products Of Particular User

            List<ProductData> productList = new List<ProductData>();//Create Empty List

            if (entity != null)//If products found
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Database.ProductData, ProductData>());
                var Mapper = new Mapper(config);
                productList = (List<ProductData>)Mapper.Map<List<ProductData>>(entity.ProductDatas);//Map Database.ProductData type to ProductData type. 
            }
            return productList;//Return Product List
        }
        //Return Specific Product Accept id as argument
        public ProductData GetProduct(int id)
        {
            var entity = _dbContext.ProductDatas.Find(id);//Find Product
            ProductData product = new ProductData();//Create empty product
            if (entity != null)//if product found
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Database.ProductData, ProductData>());
                var Mapper = new Mapper(config);
                product = Mapper.Map<ProductData>(entity);//Map Databse.ProductData to ProductData Type
            }
            return product;//return product
        }
        //Update Product. Accept ProductData type as argument and return string message
        public string UpdateProduct(ProductData model)
        {
            try
            {
                var entity = _dbContext.ProductDatas.Find(model.ProductID);//Find existing product
                if (entity != null)//Product Founds
                {
                    if (model.ProductName != null)//ProductName change
                    {
                        entity.ProductName = model.ProductName;
                    }
                    if (model.Category != null)//Category Change
                    {
                        entity.Category = model.Category;
                    }
                    if (model.SmallDescription != null)//Small Description Change
                    {
                        entity.SmallDescription = model.SmallDescription;
                    }
                    if(model.SmallImage != null)//Small Image Change
                    {
                        entity.SmallImage = model.SmallImage;
                    }
                    if (model.LongDescription != null)//Long Description Change
                    {
                        entity.LongDescription = model.LongDescription;
                    }
                    if(model.LongImage != null)//Long Image Change
                    {
                        entity.LongImage = model.LongImage;
                    }
                    if (model.Tags != null)//Tags Change
                    {
                        entity.Tags = model.Tags;
                    }
                    if (model.Price != -1)//Price Change
                    {
                        entity.Price = model.Price;
                    }
                    if (model.Quantity != 0)//Quantity Change
                    {
                        entity.Quantity = model.Quantity;
                    }
                    entity.UpdatedDate = DateTime.Now;//Updated Time
                    _dbContext.SaveChanges();//Save to database
                    return "Updated!";//Return Success Message
                }
                return "No data found";//Return product not find
            }
            catch (Exception ex)
            {
                loggerFile.Error("Error While Updating Product Error = " + ex.Message);//Log error into file.
                loggerDB.Error("Error While Updating Product Error = " + ex.Message);//Log error into database.
                return ex.Message;//Return error message
            }
        }
    }
}
