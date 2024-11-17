using Dominio.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.DataBase
{
    public class DatabaseContext : IdentityDbContext<Usuario>
    {
        //Creación de Tablas
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<Mecanico> Mecanicos { get; set; }
        public DbSet<Servicio> Servicios { get; set; }


        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Mecanico>()
                .HasOne(m => m.Usuario)
                .WithOne()
                .HasForeignKey<Mecanico>(m => m.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict); // Evita la eliminación en cascada
        }
    }
}
