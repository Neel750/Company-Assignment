using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public short IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
