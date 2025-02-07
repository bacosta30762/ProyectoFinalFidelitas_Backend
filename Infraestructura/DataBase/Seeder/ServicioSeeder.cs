using Aplicacion.DataBase;
using Dominio.Entidades;
using Microsoft.Extensions.DependencyInjection;

public static class ServicioSeeder
{
    public static async Task SeedServiciosAsync(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<DatabaseContext>(); 

        if (!dbContext.Servicios.Any())
        {
            var servicios = new List<Servicio>
            {
                new Servicio { Descripcion = "Cambio de aceite" },
                new Servicio { Descripcion = "Mecánica rápida" },
                new Servicio { Descripcion = "Revisión y cambio de llantas" }
            };

            dbContext.Servicios.AddRange(servicios);
            await dbContext.SaveChangesAsync();
        }
    }
}