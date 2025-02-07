using Aplicacion.DataBase;
using Dominio.Entidades;
using Microsoft.Extensions.DependencyInjection;

public static class CategoriaSeeder
{
    public static async Task SeedCategoriasAsync(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<DatabaseContext>();

        // Verificar si ya existen categorías antes de insertarlas
        if (!dbContext.Categorias.Any())
        {
            var categorias = new List<Categoria>
            {
                new Categoria { Nombre = "Lubricantes" },
                new Categoria { Nombre = "Filtros" },
                new Categoria { Nombre = "Repuestos y Componentes" }
            };

            dbContext.Categorias.AddRange(categorias);
            await dbContext.SaveChangesAsync();
        }
    }
}