using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace IT_project.Models
{
    public class Colors
    {
        public int ID { get; set; }
        public string Color { get; set; }
        [ForeignKey("Clothes")] 
        public int ClothesId { get; set; }
        public virtual Clothes Clothes { get; set; }
        public Colors(string color)
        {
            this.Color = color;
        }

        public Colors()
        {

        }
    }
}