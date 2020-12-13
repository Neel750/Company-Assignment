using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Source_Control_Final_Assignment.Models
{
    public class UserData
    {
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
        [Range(20, 50, ErrorMessage = "Age must be between 20-50 in years.")]
        public int age { get; set; }

        [DisplayName("Birth Day")]
        [Source_Control_Final_Assignment.Custom]
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
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required]
        public string password { get; set; }
        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [Required]
        [Compare("Password",ErrorMessage = "Password and confirm password must be same.")]
        public string confirm_password { get; set; }


        public string path { get; set; }
        [DisplayName("Upload Your Photo.")]
        [Required(ErrorMessage = "Upload your image.")]
        [FileExtensions(Extensions ="jpg",ErrorMessage ="Error")]
        public HttpPostedFileBase image { get; set; }
    }
}