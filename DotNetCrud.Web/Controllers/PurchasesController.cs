using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using DotNetCrud.Services;
using DotNetCrud.Web.Data.Models;
using DotNetCrud.Web.Models;
using Microsoft.AspNet.Identity;

namespace DotNetCrud.Web.Controllers
{
    public class PurchasesController : Controller
    {
        private IGenericEFDataService<Purchase> purchases;
        private IGenericEFDataService<Product> products;
        private ApplicationDbContext db;

        public PurchasesController(IGenericEFDataService<Purchase> purchases, IGenericEFDataService<Product> products, ApplicationDbContext db)
        {
            this.purchases = purchases;
            this.products = products;

            this.db = db;
        }

        // GET: Purchases
        public async Task<ActionResult> Index()
        {
            var vm = await purchases.GetAllAsync();

            return View(vm);
        }

        // GET: Purchases/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = await purchases.GetSingleOrDefaultAsync(x => x.Id == id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // GET: Purchases/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.ApplicationUserId = new SelectList(await db.Users.ToListAsync(), "Id", "Email");
            return View();
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ApplicationUserId,DateFulfilled")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                purchase.DateFulfilled = DateTime.Now;
                purchases.Add(purchase);
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserId = new SelectList(await db.Users.ToListAsync(), "Id", "Email", purchase.ApplicationUserId);
            return View(purchase);
        }

        // GET: Purchases/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Purchase purchase = await purchases.GetSingleOrDefaultAsync(x => x.Id == id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(await db.Users.ToListAsync(), "Id", "Email", purchase.ApplicationUserId);
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ApplicationUserId,DateFulfilled")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                purchases.Update(purchase);
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(await db.Users.ToListAsync(), "Id", "Email", purchase.ApplicationUserId);
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = await purchases.GetSingleOrDefaultAsync(x => x.Id == id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Purchase purchase = await purchases.GetSingleOrDefaultAsync(x => x.Id == id);
            purchases.Remove(purchase);
            return RedirectToAction("Index");
        } 
        
        
        // Get: Purchases/BuyNow/5
        public async Task<ActionResult> BuyNow(int id)
        {
            Product product = products.GetSingleOrDefault(x => x.Id == id);

            if (product == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            ApplicationUser user = await db.Users.SingleOrDefaultAsync(x => x.Email == User.Identity.Name);
            Purchase purchase = new Purchase()
            {
                ApplicationUser = user,
                ApplicationUserId = user.Id,
                DateFulfilled = DateTime.Now,
                Products = new List<Product>()
            };

            //TODO: ADD FUNCTIONALITY TO ADD MORE PRODUCTS TO A PURCHASE LATER (CART FOR EXAMPLE)
            purchase.Products.Add(product);

            purchases.Add(purchase);

            return RedirectToAction("Index");
        }
    }
}
