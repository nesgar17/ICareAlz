using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ICareAlz.Models;

namespace ICareAlz.Controllers
{
    public class CategoriaTipsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: CategoriaTips
        public ActionResult Index()
        {
            return View(db.CategoriaTips.ToList());
        }

        // GET: CategoriaTips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaTip categoriaTip = db.CategoriaTips.Find(id);
            if (categoriaTip == null)
            {
                return HttpNotFound();
            }
            return View(categoriaTip);
        }

        // GET: CategoriaTips/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriaTips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoriaTipId,Nombre")] CategoriaTip categoriaTip)
        {
            if (ModelState.IsValid)
            {
                db.CategoriaTips.Add(categoriaTip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoriaTip);
        }

        // GET: CategoriaTips/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaTip categoriaTip = db.CategoriaTips.Find(id);
            if (categoriaTip == null)
            {
                return HttpNotFound();
            }
            return View(categoriaTip);
        }

        // POST: CategoriaTips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoriaTipId,Nombre")] CategoriaTip categoriaTip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoriaTip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoriaTip);
        }

        // GET: CategoriaTips/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaTip categoriaTip = db.CategoriaTips.Find(id);
            if (categoriaTip == null)
            {
                return HttpNotFound();
            }
            return View(categoriaTip);
        }

        // POST: CategoriaTips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoriaTip categoriaTip = db.CategoriaTips.Find(id);
            db.CategoriaTips.Remove(categoriaTip);
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
