namespace SourceControlAssignment1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data.SqlClient;

    public partial class UserData
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is Required")]
        [StringLength(30)]
        public string fname { get; set; }

        [DisplayName("Middle Name")]
        [Required(ErrorMessage = "Middle Name is Required")]
        [StringLength(30)]
        public string mname { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name is Required")]
        [StringLength(30)]
        public string lname { get; set; }

        [DisplayName("User Name")]
        [Required(ErrorMessage = "User Name is Required")]
        [StringLength(30)]
        public string uname { get; set; }

        [DisplayName("Age")]
        [Range(1, 50, ErrorMessage = "Age must be between 1-50 in years.")]
        public int age { get; set; }

        [DisplayName("Birth Day")]
        [SourceaControlAssignment1.Custom]
        public DateTime bday { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "Address is Required")]
        [MaxLength(50)]
        public string address { get; set; }

        [DisplayName("Contact No.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter Valid Mobile Number.")]
        [Required(ErrorMessage = "Contact No. is Required")]
        public Int64 contact { get; set; }

        [DisplayName("E-mail")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+.[a-z]{2,4}", ErrorMessage = "Incorrect Email")]
        public string email { get; set; }

        static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=I:\\SourceControlAssignment1\App_Data\User Database.mdf;Integrated Security=True;MultipleActiveResultSets=True;Connect Timeout=30;Application Name=EntityFramework";
        SqlConnection con = new SqlConnection(connectionString);

        string query;

        public int SaveDetails()
        {
            //var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            //var config = builder.Build();
            //string constring = config.GetConnectionString("UserData");
            //SqlConnection con = new SqlConnection(constring);
            con.Open();
            Console.Write("Connection Made");
            query = "INSERT INTO UserDatas(fname, mname, lname, uname, age, address, contact, email) values ('"
                + fname + "','" + mname + "','" + lname + "','" + uname + "','" + age + "','" + address + "','" + contact + "','" + email + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            Console.Write("Data Added!");
            return i;
        }

        


    }
}
