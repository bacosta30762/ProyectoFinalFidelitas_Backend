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
        public DbSet<Boletin> Boletines { get; set; }
        public DbSet<Resena> Resenas { get; set; }
        public DbSet<Suscripcion> Suscripciones { get; set; }
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
            base.OnModelCreating(modelBuilder);

            // Configuración de la relación muchos a muchos entre Mecanicos y Servicios
            modelBuilder.Entity<Mecanico>()
                .HasMany(m => m.Servicios)
                .WithMany(s => s.Mecanicos)
                .UsingEntity<Dictionary<string, object>>(
                    "MecanicoServicio",
                    mec => mec.HasOne<Servicio>().WithMany().HasForeignKey("ServiciosId"),
                    srv => srv.HasOne<Mecanico>().WithMany().HasForeignKey("MecanicosUsuarioId")
                );

            modelBuilder.Entity<Orden>()
                 .HasOne(o => o.MecanicoAsignado)
                 .WithMany()
                 .HasForeignKey(o => o.MecanicoAsignadoId)
                 .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
