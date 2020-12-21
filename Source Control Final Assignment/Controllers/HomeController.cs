using Newtonsoft.Json;
using NLog;
using SCFA.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SCFA.Controllers
{
    public class HomeController : Controller
    {
        //Log into file.
        readonly Logger loggerFile = LogManager.GetCurrentClassLogger();

        //Log into database.
        readonly Logger loggerDB = LogManager.GetLogger("databaseLogger");
        //Database Entity.
        private readonly SCFAEntities db = new SCFAEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //GET: Home/Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        //POST: Home/Login
        public ActionResult Login(LoginData model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var db = new SCFAEntities())
                    {
                        var obj = db.UserDatas.Where(a => a.uname.Equals(model.uname) && a.pwd.Equals(model.pwd)).FirstOrDefault();
                        if (obj != null)
                        {
                            UserData img = db.UserDatas.Find(model.uname);
                            FormsAuthentication.SetAuthCookie(obj.uname.ToString(), false);
                            Session["UserName"] = obj.uname;
                            Session["ImagePath"] = "~/Images/"+obj.uname+".jpg";
                            loggerFile.Info("Login Successffuly. " + obj.uname);
                            loggerDB.Info("Login Successffuly " + obj.uname);
                            return this.RedirectToAction("Profile");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid Email Id or Password");
                            return View("Login");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                loggerFile.Error(ex.ToString());
                loggerDB.Error(ex.ToString());
            }
            return View(model);
        }

        [HttpGet]
        // GET: Home/Register
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        // POST: Home/Register
        public ActionResult Register(UserData user, HttpPostedFileBase image)
        {
            try
            {
                string extension = Path.GetExtension(image.FileName);
                string filename = user.uname + extension;
                user.path = "~/Images/" + filename;
                string smallImagepath = Path.Combine(Server.MapPath("~/Images/"), filename);
          
                db.UserDatas.Add(user);
                if (db.SaveChanges() > 0)
                {
                     FormsAuthentication.SetAuthCookie(user.uname.ToString(), false);
                     image.SaveAs(smallImagepath);
                     loggerFile.Info("Registered Successffuly! " + user.uname);
                     loggerDB.Info("Registered Successffuly! " + user.uname);
                     ModelState.Clear();
                     Session["UserName"] = user.uname;
                     Session["ImagePath"] = user.path;
                     return RedirectToAction("Profile", "Home");
                }
            }
            catch(Exception ex)
            {
                loggerFile.Error(ex.ToString());
                loggerDB.Error(ex.ToString());
            }
            return View(user);
        }

        
        public ActionResult Profile()
        {
            //Finds the user data and view it.
            UserData user = db.UserDatas.Find(Session["UserName"]);
            return View(user);
        }

        [HttpGet]
        //GET: Home/Logout
        public ActionResult Logout()
        {
            //Clear the session
            Session.Clear();
            return View("Index");
        }
        [HttpGet]
        public FileResult Download()
        {
            //Download data in json format
            //Use of Newtonsoft json.
            UserData user = db.UserDatas.Find(Session["UserName"]);
            string JSONResult = JsonConvert.SerializeObject(user);
            string path = @"G:\Company\Source Control Final Assignment\Json\" + Session["UserName"] + ".json";
            using (var tw = new StreamWriter(path, true))
            {
                tw.Write(JSONResult);
                tw.Close();
            }
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = Path.GetFileName(path);
            System.IO.File.Delete(path);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}