

namespace ICareAlz.Helpers
{

    using ICareAlz.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;


    public class CombosHelper : IDisposable
    {


        private static DataContext db = new DataContext();

        public static List<Estado> GetEstados()
        {
            var states = db.Estadoes.ToList();
            states.Add(new Estado
            {
                EstadoId = 0,
                Nombre = "[--Selecciona un Estado--]",
            });

            return states.OrderBy(e => e.Nombre).ToList();
        }

        public static List<Municipio> GetMunicipios()
        {
            var municipalities = db.Municipios.ToList();
            municipalities.Add(new Municipio
            {
                MunicipioId = 0,
                Nombre = "[--Selecciona un Municipio--]",
            });

            return municipalities.OrderBy(e => e.Nombre).ToList();
        }
        public static List<Municipio> GetMunicipios(int EstadoId)
        {
            var municipalities = db.Municipios.Where(m => m.EstadoId == EstadoId).ToList();
            municipalities.Add(new Municipio
            {
                MunicipioId = 0,
                Nombre = "[--Selecciona un Municipio--]",
            });

            return municipalities.OrderBy(e => e.Nombre).ToList();
        }

        public static List<Categoria> GetCategorias()
        {
            var categorias = db.Categorias.ToList();
            categorias.Add(new Categoria
            {
                CategoriaId = 0,
                Nombre = "[--Selecciona una Localidad--]",
            });

            return categorias.OrderBy(e => e.Nombre).ToList();
        }

        public static List<Localidad> GetLocalidades()
        {
            var colonies = db.Localidades.ToList();
            colonies.Add(new Localidad
            {
                LocalidadId = 0,
                Nombre = "[--Selecciona una Localidad--]",
            });

            return colonies.OrderBy(e => e.Nombre).ToList();
        }

        public static List<Localidad> GetLocalidades(int MunicipioId)
        {
            var colonies = db.Localidades.Where(m => m.MunicipioId == MunicipioId).ToList();
            colonies.Add(new Localidad
            {
                LocalidadId = 0,
                Nombre = "[--Selecciona una Localidad--]",
            });

            return colonies.OrderBy(e => e.Nombre).ToList();
        }

        public static List<CategoriaTip> GetCategoriaTips()
        {
            var catTips = db.CategoriaTips.ToList();
            catTips.Add(new CategoriaTip
            {
                CategoriaTipId = 0,
                Nombre = "[--Selecciona un Estado--]",
            });

            return catTips.OrderBy(e => e.Nombre).ToList();
        }


        public void Dispose()
        {
            db.Dispose();
        }
    }
}