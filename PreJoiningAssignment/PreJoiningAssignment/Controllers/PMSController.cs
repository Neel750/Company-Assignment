using PMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Newtonsoft.Json;
using PagedList;
using NLog;

namespace PMS.MVC.Controllers
{
    public class PMSController : Controller
    {

        private readonly HttpClient _client;
        readonly Logger loggerFile;
        readonly Logger loggerDB;
        public PMSController()
        {
            _client = new HttpClient();
            loggerFile = LogManager.GetCurrentClassLogger();
            loggerDB = LogManager.GetLogger("databaseLogger");
        }

        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        } 

        // POST: Login
        [HttpPost]
        public ActionResult Login(UserData model)
        {
            var task = _client.PostAsJsonAsync<UserData>("https://localhost:44379/api/User/PostAuthenticateUser/", model);
            task.Wait();

            var result = task.Result.Content.ReadAsAsync<List<String>>();
            result.Wait();

            var check = result.Result;
            if (check != null)
            {
                HttpContext.Session.Add("userName", check[0]);
                HttpContext.Session.Add("id", check[1]);
                loggerFile.Info(check[0] + " Logged In.");
                loggerDB.Info(check[1] + " Logged In.");
                return RedirectToAction("Index");
            }
            else
            {
                TempData["LoginMessage"] = "Email Or Password Is Wrong!";
            }
            return View();
        }

        // GET: CreateUser
        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }
        
        // POST: CreateUser
        [HttpPost]
        public ActionResult CreateUser(UserData model)
        {
            var task = _client.PostAsJsonAsync<UserData>("https://localhost:44379/api/User/PostCreateUser", model);
            task.Wait();

            var result = task.Result.Content.ReadAsAsync<List<string>>();
            result.Wait();

            var check = result.Result;
            if(check.Count == 0)
            {
                TempData["Error"] = check;
            }
            else
            {
                HttpContext.Session.Add("userName", check[0]);
                HttpContext.Session.Add("id", check[1]);
                loggerFile.Info(check[0] + " Created And Logged In.");
                loggerDB.Info(check[1] + " Created And Logged In.");
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Index or Dashboard
        [HttpGet]
        public ActionResult Index()
        {
            if (HttpContext.Session["userName"] == null && HttpContext.Session["id"] == null)
            {
                TempData["LoginMessage"] = "Please Login First!";
                return View("Login");
            }
            return View();
        }

        // GET: AddProduct
        [HttpGet]
        public ActionResult AddProduct()
        {
            if (HttpContext.Session["userName"] == null && HttpContext.Session["id"] == null)
            {
                TempData["LoginMessage"] = "Please Login First!";
                return View("Login");
            }
            return View();
        }

        // POST: AddProduct
        [HttpPost]
        public ActionResult AddProduct(ProductData model,HttpPostedFileBase smallFile, HttpPostedFileBase largeFile)
        {
            model.UserId = Convert.ToInt32(HttpContext.Session[1]);
            string smallImageExtension = Path.GetExtension(smallFile.FileName);//Get extension of small image file
            string smallImageFilename = model.UserId + model.ProductName + smallImageExtension;//rename file as userId+productName.extension
            model.SmallImage = "~/Images/SmallImages/" + smallImageFilename;//store path in database
            string smallImagepath = Path.Combine(Server.MapPath("~/Images/SmallImages/"), smallImageFilename);//get whole path for saving file
            string largeImageExtension = Path.GetExtension(largeFile.FileName);//Get extension of large image file
            string largeImageFilename = model.UserId + model.ProductName + largeImageExtension;//rename file as userId+productName.extension
            model.LongImage = "~/Images/LargeImages" + smallImageFilename;//store path in database
            string largeImagepath = Path.Combine(Server.MapPath("~/Images/LargeImages/"), largeImageFilename);//get whole path for saving file
            //making files to null to store data in database 
            model.smallFile = null;
            model.largeFile = null;
            var task = _client.PostAsJsonAsync<ProductData>("https://localhost:44379/api/Product/PostCreateProduct", model);
            task.Wait();

            var result = task.Result.Content.ReadAsStringAsync();
            result.Wait();
            if(result.Result.Contains("Successfully Added!"))
            {
                smallFile.SaveAs(smallImagepath);
                largeFile.SaveAs(largeImagepath);
                loggerFile.Info(model.ProductName + " Added.");
                loggerDB.Info(model.ProductName + " Added.");
                return RedirectToAction("Index");
            }
            return View();
        }
       
        // GET: Specific Product By Id
        public ActionResult ProductById(int id)
        {
            if (HttpContext.Session["userName"] == null && HttpContext.Session["id"] == null)
            {
                TempData["LoginMessage"] = "Please Login First!";
                return View("Login");
            }
            ProductData product = null;//For storing product data
            var task = _client.GetAsync("https://localhost:44379/api/Product/GetProduct?id=" + id);
            task.Wait();
            var result = task.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<ProductData>(readTask);

            }
            return View(product);//returns product by id page with product data
        }

        // GET: Update Product
        public ActionResult UpdateProduct(int id)
        {
            if (HttpContext.Session["userName"] == null && HttpContext.Session["id"] == null)
            {
                TempData["LoginMessage"] = "Please Login First!";
                return View("Login");
            }
            ProductData product = null;//For storing product data
            var task = _client.GetAsync("https://localhost:44379/api/Product/GetProduct?id=" + id);
            task.Wait();
            var result = task.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<ProductData>(readTask);
            }
            return View(product);
        }

        // PUT: Update Product
        [HttpPost]
        public ActionResult UpdateProduct(ProductData model, HttpPostedFileBase smallFile = null, HttpPostedFileBase largeFile = null)
        {
            if (HttpContext.Session["userName"] == null && HttpContext.Session["id"] == null)
            {
                TempData["LoginMessage"] = "Please Login First!";
                return View("Login");
            }
            model.UserId = Convert.ToInt32(HttpContext.Session["id"]);//stroes user id
            ProductData product = null;//For storing product data
            string path;//For storing existing file path
            string smallImageExtension;
            string smallImageFilename;
            string smallImagepath;
            string largeImageExtension;
            string largeImageFilename;
            string largeImagepath;
            var responseTask = _client.GetAsync("https://localhost:44379/api/Product/GetProduct?id=" + model.ProductID);//Get existing data
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<ProductData>(readTask);//store it in product variable
            }
            if (smallFile != null)//if user wants to change small image file
            {
                path = Path.Combine("~/Images/SmallImages/", product.SmallImage);//combine existing path for deleting existing file
                System.IO.File.Delete(path);//delete existing file
                smallImageExtension = Path.GetExtension(smallFile.FileName);//get new extension
                smallImageFilename = model.UserId + model.ProductName + smallImageExtension;//rename file
                model.SmallImage = "~/Images/SmallImages/" + smallImageFilename;//database path
                smallImagepath = Path.Combine(Server.MapPath("~/Images/SmallImages/"), smallImageFilename);//combine path
                smallFile.SaveAs(smallImagepath);//save file
            }
            if (largeFile != null)//if user wants to change large image file
            {
                path = Path.Combine("~/Images/LargeImages/", product.LongImage);//combine existing path for deleting existing file
                System.IO.File.Delete(path);//delete existing file
                largeImageExtension = Path.GetExtension(largeFile.FileName);//get new extension
                largeImageFilename = model.UserId + model.ProductName + largeImageExtension;//rename file
                model.LongImage = "~/Images/LargeImages/" + largeImageFilename;//database path
                largeImagepath = Path.Combine(Server.MapPath("~/Images/LargeImages/"), largeImageFilename);//combine path
                largeFile.SaveAs(largeImagepath);//save file
            }
            //making files to null to store data in database 
            model.smallFile = null;
            model.largeFile = null;
            var task = _client.PutAsJsonAsync<ProductData>("https://localhost:44379/api/Product/PutUpdateProduct", model);//Put Method send Json Type
            task.Wait();
            var taskResult = task.Result.Content.ReadAsStringAsync();
            taskResult.Wait();
            if (taskResult.Result.Contains("Updated!"))
            {
                loggerFile.Info(model.ProductName + " Updated!");
                loggerDB.Info(model.ProductName + " Updated!");
                //return pop up message edited product name and redirect to Products.
                return Content("<script language='javascript' type='text/javascript'>alert('" + product.ProductName + " Edited Successfully!');window.location.href = '/PMS/Products';</script>");
                //return View("Index");
            }
            return View();
        }

        // GET: Products List
        public ActionResult Products(int? page, string Sorting_Order, string searchBy, string search)
        {
            if (HttpContext.Session["userName"] == null && HttpContext.Session["id"] == null)
            {
                TempData["LoginMessage"] = "Please Login First!";
                return View("Login");
            }
            IEnumerable<ProductData> products = null;//For storing retrived data
            ViewBag.CurrentSortOrder = Sorting_Order;//Sorting order
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Name_Description" : "";//Sort By Name
            ViewBag.SortingPrice = Sorting_Order == "Price_Low_To_High" ? "Price_High_To_Low" : "Price_Low_To_High";//Sort By Price

            var responseTask = _client.GetAsync("https://localhost:44379/api/Product/GetAllProducts?id=" + HttpContext.Session["id"] + "&Sorting_Order=" + Sorting_Order + "&searchBy=" + searchBy + "&search=" + search);
            responseTask.Wait();

            var result = responseTask.Result;
            var readTask = result.Content.ReadAsAsync<IList<ProductData>>();
            readTask.Wait();

            products = readTask.Result;
            if (products.Count()!=0)
            {
                return View(products.ToList().ToPagedList(page ?? 1, 6));
            }
            else
            {
                TempData["EmptyProduct"] = "Please Add Products First!";
                return View("Index");
            }
        }

        // Delete Specific Product
        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            //Checks for user log in or not
            if (HttpContext.Session["userName"] == null && HttpContext.Session["id"] == null)
            {
                TempData["LoginMessage"] = "Please Login First!";
                return View("Login");
            }
            var task = _client.DeleteAsync("https://localhost:44379/api/Product/DeleteProduct?id=" + id);//Delete method api
            task.Wait();
            var taskResult = task.Result.Content.ReadAsStringAsync();
            taskResult.Wait();
            if (taskResult.Result.Contains("Deleted!"))
            {
                loggerFile.Info(id + " Deleted!");
                loggerDB.Info(id + " Deleted!");
                //returnpop up message box and redirect to products
                return Content("<script language='javascript' type='text/javascript'>alert('" + id + " Deleted Successfully!');window.location.href = '/PMS/Products';</script>");//return view with product data
            }
            else
            {
                //return pop up message something went wrong.
                 return Content("<script language='javascript' type='text/javascript'>alert('Something Went Wrong!');window.location.href = '/PMS/Product';</script>");
            }
        }

        // GET: Multiple Delete
        [HttpGet]
        public ActionResult DeleteMultipleProducts()
        {
            //Checks for user log in or not
            if (HttpContext.Session["userName"] == null && HttpContext.Session["id"] == null)
            {
                TempData["LoginMessage"] = "Please Login First!";
                return View("Login");
            }
            IEnumerable<ProductData> products = null;

            var task = _client.GetAsync("https://localhost:44379/api/Product/GetAllProducts?id=" + HttpContext.Session["id"]);//Gets Product Datas
            task.Wait();

            var result = task.Result;
            var readTask = result.Content.ReadAsAsync<IEnumerable<ProductData>>();
            readTask.Wait();
            products = readTask.Result;
            if (products.Count() != 0)
            {
                return View(products);//return Deletemultiple products with products
            }
            else
            {
                TempData["EmptyProduct"] = "Please Add Products First!";
                return View("Index");
            }
        }
        //Delete Multiple Products Post Method
        [HttpPost]
        public ActionResult DeleteMultipleProducts(FormCollection form)
        {
            string[] ids = form["ProductIds"].Split(new char[] { ',' });//stores ids that will be deleted
            string deleteId = JsonConvert.SerializeObject(ids);
            //var task = _client.DeleteAsync("https://localhost:44379/api/Product/DeleteMultipleProducts?id=" + deleteId);
            //task.Wait();
            //var taskResult = task.Result.Content.ReadAsStringAsync();
            //taskResult.Wait();
            foreach (var id in ids)
            {
                var task = _client.DeleteAsync("https://localhost:44379/api/Product/DeleteProduct?id=" + Convert.ToInt32(id));//Delete method api
                task.Wait();
                var taskResult = task.Result.Content.ReadAsStringAsync();
                taskResult.Wait();
                if (!taskResult.Result.Contains("Deleted!"))
                {
                    //returnpop up message box and redirect to products
                    return Content("<script language='javascript' type='text/javascript'>alert('Something Went Wrong When Deleting Product:"+id+"');window.location.href = '/PMS/Products';</script>");
                }
                loggerFile.Info(id + " Deleted!");
                loggerDB.Info(id + " Updated!");

            }
            //returnpop up message box and redirect to products
            return Content("<script language='javascript' type='text/javascript'>alert('Deleted Successfully!');window.location.href = '/PMS/Products';</script>");//return view with product data
        }
         
        //Logout Get Method
        [HttpGet]
        public ActionResult Logout()
        {
            loggerFile.Info(Session["UserName"] + " Logged out at" + DateTime.Now.ToString());//log in file
            loggerDB.Info(Session["UserName"] + " Logged out at" + DateTime.Now.ToString());//log in db
            HttpContext.Session.Clear();//session clear
            //return pop up message and redirect to login
            return Content("<script language='javascript' type='text/javascript'>alert('Log Out Successfully!');window.location.href = '/PMS/Login';</script>");
        }
    }
}
