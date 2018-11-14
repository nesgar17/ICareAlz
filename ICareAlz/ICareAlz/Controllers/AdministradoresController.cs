namespace ICareAlz.Controllers
{
    using ICareAlz.Helpers;
    using ICareAlz.Models;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    [Authorize(Users = "ngarciagarcia33@gmail.com")]
    public class AdministradoresController : Controller
    {
        private DataContext db = new DataContext();

        
       
        public ActionResult Index()
        {
            var administradors = db.Administradors.Include(a => a.Estado).Include(a => a.Localidad).Include(a => a.Municipio);
            return View(administradors.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrador administrador = db.Administradors.Find(id);
            if (administrador == null)
            {
                return HttpNotFound();
            }
            return View(administrador);
        }

        // GET: Administradores/Create
        public ActionResult Create()
        {
            ViewBag.EstadoId = new SelectList(CombosHelper.GetEstados(), "EstadoId", "Nombre");
            ViewBag.LocalidadId = new SelectList(CombosHelper.GetLocalidades(0), "LocalidadId", "Nombre");
            ViewBag.MunicipioId = new SelectList(CombosHelper.GetMunicipios(0), "MunicipioId", "Nombre");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Administrador administrador)
        {
            if (!ModelState.IsValid)
            {
                administrador.Password = administrador.Correo;
                db.Administradors.Add(administrador);
                var response = DbHelper.SaveChanges(db);
                UsersHelper.CreateUserASP(administrador.Correo, "Admin", administrador.Password);
                if (administrador.FotoFile != null)
                {

                    var folder = "~/Content/Fotos";
                    var file = string.Format("{0}{1}.jpg", administrador.AdministradorId,administrador.Nombre);
                    var responsefile = FileHelper.UploadPhoto(administrador.FotoFile, folder, file);
                    if (responsefile)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        administrador.Foto = pic;
                        db.Entry(administrador).State = EntityState.Modified;
                        db.SaveChanges();

                    }

                }
                if (response.Successfully)
                {

                    return RedirectToAction("Index");

                }
                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.EstadoId = new SelectList(CombosHelper.GetEstados(), "EstadoId", "Nombre", administrador.EstadoId);
            ViewBag.LocalidadId = new SelectList(CombosHelper.GetLocalidades(administrador.LocalidadId), "LocalidadId", "Nombre", administrador.LocalidadId);
            ViewBag.MunicipioId = new SelectList(CombosHelper.GetMunicipios(administrador.MunicipioId), "MunicipioId", "Nombre", administrador.MunicipioId);
            return View(administrador);
        }

       
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrador administrador = db.Administradors.Find(id);
            if (administrador == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstadoId = new SelectList(CombosHelper.GetEstados(), "EstadoId", "Nombre", administrador.EstadoId);
            ViewBag.LocalidadId = new SelectList(CombosHelper.GetLocalidades(administrador.LocalidadId), "LocalidadId", "Nombre", administrador.LocalidadId);
            ViewBag.MunicipioId = new SelectList(CombosHelper.GetMunicipios(administrador.MunicipioId), "MunicipioId", "Nombre", administrador.MunicipioId);
            return View(administrador);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Administrador administrador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(administrador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EstadoId = new SelectList(CombosHelper.GetEstados(), "EstadoId", "Nombre", administrador.EstadoId);
            ViewBag.LocalidadId = new SelectList(CombosHelper.GetLocalidades(administrador.LocalidadId), "LocalidadId", "Nombre", administrador.LocalidadId);
            ViewBag.MunicipioId = new SelectList(CombosHelper.GetMunicipios(administrador.MunicipioId), "MunicipioId", "Nombre", administrador.MunicipioId);
            return View(administrador);
        }

       
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrador administrador = db.Administradors.Find(id);
            if (administrador == null)
            {
                return HttpNotFound();
            }
            return View(administrador);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Administrador administrador = db.Administradors.Find(id);
            db.Administradors.Remove(administrador);
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
