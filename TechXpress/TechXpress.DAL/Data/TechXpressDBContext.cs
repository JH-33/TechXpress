using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TechXpress.DAL.Data.Models;

namespace TechXpress.DAL.Data
{
    public class TechXpressDBContext : DbContext
    {
        public TechXpressDBContext(DbContextOptions<TechXpressDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(a => a.ShoppingCart)
                .WithOne(a => a.User)
                .HasForeignKey<ShoppingCart>(a => a.UserID);

            modelBuilder.Entity<Order>()
                .HasOne(a => a.Payment)
                .WithOne(a => a.Order)
                .HasForeignKey<Payment>(a=> a.OrderID);

            modelBuilder.Entity<Order>()
                .HasOne(a => a.ShoppingCart)
                .WithOne(a => a.Order)
                .HasForeignKey<Order>(a => a.ShoppingCart_ID);

        



            base.OnModelCreating(modelBuilder);


        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Payment> Payment { get; set; }

    }
}
