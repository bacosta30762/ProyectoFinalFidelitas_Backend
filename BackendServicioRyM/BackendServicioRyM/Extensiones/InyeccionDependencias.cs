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
        }
    }
}
