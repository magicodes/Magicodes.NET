
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Magicodes.Admin.Models;

namespace Magicodes.Admin.Controllers
{
    [Route("{action}/{id}")]
    public class SiteAdminNavigations : Controller
    {
        private MagicodesAdminContext db = new MagicodesAdminContext();

        // GET: 
        [Route]
        public async Task<ActionResult> Index()
        {
            return View(await db.SiteAdminNavigations.ToListAsync());
        }

        // GET: /Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            SiteAdminNavigation model = await db.SiteAdminNavigations.FindAsync(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: /Create
        [Route("Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Create
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SiteAdminNavigation model)
        {
            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid();
                db.SiteAdminNavigations.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: /Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            SiteAdminNavigation model = await db.SiteAdminNavigations.FindAsync(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: /Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SiteAdminNavigation model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: /Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            SiteAdminNavigation model = await db.SiteAdminNavigations.FindAsync(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: /Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            SiteAdminNavigation model = await db.SiteAdminNavigations.FindAsync(id);
            db.SiteAdminNavigations.Remove(model);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
