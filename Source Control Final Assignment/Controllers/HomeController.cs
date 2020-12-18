using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Source_Control_Final_Assignment.Models;
using System.IO;
using NLog;
using Newtonsoft.Json;

namespace Source_Control_Final_Assignment.Controllers
{
    public class HomeController : Controller
    {
        //Log into file.
        Logger loggerFile = LogManager.GetCurrentClassLogger();
        //Log into database.
        Logger loggerDB = LogManager.GetLogger("databaseLogger");
        //Connection String To connect Database.
        String connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Source Control Final Assignment;Integrated Security=True";
        //Sql connection.
        SqlConnection con;
        //Sqlcommand for Sql query executer 
        SqlCommand cmd;
        //Sql data reader for fetch data from database.
        SqlDataReader dr;
        //Session variable.
        static string username = null;
        //Home/
        public ActionResult Index()
        {
            return View();
        }
        //Home/Login
        public ActionResult Login()
        {
            return View();
        }
        //Home/Verify
        //Authenticate user method.
        [HttpPost]
        [Obsolete]
        public ActionResult Verify(Login data)
        {
            try
            {
                //Validates Model
                if (ModelState.IsValid)
                {
                    //Connection Makeing Statement
                    con = new SqlConnection(connectionString);
                    cmd = new SqlCommand();
                    //Connection Opened.
                    con.Open();
                    cmd.Connection = con;
                    //Sql query for verifing user. 
                    cmd.CommandText = "SELECT * FROM Authentication WHERE username='" + data.uname + "'AND pwd='" + data.password + "'";
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        //Session Started
                        username = data.uname;
                        //Connection close.
                        con.Close();
                        //Fetch Data of user.
                        FetchData();
                        //Redirect to profile page.
                        return View("Profile");
                    }
                    else
                    {
                        //username or password is wrong then move to error page.
                        con.Close();
                        return View("Error");
                    }
                }
                else
                {
                    //If model is not validated then redirect to Login page.
                    return View("Login");
                }
            }
            catch(Exception ex)
            {
                //Log into file
                loggerFile.ErrorException("Error occured in Verify action" + ex.StackTrace, ex);
                //Log into database
                loggerDB.Error(ex, "Error occured in Verify action");
                return View("Login");
            }
        }

        //Home/Profile
        public new ActionResult Profile()
        {
            //Check for session started or not
            if(username == null)
            {
                return View("Login");
            }
            else
            {
                return View();
            }
        }

        [Obsolete]
        public void FetchData()
        {
            try
            {
                //throw new Exception("Exception Thrown From Fetch Data.");
                con = new SqlConnection(connectionString);
                cmd = new SqlCommand();
                //Open Connection for UserData table.
                con.Open();
                cmd.Connection = con;
                //Sql query that fetch user's data.
                cmd.CommandText = "Select * from UserData where uname='" + username + "'";
                dr = cmd.ExecuteReader();
                dr.Read();
                //Store data in ViewData.
                ViewData["fname"] = dr["fname"].ToString();
                ViewData["mname"] = dr["mname"].ToString();
                ViewData["lname"] = dr["lname"].ToString();
                ViewData["uname"] = dr["uname"].ToString();
                ViewData["age"] = (Int16)dr["age"];//Convert into small int.
                ViewData["bday"] = DateTime.Parse(dr["bday"].ToString()).ToShortDateString();//convert into DateTime
                ViewData["address"]= dr["address"].ToString();
                ViewData["contact"] = (Int64)dr["contact"];//Convert into long int.
                ViewData["email"] = dr["email"].ToString();
                ViewBag.json = JsonConvert.SerializeObject(ViewData.ToList());//newtonjson
                //Close Connection.
                con.Close();
                //Open Connection For Image Table.
                con.Open();
                cmd.Connection = con;
                //Sql query for image path
                cmd.CommandText = "select path from Image where uname = '" + username + "'";
                dr = cmd.ExecuteReader();
                dr.Read();
                ViewData["path"] = dr["path"].ToString();
                //Close Connection
                con.Close();
            }catch(Exception ex)
            {
                //Log into File.
                loggerFile.ErrorException("Error occured in FetchData method."+ex.StackTrace, ex);
                //Log into database
                loggerDB.Error(ex, "Error occured in FetchData method.");         
            }
        }

        //Get: Home/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Home/Add
        [HttpPost]
        [Obsolete]
        public ActionResult Add(FormCollection collection)
        {
            // TODO: Add insert logic here
            try
            {
                //Check model is valid or not.
                if (ModelState.IsValid)
                {
                    //Fetch data to store into database
                    UserData umodel = new UserData();
                    umodel.fname = HttpContext.Request.Form["fname"].ToString();
                    umodel.mname = HttpContext.Request.Form["mname"].ToString();
                    umodel.lname = HttpContext.Request.Form["lname"].ToString();
                    umodel.uname = HttpContext.Request.Form["uname"].ToString();
                    umodel.age = Convert.ToInt32(HttpContext.Request.Form["age"]);
                    umodel.bday = DateTime.Parse(HttpContext.Request.Form["bday"]);
                    umodel.address = HttpContext.Request.Form["address"].ToString();
                    umodel.contact = Convert.ToInt64(HttpContext.Request.Form["contact"]);
                    umodel.email = HttpContext.Request.Form["email"].ToString();
                    umodel.password = HttpContext.Request.Form["password"].ToString();
                    umodel.image = Request.Files["image"];

                    con = new SqlConnection(connectionString);
                    //Connection open.
                    con.Open();
                    //Sql string for Authentication table.
                    String query = "INSERT INTO Authentication (username, pwd) values ('" + umodel.uname + "','" + umodel.password + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    int j = cmd.ExecuteNonQuery();

                    //Sql string for UserData table.
                    query = "INSERT INTO UserData(fname, mname, lname, uname, age, bday, address, contact, email) values ('"
                        + umodel.fname + "','" + umodel.mname + "','" + umodel.lname + "','" + umodel.uname + "','" + umodel.age + "','" + umodel.bday + "','" + umodel.address + "','" + umodel.contact + "','" + umodel.email + "')";
                    cmd = new SqlCommand(query, con);
                    int i = cmd.ExecuteNonQuery();

                    //Extension of image file
                    string extension = Path.GetExtension(umodel.image.FileName);
                    //new image file name
                    string filename = umodel.uname + extension;
                    //path saved in image table.
                    umodel.path = "~/Upload/" + filename;
                    //Sql query for Image table.
                    query = "INSERT INTO Image (uname, path) values ('" + umodel.uname + "','" + umodel.path + "')";
                    cmd = new SqlCommand(query, con);
                    int k = cmd.ExecuteNonQuery();

                    //Database connection close.
                    con.Close();
                    //Checks all data are inserted in all table.
                    if (i > 0 && j > 0 && k > 0)
                    {
                        //Whole path for saving image file.
                        filename = Path.Combine(Server.MapPath("~/Upload/"), filename);
                        //save image.
                        umodel.image.SaveAs(filename);
                        //Session starts.
                        username = umodel.uname;
                        //Data fetched from table
                        FetchData();
                        return View("Profile");
                    }
                    else
                    {
                        return RedirectToAction("Error");
                    }
                }
                else
                {
                    return View("Register");
                }
            }
            catch(Exception ex)
            {
                //log into file.
                loggerFile.ErrorException("Error occured in Add method."+ex.StackTrace, ex);
                //log into database.
                loggerDB.Error(ex, "Error occured in Add method.");
                return View("Register");
            }
        }
        //Home/Logout
        [HttpGet]
        public ActionResult Logout()
        {
            //session ended.
            username = null;
            return View("Index");
        }
    }
}