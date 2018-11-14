using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ICareAlz.Controllers
{
    using ICareAlz.Models;
    using System.Linq;
    using System.Web.Mvc;

    public class GenericController : Controller
    {
        private DataContext db = new DataContext();

        public JsonResult GetMunicipios(int EstadoId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var municipalities = db.Municipios.Where(e => e.EstadoId == EstadoId);
            return Json(municipalities);
        }

        public JsonResult GetLocalidades(int MunicipioId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var colonies = db.Localidades.Where(e => e.MunicipioId == MunicipioId);
            return Json(colonies);
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