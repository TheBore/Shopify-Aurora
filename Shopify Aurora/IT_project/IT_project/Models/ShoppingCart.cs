using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mime;
using System.Web;

namespace IT_project.Models
{
    
    public class ShoppingCart
    {        
        public int ID { get; set; }
        public string dateCreated { get; set; }
        public virtual ICollection<Clothes> Clothes { get; set; }

        public ShoppingCart()
        {
            Clothes = new List<Clothes>();
           
        }
    }
}