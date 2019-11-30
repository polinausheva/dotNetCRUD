using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNetCrud.Web.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DotNetCrud.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual IList<Purchase> Purchases { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGroup> ProductGroup { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //            //Write Fluent API configurations here
            
            // describe users - products
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Purchases)
                .WithRequired(p => p.ApplicationUser)
                .HasForeignKey(p => p.ApplicationUserId);
            
            // describe purchase - products
            modelBuilder.Entity<Purchase>()
                .HasMany(p => p.Products)
                .WithMany(pg => pg.Purchases)
                // describe mapping table
                .Map(cs =>
                {
                    cs.MapLeftKey("PurchaseId");
                    cs.MapRightKey("ProductId");
            
                    cs.ToTable("ProductPurchases");
                });
            
            // describe product - productGroup
            modelBuilder.Entity<Product>()
                .HasRequired(p => p.ProductGroup)
                .WithMany(pg => pg.Products)
                .HasForeignKey(p => p.ProductGroupId);

            // the all important base class call! Add this line to make your problems go away.
            base.OnModelCreating(modelBuilder);
        }
    }
}