using Dominio.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.DataBase
{
    public class DatabaseContext : IdentityDbContext<Usuario>
    {
        //Creación de Tablas
        public DbSet<Usuario> Usuarios { get; set; }
        
        /*public DbSet<Orden> Ordenes { get; set; }
        public DbSet<AsignacionTecnico> AsignacionTecnicos { get; set; }*/

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {

        }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<Orden>().HasKey(o => o.NumeroOrden);
            modelBuilder.Entity<AsignacionTecnico>().HasKey(at => new { at.Nombre, at.Especialidad, at.NumeroOrden });

            modelBuilder.Entity<AsignacionTecnico>()
                .HasOne(at => at.Orden)
                .WithMany(o => o.AsignacionTecnicos)
                .HasForeignKey(at => at.NumeroOrden);
        }*/
    }
}
