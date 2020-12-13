using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Source_Control_Final_Assignment.Models
{
    public class Login
    {
        [Required(ErrorMessage ="User Name Is Required.")]
        [DisplayName("User Name")]
        public string uname  { get; set; }
        [Required(ErrorMessage ="Password Is Required.")]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}