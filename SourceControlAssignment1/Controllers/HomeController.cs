using SourceaControlAssignment1.Context;
using SourceControlAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SourceControlAssignment1.Controllers
{
    public class HomeController : Controller
    {
        List<UserData> data= new List<UserData>();
        UserDataContext db = new UserDataContext();
        // GET: Home
        public ActionResult Index()
        {
            FetchData();
            return View(data);
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        //Display Data In Index Page
        private void FetchData()
        {
            if(data.Count > 0)
            {
                data.Clear();
            }
            try
            {
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=I:\\SourceControlAssignment1\App_Data\User Database.mdf;Integrated Security=True;MultipleActiveResultSets=True;Connect Timeout=30;Application Name=EntityFramework";
                SqlConnection con = new SqlConnection(connectionString);

                string query;
                con.Open();
                query = "SELECT * FROM UserDatas";
                SqlCommand cmd = new SqlCommand();
                SqlDataReader dr;
                cmd.Connection = con;
                cmd.CommandText = query;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(new UserData()
                    {
                        Id = (int)dr["Id"],
                        fname = dr["fname"].ToString(),
                        mname = dr["mname"].ToString(),
                        lname = dr["lname"].ToString(),
                        uname = dr["uname"].ToString(),
                        age = (int)dr["age"],
                        address = dr["address"].ToString(),
                        contact = (Int64)dr["contact"],
                        email = dr["email"].ToString()
                    });
                }
                con.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                UserData umodel = new UserData();
                umodel.fname = HttpContext.Request.Form["fname"].ToString();
                umodel.mname = HttpContext.Request.Form["mname"].ToString();
                umodel.lname = HttpContext.Request.Form["lname"].ToString();
                umodel.uname = HttpContext.Request.Form["uname"].ToString();
                umodel.age = Convert.ToInt32(HttpContext.Request.Form["age"]);
                umodel.address = HttpContext.Request.Form["address"].ToString();
                umodel.contact = (Int64)Convert.ToInt64(HttpContext.Request.Form["contact"]);
                umodel.email = HttpContext.Request.Form["email"].ToString();
                int result = umodel.SaveDetails();
                if (result > 0)
                {
                    return RedirectToAction("Thank_You");
                }
                else
                {
                    return RedirectToAction("Index");
                }
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Thank_You
        public ActionResult Thank_You()
        {
            return View();
        }
    }
}