using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using URL.Rewriter.DAL;
using URL.Rewriter.DAL.Models;
using URL.Rewriter.BLL;

namespace URL.Rewriter.WEB.Controllers
{
    public class UrlEntitiesController : Controller
    {
        private UrlsDbContext db = new UrlsDbContext();

        // GET: UrlEntities
        public ActionResult Index()
        {
            return View(db.Urls.ToList());
        }

        // GET: UrlEntities/Details/5
        public ActionResult LongUrl(string shortUrl)
        {
            if (shortUrl == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UrlEntity urlEntity = db.Urls.FirstOrDefault(u=> u.Short == shortUrl);

            if (urlEntity == null)
            {
                return HttpNotFound();
            }

            return Redirect(urlEntity.Long);
        }

        // GET: UrlEntities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UrlEntities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Short,Long")] UrlEntity urlEntity)
        {
            if (ModelState.IsValid)
            {
                var helper = new URL.Rewriter.BLL.UrlHelper();

                var shortUrl = helper.GenerateUrl(urlEntity.Long);

                urlEntity.Short = shortUrl;
            }

            return View(urlEntity);
        }

        // GET: UrlEntities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UrlEntity urlEntity = db.Urls.Find(id);
            if (urlEntity == null)
            {
                return HttpNotFound();
            }
            return View(urlEntity);
        }

        // POST: UrlEntities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Short,Long")] UrlEntity urlEntity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(urlEntity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(urlEntity);
        }

        // GET: UrlEntities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UrlEntity urlEntity = db.Urls.Find(id);
            if (urlEntity == null)
            {
                return HttpNotFound();
            }
            return View(urlEntity);
        }

        // POST: UrlEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UrlEntity urlEntity = db.Urls.Find(id);
            db.Urls.Remove(urlEntity);
            db.SaveChanges();
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
