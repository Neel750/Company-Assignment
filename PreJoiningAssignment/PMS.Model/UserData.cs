using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{
    public class UserData
    {
        public int Id { get; set; }
        [DisplayName("Name:")]
        [Required(ErrorMessage = "Name Required.")]
        public string Name { get; set; }
        [DisplayName("Address:")]
        [Required(ErrorMessage = "Address Required.")]
        public string Address { get; set; }
        [DisplayName("Contact:")]
        [Required(ErrorMessage = "Contact Required.")]
        [DataType(DataType.PhoneNumber)]
        public int Contact { get; set; }
        [DisplayName("Email:")]
        [Required(ErrorMessage = "Email Required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DisplayName("Password:")]
        [Required(ErrorMessage = "Password Required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
