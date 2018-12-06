namespace ICareAlz.Models
{

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using System.Web;

    public class DataContext : DbContext
    {


        public DataContext() : base("DefaultConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<Estado> Estadoes { get; set; }

        public DbSet<Municipio> Municipios { get; set; }

        public DbSet<Localidad> Localidades { get; set; }

        public DbSet<Administrador> Administradors { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Solicitud> Solicituds { get; set; }

        public DbSet<Instituto> Institutos { get; set; }

        public DbSet<Pregunta> Preguntas { get; set; }

        public DbSet<Respuesta> Respuestas { get; set; }

        public DbSet<Test> Tests { get; set; }

        public System.Data.Entity.DbSet<ICareAlz.Models.CategoriaTip> CategoriaTips { get; set; }

        public System.Data.Entity.DbSet<ICareAlz.Models.Tip> Tips { get; set; }

        public System.Data.Entity.DbSet<ICareAlz.Models.Usuario> Usuarios { get; set; }

        public System.Data.Entity.DbSet<ICareAlz.Models.Dieta> Dietas { get; set; }
    }
}