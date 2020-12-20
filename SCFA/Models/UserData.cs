//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SCFA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Web;

    public partial class UserData
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

        [DisplayName("Contact No.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter Valid Mobile Number.")]
        [Required(ErrorMessage = "Contact No. is Required")]
        public long contact { get; set; }

        [DisplayName("Birth Day")]
        [SCFA.Models.BDayCheck]
        public Nullable<System.DateTime> bday { get; set; }

        [DisplayName("Age")]
        [Range(20, 50, ErrorMessage = "Age must be between 20-50 in years.")]
        public Nullable<short> age { get; set; }

        [Required]
        [DisplayName("E-mail")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+.[a-z]{2,4}", ErrorMessage = "Incorrect Email")]
        public string email { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "Address is Required")]
        [MaxLength(50)]
        public string address { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage ="Password Is Required")]
        [DataType(DataType.Password)]
        public string pwd { get; set; }

        [DisplayName("Image")]
        [Required(ErrorMessage = "Upload your image.")]
        public HttpPostedFileBase image { get; set; }

        [DisplayName("Image")]
        [Required(ErrorMessage = "Upload your image.")]
        [FileExtensions(Extensions = "jpg", ErrorMessage = "Please Upload Only jpg type images only.")]
        public string path{
            get
            {
                if(image!=null)
                    return "~/Images/" + uname+Path.GetExtension(image.FileName).ToString();
                return "";
            }
            set 
            {
                
            }
        }
    }

    public partial class LoginData
    {
        [DisplayName("User Name")]
        [Required(ErrorMessage = "User Name is Required")]
        [StringLength(30)]
        public string uname { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string pwd { get; set; }
    }
}
