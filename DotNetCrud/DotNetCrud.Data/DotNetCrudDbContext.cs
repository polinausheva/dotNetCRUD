using DotNetCrud.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace DotNetCrud.Data
{
    public class DotNetCrudDbContext : IdentityDbContext<ApplicationUser>
    {
        public DotNetCrudDbContext()
            : base("Server=(LocalDb)\\MSSQLLocalDB;Database=Shop;Trusted_Connection=True;MultipleActiveResultSets=true")
        {

        }

        public DbSet<Product> Products{ get; set; }
        public DbSet<ProductGroup> ProductGroup{ get; set; }
        public DbSet<Purchase> Purchases{ get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here

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

        }
    }
}
