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

            var faqs = new List<Faq>
            {
                new Faq{Id = 1, Question = "How to purchase an item?", Answer = "I don't know yet, ask the developers :)"},
                new Faq{Id = 2, Question = "Who's the best developer on the planet?", Answer = "The one who developed this app..."},
                new Faq{Id = 3, Question = "How do people rate Poli Usheva?", Answer = "Straight 10/10 brat ;)"},
            };
            faqs.ForEach(f => context.Faqs.AddOrUpdate(f));
            context.SaveChanges();

            var productGroups = new List<ProductGroup>
            {
                new ProductGroup{Id = 1, Name = "Fruits & Vegetables"},
                new ProductGroup{Id = 2, Name = "Womans Clothing"},
                new ProductGroup{Id = 3, Name = "Mens Clothing"},
                new ProductGroup{Id = 4, Name = "Toys"},
            };
            productGroups.ForEach(s => context.ProductGroup.AddOrUpdate(s));
            context.SaveChanges();

            var products = new List<Product>
            {
                new Product{Id = 1, Name = "Banana", Barcode = "LOREMIPSUM00000", Description = "Lorem ipsum dolor sit amet...", Price = 5.99m, ProductGroupId = 1},
                new Product{Id = 2, Name = "Watermelon", Barcode = "LOREMIPSUM1111", Description = "Kenfo drese calor sit amet...", Price = 3.99m, ProductGroupId = 1},
                new Product{Id = 3, Name = "Train on rails", Barcode = "LOREMIPSUM02020202", Description = "Treno faro keti male amet...", Price = 15.85m, ProductGroupId = 4},
                new Product{Id = 4, Name = "Formal shirt", Barcode = "LOREMIPSUM333333", Description = "Cano seto ani ting elso dolor sit amet...", Price = 5.99m, ProductGroupId = 3},
            };
            products.ForEach(s => context.Products.AddOrUpdate(s));
            context.SaveChanges();

            // User and purchases seeding is omitted so app functionality can be demonstrated
        }
    }
}
