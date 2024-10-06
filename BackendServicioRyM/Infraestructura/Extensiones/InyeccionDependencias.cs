using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Repositorios;
using Infraestructura.DataBase;
using Infraestructura.Usuarios;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructura.Extensiones
{
    public static class InyeccionDependencias
    {
        public static void AddInfraestructura(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(config => config.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentityCore<Usuario>().AddRoles<IdentityRole>().AddEntityFrameworkStores<DatabaseContext>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IAutenticacionRepository, AutenticacionRepository>(); 
            services.AddScoped<IJwtRepository, JwtService>();
        }
    }
}
