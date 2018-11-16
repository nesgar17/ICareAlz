using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ICareAlz.Helpers;
using ICareAlz.Models;

namespace ICareAlz.Controllers
{
    public class SolicitudesController : Controller
    {
        private DataContext db = new DataContext();

        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            var solicituds = db.Solicituds
                .Include(s => s.Categoria)
                .Include(s => s.Estado)
                .Include(s => s.Municipio)
                .Include(s => s.Localidad);
            return View(solicituds.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult SolicitudesAceptadas()
        {
            var solicitudes = db.Solicituds
                .Include(s => s.Categoria)
                .Include(s => s.Estado)
                .Include(s => s.Municipio)
                .Include(s => s.Localidad).Where(s => s.Status == "Aceptada");
            return View(solicitudes.ToList());
        }

        public ActionResult Success()
        {
            return View();
        }

        public async Task Validar(int? id)
        {
            if (id == null)
            {
                ViewBag.Message = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var solicitud = db.Solicituds.Find(id);
            solicitud.Status = "Aceptada";
            db.Entry(solicitud).State = EntityState.Modified;
            var response = DbHelper.SaveChanges(db);
            if (response.Successfully)
            {
                await EnviarCorreo(id);
                this.RegistrarInstituto(solicitud);
                RedirectToAction("Success");
            }
            ModelState.AddModelError(string.Empty, response.Message);


        }

        private void RegistrarInstituto(Solicitud solicitud)
        {
            var instituto = new Instituto
            {
                CategoriaId = solicitud.CategoriaId,
                Correo = solicitud.Correo,
                Descripcion = solicitud.Descripcion,
                Direccion = solicitud.Direccion,
                EstadoId = solicitud.EstadoId,
                FechaFundacion = solicitud.FechaFundacion,
                LocalidadId = solicitud.LocalidadId,
                Logo = solicitud.Logo,
                MunicipioId = solicitud.MunicipioId,
                Nombre = solicitud.Nombre,
                Password = solicitud.Password,
                Responsable = solicitud.Responsable,
                Telefono = solicitud.Telefono,
                
            };
            db.Institutos.Add(instituto);
            db.SaveChanges();
            UsersHelper.CreateUserASP(solicitud.Correo, "Instituto", solicitud.Password);

        }

        public async Task EnviarCorreo(int? id)
        {
            MailHelper mail = new MailHelper();
            var solicitud = db.Solicituds.Find(id);
            string subject = "--Respuesta a Solicitud--";
            string body = "Felicitaciones tu solicitud ha sido aprobada, ahora ya formas parte de CareAlz \n" +
                         "Apartir de hoy puedes ingresar a la plataforma con los siguentes datos," + "Usuario: " + solicitud.Correo + "Password:" + solicitud.Correo;

            await mail.SendMail(solicitud.Correo, subject, body);

        }

        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitud solicitud = db.Solicituds.Find(id);
            if (solicitud == null)
            {
                return HttpNotFound();
            }
            return View(solicitud);
        }

        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(CombosHelper.GetCategorias(), "CategoriaId", "Nombre");
            ViewBag.EstadoId = new SelectList(CombosHelper.GetEstados(), "EstadoId", "Nombre");
            ViewBag.MunicipioId = new SelectList(CombosHelper.GetMunicipios(0), "MunicipioId", "Nombre");
            ViewBag.LocalidadId = new SelectList(CombosHelper.GetLocalidades(0), "LocalidadId", "Nombre");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Solicitud solicitud)
        {
            if (ModelState.IsValid)
            {
                solicitud.Status = "Espera";
                solicitud.Password = solicitud.Correo;
                db.Solicituds.Add(solicitud);
                db.SaveChanges();
                return RedirectToAction("Mensaje");
            }

            ViewBag.CategoriaId = new SelectList(CombosHelper.GetCategorias(), "CategoriaId", "Nombre", solicitud.CategoriaId);
            ViewBag.EstadoId = new SelectList(CombosHelper.GetEstados(), "EstadoId", "Nombre", solicitud.EstadoId);
            ViewBag.MunicipioId = new SelectList(CombosHelper.GetMunicipios(solicitud.MunicipioId), "MunicipioId", "Nombre", solicitud.MunicipioId);
            ViewBag.LocalidadId = new SelectList(CombosHelper.GetLocalidades(solicitud.LocalidadId), "LocalidadId", "Nombre", solicitud.LocalidadId);
            return View(solicitud);
        }

        public ActionResult Mensaje()
        {
            return View();
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitud solicitud = db.Solicituds.Find(id);
            if (solicitud == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaId = new SelectList(CombosHelper.GetCategorias(), "CategoriaId", "Nombre", solicitud.CategoriaId);
            ViewBag.EstadoId = new SelectList(CombosHelper.GetEstados(), "EstadoId", "Nombre", solicitud.EstadoId);
            ViewBag.MunicipioId = new SelectList(CombosHelper.GetMunicipios(solicitud.MunicipioId), "MunicipioId", "Nombre", solicitud.MunicipioId);
            ViewBag.LocalidadId = new SelectList(CombosHelper.GetLocalidades(solicitud.LocalidadId), "LocalidadId", "Nombre", solicitud.LocalidadId);
            return View(solicitud);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Solicitud solicitud)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitud).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaId = new SelectList(CombosHelper.GetCategorias(), "CategoriaId", "Nombre", solicitud.CategoriaId);
            ViewBag.EstadoId = new SelectList(CombosHelper.GetEstados(), "EstadoId", "Nombre", solicitud.EstadoId);
            ViewBag.MunicipioId = new SelectList(CombosHelper.GetMunicipios(solicitud.MunicipioId), "MunicipioId", "Nombre", solicitud.MunicipioId);
            ViewBag.LocalidadId = new SelectList(CombosHelper.GetLocalidades(solicitud.LocalidadId), "LocalidadId", "Nombre", solicitud.LocalidadId);
            return View(solicitud);
        }

        // GET: Solicitudes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitud solicitud = db.Solicituds.Find(id);
            if (solicitud == null)
            {
                return HttpNotFound();
            }
            return View(solicitud);
        }

        // POST: Solicitudes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Solicitud solicitud = db.Solicituds.Find(id);
            db.Solicituds.Remove(solicitud);
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
