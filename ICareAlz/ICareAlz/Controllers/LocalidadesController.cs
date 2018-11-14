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
    public class LocalidadesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Localidades
        public ActionResult Index()
        {
            var localidads = db.Localidades.Include(l => l.Municipio);
            return View(localidads.ToList());
        }

        // GET: Localidades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localidad localidad = db.Localidades.Find(id);
            if (localidad == null)
            {
                return HttpNotFound();
            }
            return View(localidad);
        }

        // GET: Localidades/Create
        public ActionResult Create()
        {
            ViewBag.MunicipioId = new SelectList(db.Municipios, "MunicipioId", "Nombre");
            return View();
        }

        // POST: Localidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocalidadId,MunicipioId,Clave,Nombre,Latitud,Longitud,Altitud,Carta,Ambito,Poblacion,Femenino,Masculino,Viviendas,Lat,Lng,Activo")] Localidad localidad)
        {
            if (ModelState.IsValid)
            {
                db.Localidades.Add(localidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MunicipioId = new SelectList(db.Municipios, "MunicipioId", "Nombre", localidad.MunicipioId);
            return View(localidad);
        }

        // GET: Localidades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localidad localidad = db.Localidades.Find(id);
            if (localidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.MunicipioId = new SelectList(db.Municipios, "MunicipioId", "Nombre", localidad.MunicipioId);
            return View(localidad);
        }

        // POST: Localidades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocalidadId,MunicipioId,Clave,Nombre,Latitud,Longitud,Altitud,Carta,Ambito,Poblacion,Femenino,Masculino,Viviendas,Lat,Lng,Activo")] Localidad localidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(localidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MunicipioId = new SelectList(db.Municipios, "MunicipioId", "Nombre", localidad.MunicipioId);
            return View(localidad);
        }

        // GET: Localidades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localidad localidad = db.Localidades.Find(id);
            if (localidad == null)
            {
                return HttpNotFound();
            }
            return View(localidad);
        }

        // POST: Localidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Localidad localidad = db.Localidades.Find(id);
            db.Localidades.Remove(localidad);
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
