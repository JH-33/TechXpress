using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
    public class TechXpressDBContext : IdentityDbContext<ApplicationUser>
    {
        public TechXpressDBContext(DbContextOptions<TechXpressDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.ShoppingCart)
                .WithOne(a => a.User)
                .HasForeignKey<ShoppingCart>(a => a.UserID);

            modelBuilder.Entity<Order>()
                .HasOne(a => a.Payment)
                .WithOne(a => a.Order)
                .HasForeignKey<Payment>(a=> a.Id);

            modelBuilder.Entity<Order>()
                .HasOne(a => a.ShoppingCart)
                .WithOne(a => a.Order)
                .HasForeignKey<Order>(a => a.Id);
            
            
            base.OnModelCreating(modelBuilder);


        }
        public override int SaveChanges()
        {
            ApplyAuditInfo();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditInfo();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAuditInfo()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is  baseEntity &&
                           (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted));

            foreach (var entry in entries)
            {
                var entity = (baseEntity)entry.Entity;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreatedDate = DateTime.Now;
                        entity.UpdatedDate = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entity.UpdatedDate = DateTime.Now;
                        break;

                    case EntityState.Deleted:
                       
                        entry.State = EntityState.Modified;
                        entity.IsDeleted = true;
                        entity.DeletedDate = DateTime.Now;
                        break;
                }
            }
        }


        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Discount> discounts { get; set; }



    }
}
