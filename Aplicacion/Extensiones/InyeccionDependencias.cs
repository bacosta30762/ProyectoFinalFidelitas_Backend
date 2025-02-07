using Aplicacion.Interfaces;
using Aplicacion.Servicios;
using Aplicacion.Usuarios;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Aplicacion.Extensiones
{
    public static class InyeccionDependencias
    {
        public static void AddAplicacion(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IUsuariosService, UsuariosService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IOrdenService, OrdenService>();
            services.AddScoped<IMecanicoService, MecanicoService>();
            services.AddScoped<IComentariosService, ComentariosService>();
            services.AddScoped<IArticuloService, ArticuloService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IEgresoService, EgresoService>();
            services.AddScoped<IIngresoService, IngresoService>();
            services.AddScoped<IReporteFinancieroService, ReporteFinancieroService>();
        }
    }
}
