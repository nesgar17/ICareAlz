
namespace ICareAlz.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using ICareAlz.Helpers;
    using ICareAlz.Models;

    [Authorize(Roles = "Instituto")]
    public class TipsController : Controller
    {
        private DataContext db = new DataContext();



        public ActionResult Index()
        {
            var user = db.Institutos.Where(u => u.Correo == User.Identity.Name).FirstOrDefault();

            var tips = db.Tips
                .Include(t => t.CategoriaTip)
                .Include(t => t.Instituto)
                .Where(t => t.InstitutoId == user.InstitutoId);
            return View(tips.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tip tip = db.Tips.Find(id);
            if (tip == null)
            {
                return HttpNotFound();
            }
            return View(tip);
        }


        public ActionResult Create()
        {
            var user = db.Institutos.Where(u => u.Correo == User.Identity.Name).FirstOrDefault();


            ViewBag.CategoriaTipId = new SelectList(CombosHelper.GetCategoriaTips(), "CategoriaTipId", "Nombre");

            var tips = new Tip
            {
               InstitutoId = user.InstitutoId,
            };
            return View(tips);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tip tip)
        {
            var user = db.Institutos.Where(u => u.Correo == User.Identity.Name).FirstOrDefault();


            if (ModelState.IsValid)
            {
                db.Tips.Add(tip);
                db.SaveChanges();
                if (tip.ContenidoFile != null)
                {

                    var folder = "~/Content/ContenidoTips";
                    var file = string.Format("{0}{1}.jpg", tip.Titulo, user.InstitutoId);
                    var response = FileHelper.UploadPhoto(tip.ContenidoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        tip.Contenido = pic;
                        db.Entry(tip).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                }

                return RedirectToAction("Index");
            }

            ViewBag.CategoriaTipId = new SelectList(CombosHelper.GetCategoriaTips(), "CategoriaTipId", "Nombre", tip.CategoriaTipId);

            return View(tip);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tip tip = db.Tips.Find(id);
            if (tip == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaTipId = new SelectList(db.CategoriaTips, "CategoriaTipId", "Nombre", tip.CategoriaTipId);
            ViewBag.InstitutoId = new SelectList(db.Institutos, "InstitutoId", "Nombre", tip.InstitutoId);
            return View(tip);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tip tip)
        {
            var user = db.Institutos.Where(u => u.Correo == User.Identity.Name).FirstOrDefault();


            if (ModelState.IsValid)
            {
                db.Entry(tip).State = EntityState.Modified;
                db.SaveChanges();
                if (tip.ContenidoFile != null)
                {

                    var folder = "~/Content/ContenidoTips";
                    var file = string.Format("{0}{1}.jpg", tip.Titulo, user.InstitutoId);
                    var response = FileHelper.UploadPhoto(tip.ContenidoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        tip.Contenido = pic;
                        db.Entry(tip).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                }
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaTipId = new SelectList(db.CategoriaTips, "CategoriaTipId", "Nombre", tip.CategoriaTipId);
            //ViewBag.InstitutoId = new SelectList(db.Institutos, "InstitutoId", "Nombre", tip.InstitutoId);
            return View(tip);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tip tip = db.Tips.Find(id);
            if (tip == null)
            {
                return HttpNotFound();
            }
            return View(tip);
        }

        // POST: Tips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tip tip = db.Tips.Find(id);
            db.Tips.Remove(tip);
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
