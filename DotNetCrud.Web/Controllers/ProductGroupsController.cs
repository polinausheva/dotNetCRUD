using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DotNetCrud.Services;
using DotNetCrud.Web.Data.Models;
using DotNetCrud.Web.Models;

namespace DotNetCrud.Web.Controllers
{
    public class ProductGroupsController : Controller
    {
        private IGenericEFDataService<ProductGroup> productGroups;

        public ProductGroupsController(IGenericEFDataService<ProductGroup> productGroups)
        {
            this.productGroups = productGroups;
        }

        // GET: ProductGroups
        public async Task<ActionResult> Index()
        {
            var vm = await productGroups.GetAllAsync();

            return View(vm);
        }

        // GET: ProductGroups/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductGroup productGroup = await productGroups.GetSingleOrDefaultAsync(x => x.Id == id);

            if (productGroup == null)
            {
                return HttpNotFound();
            }

            return View(productGroup);
        }

        // GET: ProductGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] ProductGroup productGroup)
        {
            if (ModelState.IsValid)
            {
                productGroups.Add(productGroup);
                return RedirectToAction("Index");
            }

            return View(productGroup);
        }

        // GET: ProductGroups/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductGroup productGroup = await productGroups.GetSingleOrDefaultAsync(x=> x.Id == id);
            if (productGroup == null)
            {
                return HttpNotFound();
            }
            return View(productGroup);
        }

        // POST: ProductGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] ProductGroup productGroup)
        {
            if (ModelState.IsValid)
            {
                productGroups.Update(productGroup);

                return RedirectToAction("Index");
            }
            return View(productGroup);
        }

        // GET: ProductGroups/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductGroup productGroup = await productGroups.GetSingleOrDefaultAsync(x => x.Id == id);
            if (productGroup == null)
            {
                return HttpNotFound();
            }
            return View(productGroup);
        }

        // POST: ProductGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProductGroup productGroup = await productGroups.GetSingleOrDefaultAsync(x => x.Id == id);

            productGroups.Remove(productGroup);
            return RedirectToAction("Index");
        }
    }
}
