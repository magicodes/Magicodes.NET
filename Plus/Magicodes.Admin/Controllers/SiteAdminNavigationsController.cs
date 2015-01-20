using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Magicodes.Admin;
using Magicodes.Admin.Models;

namespace Magicodes.Admin.Controllers
{
    public class SiteAdminNavigationsController : Controller
    {
        private MagicodesAdminContext db = new MagicodesAdminContext();

        // GET: /SiteAdminNavigations/
        public async Task<ActionResult> Index()
        {
            return View(await db.SiteAdminNavigations.ToListAsync());
        }

        // GET: /SiteAdminNavigations/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteAdminNavigation siteAdminNavigation = await db.SiteAdminNavigations.FindAsync(id);
            if (siteAdminNavigation == null)
            {
                return HttpNotFound();
            }
            return View(siteAdminNavigation);
        }

        // GET: /SiteAdminNavigations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /SiteAdminNavigations/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,ParentId,Text,Href,IconCls,TextCls,IsShowBadge,MenuBadgeType,BadgeRequestUrl,PlusId,SortNo,CreateTime,UpdateTime,Deleted,CreateBy,UpdateBy")] SiteAdminNavigation siteAdminNavigation)
        {
            if (ModelState.IsValid)
            {
                siteAdminNavigation.Id = Guid.NewGuid();
                db.SiteAdminNavigations.Add(siteAdminNavigation);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(siteAdminNavigation);
        }

        // GET: /SiteAdminNavigations/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteAdminNavigation siteAdminNavigation = await db.SiteAdminNavigations.FindAsync(id);
            if (siteAdminNavigation == null)
            {
                return HttpNotFound();
            }
            return View(siteAdminNavigation);
        }

        // POST: /SiteAdminNavigations/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,ParentId,Text,Href,IconCls,TextCls,IsShowBadge,MenuBadgeType,BadgeRequestUrl,PlusId,SortNo,CreateTime,UpdateTime,Deleted,CreateBy,UpdateBy")] SiteAdminNavigation siteAdminNavigation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siteAdminNavigation).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(siteAdminNavigation);
        }

        // GET: /SiteAdminNavigations/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteAdminNavigation siteAdminNavigation = await db.SiteAdminNavigations.FindAsync(id);
            if (siteAdminNavigation == null)
            {
                return HttpNotFound();
            }
            return View(siteAdminNavigation);
        }

        // POST: /SiteAdminNavigations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            SiteAdminNavigation siteAdminNavigation = await db.SiteAdminNavigations.FindAsync(id);
            db.SiteAdminNavigations.Remove(siteAdminNavigation);
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
