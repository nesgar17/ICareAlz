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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ICareAlz.Controllers
{
    public class UsuariosController : Controller
    {
        private DataContext db = new DataContext();
        private static ApplicationDbContext userContext = new ApplicationDbContext();


        // GET: Usuarios
        public ActionResult Index()
        {
            var usuarios = db.Usuarios.Include(u => u.Estado).Include(u => u.Localidad).Include(u => u.Municipio);
            return View(usuarios.ToList());
        }

      
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        public ActionResult Create()
        {
            ViewBag.EstadoId = new SelectList(CombosHelper.GetEstados(), "EstadoId", "Nombre");
            ViewBag.LocalidadId = new SelectList(CombosHelper.GetLocalidades(0), "LocalidadId", "Nombre");
            ViewBag.MunicipioId = new SelectList(CombosHelper.GetMunicipios(0), "MunicipioId", "Nombre");
            return View();
        }

        public  ActionResult Mensaje()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Usuario usuario)
        {
          

            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                var response = DbHelper.SaveChanges(db);
               UsersHelper.CreateUserASP(usuario.Correo, "Paciente", usuario.Password);
              
           

                if (response.Successfully)
                {
                
                    if (usuario.FotoFile != null)
                    {

                        var folder = "~/Content/Fotos";
                        var file = $"{usuario.UsuarioId}{usuario.Nombre}";
                        var responsefile = FileHelper.UploadPhoto(usuario.FotoFile, folder, file);
                        if (responsefile)
                        {
                            var pic = $"{folder}{file}.jpg";
                            usuario.Foto = pic;
                            db.Entry(usuario).State = EntityState.Modified;
                            db.SaveChanges();

                        }

                    }
                    return RedirectToAction("Mensaje");
                }
                ModelState.AddModelError(string.Empty, response.Message);


            }

            ViewBag.EstadoId = new SelectList(CombosHelper.GetEstados(), "EstadoId", "Nombre", usuario.EstadoId);
            ViewBag.LocalidadId = new SelectList(CombosHelper.GetLocalidades(usuario.LocalidadId), "LocalidadId", "Nombre", usuario.LocalidadId);
            ViewBag.MunicipioId = new SelectList(CombosHelper.GetMunicipios(usuario.MunicipioId), "MunicipioId", "Nombre", usuario.MunicipioId);
            return View(usuario);
        }

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstadoId = new SelectList(CombosHelper.GetEstados(), "EstadoId", "Nombre", usuario.EstadoId);
            ViewBag.LocalidadId = new SelectList(CombosHelper.GetLocalidades(usuario.LocalidadId), "LocalidadId", "Nombre", usuario.LocalidadId);
            ViewBag.MunicipioId = new SelectList(CombosHelper.GetMunicipios(usuario.MunicipioId), "MunicipioId", "Nombre", usuario.MunicipioId);
            return View(usuario);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                var response = DbHelper.SaveChanges(db);
                if (response.Successfully)
                {
                    if (usuario.FotoFile != null)
                    {

                        var folder = "~/Content/Fotos";
                        var file = $"{usuario.UsuarioId}{usuario.Nombre}";
                        var responsefile = FileHelper.UploadPhoto(usuario.FotoFile, folder, file);
                        if (responsefile)
                        {
                            var pic = $"{folder}{file}.jpg";
                            usuario.Foto = pic;
                            db.Entry(usuario).State = EntityState.Modified;
                            db.SaveChanges();

                        }

                    }
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, response.Message);
            }
            ViewBag.EstadoId = new SelectList(CombosHelper.GetEstados(), "EstadoId", "Nombre", usuario.EstadoId);
            ViewBag.LocalidadId = new SelectList(CombosHelper.GetLocalidades(usuario.LocalidadId), "LocalidadId", "Nombre", usuario.LocalidadId);
            ViewBag.MunicipioId = new SelectList(CombosHelper.GetMunicipios(usuario.MunicipioId), "MunicipioId", "Nombre", usuario.MunicipioId);
            return View(usuario);
        }

       
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
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
