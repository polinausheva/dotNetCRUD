using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using DotNetCrud.Services;
using DotNetCrud.Web.Data.Models;
using DotNetCrud.Web.Models;

namespace DotNetCrud.Web.Controllers
{
    public class ProductsController : Controller
    {
        private IGenericEFDataService<Product> products;
        private IGenericEFDataService<ProductGroup> productGroups;
        private ApplicationDbContext db;

        public ProductsController(IGenericEFDataService<Product> products, IGenericEFDataService<ProductGroup> productGroups, ApplicationDbContext db)
        {
            this.products = products;
            this.productGroups = productGroups;
            this.db = db;
        }

        // GET: Products
        public async Task<ActionResult> Index()
        {
            var products = await this.products.GetAllAsync();

            return View(products);
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = await this.products.GetSingleOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.ProductGroupId = new SelectList(await productGroups.GetAllAsync(), "Id", "Name");

            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description,Price,Barcode,ProductGroupId")] Product product)
        {
            if (ModelState.IsValid)
            {
                products.Add(product);
                return RedirectToAction("Index");
            }

            ViewBag.ProductGroupId = new SelectList(await productGroups.GetAllAsync(), "Id", "Name", product.ProductGroupId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = await products.GetSingleOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductGroupId = new SelectList(await productGroups.GetAllAsync(), "Id", "Name", product.ProductGroupId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,Price,Barcode,ProductGroupId")] Product product)
        {
            if (ModelState.IsValid)
            {
                products.Update(product);

                return RedirectToAction("Index");
            }
            ViewBag.ProductGroupId = new SelectList(await productGroups.GetAllAsync(), "Id", "Name", product.ProductGroupId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await products.GetSingleOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await products.GetSingleOrDefaultAsync(x => x.Id == id);
            products.Remove(product);

            return RedirectToAction("Index");
        }
    }
}
