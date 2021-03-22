using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IT_project.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //add custom objects, lists etc here
        public int ShoppingCartID { get; set; }
        public virtual ICollection <Clothes> clothes { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Clothes> Clothes { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Sizes> Sizes { get; set; }
        public DbSet<Colors> Colors { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        // public System.Data.Entity.DbSet<IT_project.Models.ShoppingCart> ShoppingCarts { get; set; }

        //public System.Data.Entity.DbSet<IT_project.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}