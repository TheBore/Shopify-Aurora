using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IT_project.Models
{
    public class Sizes
    {
        public int ID { get; set; }
        public string Size { get; set; }
        [ForeignKey("Clothes")]
        public int ClothesId { get; set; }
        public virtual Clothes Clothes { get; set; }

        public Sizes()
        {

        }

        public Sizes(string size)
        {
            Size = size;
        }
    }
}