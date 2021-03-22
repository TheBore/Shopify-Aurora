using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IT_project.Models.ViewModels
{
    public class AllClothesViewModel
    {
        public List<ClothesViewModel> AllClothes { get; set; }
       
    }
    public class ClothesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public int Price { get; set; }
        public Type? Type { get; set; }
        public bool AvailablePieces { get; set; }
        public String Image { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
        public virtual ICollection<Colors> Colors { get; set; }
        public virtual ICollection<Sizes> Sizes { get; set; }
    }
}