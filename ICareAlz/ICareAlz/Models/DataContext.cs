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
    }
}