using ICareAlz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ICareAlz.Controllers
{
    public class TestController : Controller
    {
        private DataContext db = new DataContext();

        public ActionResult HacerTest()
        {
            var respuesta = db.Respuestas;

            var preguntas = db.Preguntas.Where( p => p.PreguntaId == respuesta.FirstOrDefault().PreguntaId);

            var preguntaview = new TestView
            {
                Nombre = preguntas.FirstOrDefault().Nombre,
                NombreRespuesta = respuesta.FirstOrDefault().Nombre,
                PreguntaId = preguntas.FirstOrDefault().PreguntaId,
                RespuestaId = respuesta.FirstOrDefault().RespuestaId,
                Valor = respuesta.FirstOrDefault().Valor,
            };

            return View(preguntaview);
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