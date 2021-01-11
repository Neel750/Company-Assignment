using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PMS.Model
{
    public class ProductData
    {
        public int ProductID { get; set; }
        [DisplayName("Product Name:")]
        [Required(ErrorMessage = "Product Name Required.")]
        public string ProductName { get; set; }
        public int UserId { get; set; }
        [DisplayName("Category:")]
        [Required(ErrorMessage = "Category Required.")]
        public string Category { get; set; }
        [DisplayName("Description:")]
        [Required(ErrorMessage = "Description Required.")]
        public string SmallDescription { get; set; }
        [DisplayName("Image:")]
        [Required(ErrorMessage = "Small Image Required.")]
        public string SmallImage { get; set; }
        [DisplayName("Detail Description:")]
        [Required(ErrorMessage = "Detail Description Required.")]
        public string LongDescription { get; set; }
        [DisplayName("Detailed Image:")]
        [Required(ErrorMessage = "Detailed Image Required.")]
        public string LongImage { get; set; }
        [DisplayName("Tags:")]
        public string Tags { get; set; }
        [DisplayName("Price:")]
        [Required(ErrorMessage = "Price Required.")]
        public int Price { get; set; }
        [DisplayName("Quantity:")]
        [Required(ErrorMessage = "Quantity Required.")]
        public int Quantity { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public HttpPostedFileBase smallFile { get; set; }
        public HttpPostedFileBase largeFile { get; set; }
    }
}
