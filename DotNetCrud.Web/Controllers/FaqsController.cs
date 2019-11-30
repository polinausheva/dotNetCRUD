using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DotNetCrud.Web.Data.Models;
using DotNetCrud.Web.Models;
using DotNetCrud.Services;

namespace DotNetCrud.Web.Controllers
{
    public class FaqsController : Controller
    {
        //1. Delete db context*
        //2. Inject IGenericEFDataService<Faq>
        //3. Replace all db context* calls with the injected service calls
        //4. If u use async methods from the service => change the return types of methods to async Task<ActionResult>

        private IGenericEFDataService<Faq> faqs;

        public FaqsController(IGenericEFDataService<Faq> faqs)
        {
            this.faqs = faqs;
        }

        // GET: Faqs
        public async Task<ActionResult> Index()
        {
            var vm = await faqs.GetAllAsync();

            return View(vm);
        }

        // GET: Faqs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Faq faq = await faqs.GetSingleOrDefaultAsync(x=> x.Id == id);
            if (faq == null)
            {
                return HttpNotFound();
            }
            return View(faq);
        }

        // GET: Faqs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Faqs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Question,Answer")] Faq faq)
        {
            if (ModelState.IsValid)
            {
                faqs.Add(faq);

                return RedirectToAction(nameof(Index));
            }

            return View(faq);
        }

        // GET: Faqs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faq faq = await faqs.GetSingleOrDefaultAsync(x => x.Id == id);
            if (faq == null)
            {
                return HttpNotFound();
            }
            return View(faq);
        }

        // POST: Faqs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Question,Answer")] Faq faq)
        {
            if (ModelState.IsValid)
            {
                faqs.Update(faq);
                return RedirectToAction(nameof(Index));
            }
            return View(faq);
        }

        // GET: Faqs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faq faq = await faqs.GetSingleOrDefaultAsync(x=> x.Id == id);
            if (faq == null)
            {
                return HttpNotFound();
            }
            return View(faq);
        }

        // POST: Faqs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Faq faq = await faqs.GetSingleOrDefaultAsync(x => x.Id == id);
            faqs.Remove(faq);

            return RedirectToAction(nameof(Index));
        }
    }
}
