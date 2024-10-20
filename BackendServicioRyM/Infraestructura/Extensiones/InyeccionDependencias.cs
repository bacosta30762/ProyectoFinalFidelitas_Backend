using Aplicacion.DataBase;
using Aplicacion.Roles;
using Aplicacion.Usuarios;
using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Repositorios;
using Infraestructura.Mecanicos;
using Infraestructura.Ordenes;
using Infraestructura.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Aplicacion.Extensiones
{
    public static class InyeccionDependencias
    {
        public static void AddInfraestructura(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(config => config.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentityCore<Usuario>().AddRoles<IdentityRole>().AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IRoleRepository, ServicioRoles>();
            services.AddScoped<IEnviadorCorreos, EnviadorCorreos>();
            services.AddScoped<IOrdenRepository, OrdenRepository>();
            services.AddScoped<IMecanicoRepository, MecanicoRepository>();

            //services.AddIdentityCore<Usuario, IdentityRole>().AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]))
                };
            });
        }
    }
}
