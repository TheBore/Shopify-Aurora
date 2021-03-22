using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IT_project.Models
{
    public class Price
    {
        public int ID { get; set; }
        public int ClothesID { get; set; }
        public int price { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }
        public virtual Clothes Clothes { get; set; }
    }
}