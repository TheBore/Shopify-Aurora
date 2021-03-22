using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IT_project.Models
{
    public class AddToCart
    {
        public string Email { get; set; }
        public string UserId { get; set; }
        public int ShoppingCartId { get; set; }
        public List<Clothes> Clothes { get; set; }
       public AddToCart()
        {
            Clothes = new List<Clothes>();
        }
    }
}