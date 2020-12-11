using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Source_Control_Final_Assignment.Models;
using System.IO;

namespace Source_Control_Final_Assignment.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        static string username = null;
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
        //Verify Data Username and Password
        [HttpPost]
        public ActionResult Verify(Login data)
        {
            String connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Source Control Final Assignment;Integrated Security=True";
            con = new SqlConnection(connectionString);
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM Authentication WHERE username='" + data.uname + "'AND pwd='" + data.password + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                username = data.uname;
                con.Close();
                FetchData();
                return View("Profile");
            }
            else
            {
                con.Close();
                return View("Error");
            }
        }

        //Home/Profile
        public new ActionResult Profile()
        {
            if(username == null)
            {
                return View("Login");
            }
            else
            {
                return View();
            }
        }

        public void FetchData()
        {
            try
            {
                String connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Source Control Final Assignment;Integrated Security=True";
                con = new SqlConnection(connectionString);
                cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "Select * from UserData where uname='" + username + "'";
                dr = cmd.ExecuteReader();
                dr.Read();
                ViewData["fname"] = dr["fname"].ToString();
                ViewData["mname"] = dr["mname"].ToString();
                ViewData["lname"] = dr["lname"].ToString();
                ViewData["uname"] = dr["uname"].ToString();
                ViewData["age"] = (int)dr["age"];
                ViewData["bday"] = DateTime.Parse(dr["bday"].ToString()).ToShortDateString();
                ViewData["address"]= dr["address"].ToString();
                ViewData["contact"] = (Int64)dr["contact"];
                ViewData["email"] = dr["email"].ToString();
                con.Close();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "select path from Image where uname = '" + username + "'";
                dr = cmd.ExecuteReader();
                dr.Read();
                ViewData["path"] = dr["path"].ToString();
                con.Close();
            }catch(Exception ex)
            {
                ex.ToString();
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult add(FormCollection collection)
        {
            // TODO: Add insert logic here
            UserData umodel = new UserData();
            umodel.fname = HttpContext.Request.Form["fname"].ToString();
            umodel.mname = HttpContext.Request.Form["mname"].ToString();
            umodel.lname = HttpContext.Request.Form["lname"].ToString();
            umodel.uname = HttpContext.Request.Form["uname"].ToString();
            umodel.age = Convert.ToInt32(HttpContext.Request.Form["age"]);
            umodel.bday = DateTime.Parse(HttpContext.Request.Form["bday"]);
            umodel.address = HttpContext.Request.Form["address"].ToString();
            umodel.contact = (Int64)Convert.ToInt64(HttpContext.Request.Form["contact"]);
            umodel.email = HttpContext.Request.Form["email"].ToString();
            umodel.password = HttpContext.Request.Form["password"].ToString();
            umodel.image = Request.Files["image"];

            string extension = ".jpg";
            string filename = umodel.uname + extension;
            umodel.path = "~/Upload/" + filename;
            filename = Path.Combine(Server.MapPath("~/Upload/"), filename);
            umodel.image.SaveAs(filename);
            String connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Source Control Final Assignment;Integrated Security=True";
            con = new SqlConnection(connectionString);
            con.Open();
            String query = "INSERT INTO Authentication (username, pwd) values ('" + umodel.uname + "','" + umodel.password + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            int j = cmd.ExecuteNonQuery();
            query = "INSERT INTO UserData(fname, mname, lname, uname, age, bday, address, contact, email) values ('"
                + umodel.fname + "','" + umodel.mname + "','" + umodel.lname + "','" + umodel.uname + "','" + umodel.age +"','" + umodel.bday + "','" + umodel.address + "','" + umodel.contact + "','" + umodel.email + "')";
            cmd = new SqlCommand(query, con);
            int i = cmd.ExecuteNonQuery();
            query = "INSERT INTO Image (uname, path) values ('" + umodel.uname + "','" + umodel.path + "')";
            cmd = new SqlCommand(query, con);
            int k = cmd.ExecuteNonQuery();
            username = umodel.uname;
            con.Close();
            if (i > 0 && j > 0 && k > 0)
            {
                FetchData();
                return View("Profile");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult Logout()
        {
            username = null;
            return View("Index");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}