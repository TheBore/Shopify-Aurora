using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IT_project.Models
{
    public enum Type
    {
        Male, Female
    }
    public class Clothes
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public Type? Type { get; set; }
        [Required]
        public string Image { get; set; }
        public bool AvailablePieces { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
        public virtual ICollection<Colors> Colors { get; set; }
        public virtual ICollection<Sizes> Sizes { get; set; }
        

        public Clothes()
        {
            Colors = new List<Colors>();
            Sizes = new List<Sizes>();
            ShoppingCarts = new List<ShoppingCart>();
        }

        public static implicit operator Clothes(List<Clothes> v)
        {
            throw new NotImplementedException();
        }
    }
}