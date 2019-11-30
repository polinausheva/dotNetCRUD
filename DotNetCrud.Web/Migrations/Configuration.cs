using System.Collections.Generic;
using DotNetCrud.Web.Data.Models;

namespace DotNetCrud.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DotNetCrud.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DotNetCrud.Web.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var productGroups = new List<ProductGroup>
            {
                new ProductGroup{Name = "Fruits & Vegetables"},
                new ProductGroup{Name = "Womans Clothing"},
                new ProductGroup{Name = "Mens Clothing"},
                new ProductGroup{Name = "Toys"},
            };
            productGroups.ForEach(s => context.ProductGroup.AddOrUpdate(s));
            context.SaveChanges();

            var products = new List<Product>
            {
                new Product{Name = "Banana", Barcode = "LOREMIPSUM00000", Description = "Lorem ipsum dolor sit amet...", Price = 5.99m, ProductGroupId = 1},
                new Product{Name = "Watermelon", Barcode = "LOREMIPSUM1111", Description = "Kenfo drese calor sit amet...", Price = 3.99m, ProductGroupId = 1},
                new Product{Name = "Train on rails", Barcode = "LOREMIPSUM02020202", Description = "Treno faro keti male amet...", Price = 15.85m, ProductGroupId = 4},
                new Product{Name = "Formal shirt", Barcode = "LOREMIPSUM333333", Description = "Cano seto ani ting elso dolor sit amet...", Price = 5.99m, ProductGroupId = 3},
            };
            products.ForEach(s => context.Products.AddOrUpdate(s));
            context.SaveChanges();

            // User and purchases seeding is omitted so app functionality can be demonstrated
        }
    }
}
