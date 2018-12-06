namespace ICareAlz.Controllers
{
    using ICareAlz.Models;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    [Authorize(Roles = "Instituto")]
    public class DietasController : Controller
    {
        private DataContext db = new DataContext();

        

        public ActionResult Index()
        {
            var user = db.Institutos.Where(u => u.Correo == User.Identity.Name).FirstOrDefault();


            var dietas = db.Dietas
                .Include(d => d.Instituto)
                .Where(t => t.InstitutoId == user.InstitutoId); 
            return View(dietas.ToList());
        }

       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dieta dieta = db.Dietas.Find(id);
            if (dieta == null)
            {
                return HttpNotFound();
            }
            return View(dieta);
        }

       
        public ActionResult Create()
        {
            var user = db.Institutos.Where(u => u.Correo == User.Identity.Name).FirstOrDefault();


            var dieta = new Dieta
            {
                InstitutoId = user.InstitutoId,
            };

            return View(dieta);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dieta dieta)
        {
            if (ModelState.IsValid)
            {
                db.Dietas.Add(dieta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dieta);
        }

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dieta dieta = db.Dietas.Find(id);
            if (dieta == null)
            {
                return HttpNotFound();
            }
        
            return View(dieta);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Dieta dieta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dieta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
             return View(dieta);
        }

       
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dieta dieta = db.Dietas.Find(id);
            if (dieta == null)
            {
                return HttpNotFound();
            }
            return View(dieta);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dieta dieta = db.Dietas.Find(id);
            db.Dietas.Remove(dieta);
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
