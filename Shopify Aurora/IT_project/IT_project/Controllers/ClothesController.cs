using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI;
using System.Windows.Forms;
using IT_project.Models;
using IT_project.Models.ViewModels;
using IT_project.Helper;

namespace IT_project.Controllers
{
    
    public class Grouping<TKey, TElement> : List<TElement>, IGrouping<TKey, TElement>
    {
        public Grouping(TKey key) : base() => Key = key;
        public Grouping(TKey key, int capacity) : base(capacity) => Key = key;
        public Grouping(TKey key, IEnumerable<TElement> collection)
            : base(collection) => Key = key;
        public TKey Key { get; private set; }
    }

    public class ClothesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private List<string> sizes;

        // GET: Clothes
        public ActionResult Index(int? Sex, string selectedSize, string selectedBrand, string selectedType, string selectedPrice)
        {
            string[] array = { "", "XS", "S", "M", "L", "XL", "XXL" };
            sizes = new List<string>();
            sizes.AddRange(array);
            ViewBag.Sizes = new SelectList(sizes);

            var brands = new List<IGrouping<string, Clothes>>();
            brands.Add(new Grouping<string, Clothes>(""));
            brands.AddRange(db.Clothes.GroupBy(x => x.Brand).ToList());
            ViewBag.Brands = new SelectList(brands, "Key", "Key");

            var types = new List<IGrouping<string, Clothes>>();
            types.Add(new Grouping<string, Clothes>(""));
            types.AddRange(db.Clothes.GroupBy(x => x.Name).ToList());
            ViewBag.Types = new SelectList(types, "Key", "Key");

            var allClothes = db.Clothes.ToList();

            if (Sex != null && Sex != 3)
            {
                var typeEnym = Sex == 0 ? Models.Type.Male : Models.Type.Female;
                allClothes = allClothes.Where(x => x.Type == typeEnym).ToList();
            }

            if (selectedSize != null && !selectedSize.Equals(""))
            {
                foreach (var cloth in allClothes.ToList())
                {
                    var flag = 1;
                    foreach (var size in cloth.Sizes)
                    {
                        if (selectedSize.Equals(size.Size))
                        {
                            flag = 0;
                            break;
                        }
                    }

                    if (flag==1)
                    {
                        allClothes.Remove(cloth);
                    }
                }

            }

            if (selectedBrand != null && !selectedBrand.Equals(""))
            {
                allClothes = allClothes.Where(x => x.Brand == selectedBrand).ToList();
            }


            if (selectedType != null && !selectedType.Equals(""))
            {
                allClothes = allClothes.Where(x => x.Name == selectedType).ToList();
            }

            if (selectedPrice == null)
            {
                ViewBag.SelectedPrice = 1000;
            }
            else
            {
                var price = Int32.Parse(selectedPrice);
                allClothes = allClothes.Where(x => x.Price <= price).ToList();
                ViewBag.SelectedPrice = price;
            }

            var allChothesViewModel = new AllClothesViewModel();

            var result = new List<ClothesViewModel>();
            foreach (var cloth in allClothes)
            {
                result.Add(new ClothesViewModel
                {
                    Id = cloth.ID,
                    AvailablePieces = cloth.AvailablePieces,
                    Brand = cloth.Brand,
                    Image = cloth.Image,
                    Colors = cloth.Colors,
                    Name = cloth.Name,
                    Price = cloth.Price,
                    Sizes = cloth.Sizes,
                    ShoppingCarts = cloth.ShoppingCarts,
                    Type = cloth.Type,
                });
            }


            allChothesViewModel.AllClothes = result;
            return View(allChothesViewModel);
        }


        [Authorize(Roles = "shop")]
        public ActionResult ItemsForShop()
        {
            var username = System.Web.HttpContext.Current.User.Identity.Name;
            var user = db.Users.Where(x => x.UserName == username).ToList()[0];
            var allClothes = db.Clothes.Where(x => x.ApplicationUserId == user.Id).ToList();
            var allChothesViewModel = new AllClothesViewModel();

            var result = new List<ClothesViewModel>();
            foreach (var cloth in allClothes)
            {
                result.Add(new ClothesViewModel
                {
                    Id = cloth.ID,
                    AvailablePieces = cloth.AvailablePieces,
                    Brand = cloth.Brand,
                    Image = cloth.Image,
                    Colors = cloth.Colors,
                    Name = cloth.Name,
                    Price = cloth.Price,
                    Sizes = cloth.Sizes,
                    ShoppingCarts = cloth.ShoppingCarts,
                    Type = cloth.Type,
                });
            }
            allChothesViewModel.AllClothes = result;
            return View(allChothesViewModel);

        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clothes clothes = db.Clothes.Find(id);
            if (clothes == null)
            {
                return HttpNotFound();
            }
            return View(clothes);
        }
        [Authorize(Roles = "shop")]

        // GET: Clothes/Create
        public ActionResult Create()
        {
            string[] array = {"XS", "S", "M", "L", "XL", "XXL"};
            sizes = new List<string>();
            sizes.AddRange(array);
            ViewBag.Sizes = new MultiSelectList(sizes);
            return View();
        }

        // POST: Clothes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "shop")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Brand,Image,Price,Type,AvailablePieces")] Clothes clothes, string[] colors, string[] sizes)
        {
            if (ModelState.IsValid)
            {
                foreach (var color in colors)
                {
                    clothes.Colors.Add(new Colors(color));
                }

                foreach (var size in sizes)
                {
                    clothes.Sizes.Add(new Sizes(size));
                }
                var username = System.Web.HttpContext.Current.User.Identity.Name;
                var user = db.Users.Where(x => x.UserName == username).ToList()[0];
                if(user.clothes == null)
                {
                    clothes = new List<Clothes>();
                }
                user.clothes.Add(clothes);
                clothes.ApplicationUser = user;
                clothes.ApplicationUserId = user.Id;
                db.Clothes.Add(clothes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clothes);
        }
        

        [Authorize(Roles = "shop")]
        // GET: Clothes/Edit/5
        public ActionResult Edit(int? id)
        {
            string[] array = {"XS", "S", "M", "L", "XL", "XXL"};
            sizes = new List<string>();
            sizes.AddRange(array);
            ViewBag.Sizes = new MultiSelectList(sizes);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clothes clothes = db.Clothes.Find(id);
            if (clothes == null)
            {
                return HttpNotFound();
            }

            List<string> modelSizes = new List<string>();
            foreach (var size in clothes.Sizes)
            {
                modelSizes.Add(size.Size);
            }

            ViewBag.SelectedSizes = modelSizes;
            return View(clothes);
        }
        [Authorize(Roles = "shop")]
        // POST: Clothes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Brand,Price,Type,AvailablePieces,Image")] Clothes clothes,int id, string[] colors, string[] sizes)
        {
            if (ModelState.IsValid)
            {
                Clothes tmp = db.Clothes.Find(id);
                foreach (var color in colors)
                {
                    clothes.Colors.Add(new Colors(color));
                }
                foreach (var color in db.Colors)
                {
                    if (color.ClothesId == clothes.ID)
                        db.Colors.Remove(color);
                }
                foreach (var size in sizes)
                {
                    clothes.Sizes.Add(new Sizes(size));
                }
                foreach (var size in db.Sizes)
                {
                    if (size.ClothesId == clothes.ID)
                        db.Sizes.Remove(size);
                }
                db.Clothes.Remove(tmp);
                var username = System.Web.HttpContext.Current.User.Identity.Name;
                var user = db.Users.Where(x => x.UserName == username).ToList()[0];
                if (user.clothes == null)
                {
                    user.clothes = new List<Clothes>();
                }
                user.clothes.Add(clothes);
                clothes.ApplicationUser = user;
                clothes.ApplicationUserId = user.Id;
                db.Clothes.Add(clothes);
                //db.Entry(clothes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ItemsForShop");
            }
            return View(clothes);
        }

        [Authorize(Roles = "user")]
        [HttpGet]
        public ActionResult AddToCart(int? id)
        {
           
            var username = System.Web.HttpContext.Current.User.Identity.Name;
            var user = db.Users.Where(x => x.UserName == username).ToList()[0];
            var shoppingCartId = user.ShoppingCartID;

            var shoppingCart = db.ShoppingCarts.Find(shoppingCartId);
            if(id != null)
            {
                var item = db.Clothes.Find(id);
                if (!shoppingCart.Clothes.Contains(item))
                {
                    shoppingCart.Clothes.Add(item);
                    db.SaveChanges();
                }
            }

            ViewBag.shoppingCart = shoppingCart.Clothes;

            return View(shoppingCart);
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public ActionResult AddToCart()
        {
            var username = System.Web.HttpContext.Current.User.Identity.Name;
            var user = db.Users.Where(x => x.UserName == username).ToList()[0];
            var shoppingCartId = user.ShoppingCartID;

            var shoppingCart = db.ShoppingCarts.Find(shoppingCartId);

            shoppingCart?.Clothes.Clear();
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        // GET: Clothes/DeleteItem/5
        public ActionResult DeleteItem(int id)
        {
            
            var username = System.Web.HttpContext.Current.User.Identity.Name;
            var user = db.Users.Where(x => x.UserName == username).ToList()[0];
            var shoppingCartId = user.ShoppingCartID;

            var shoppingCart = db.ShoppingCarts.Find(shoppingCartId);

            if (shoppingCart != null)
            {
                var clothes = db.Clothes.Find(id);
                shoppingCart.Clothes.Remove(clothes);
                db.SaveChanges();
            }

            ViewBag.shoppingCart = shoppingCart.Clothes;

            // return View("AddToCart");
            return RedirectToAction("AddToCart");
        }
        
        // GET: Clothes/Delete/5  
        public ActionResult Delete(int id)
        {
            Clothes clothes = db.Clothes.Find(id);
            db.Clothes.Remove(clothes);
            db.SaveChanges();
            return RedirectToAction("ItemsForShop");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
