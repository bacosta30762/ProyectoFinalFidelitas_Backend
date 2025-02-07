var builder = WebApplication.CreateBuilder(args);

// Agregar configuración del Startup
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await RoleSeeder.SeedRolesAsync(scope.ServiceProvider);
    await ServicioSeeder.SeedServiciosAsync(scope.ServiceProvider);
    await CategoriaSeeder.SeedCategoriasAsync(scope.ServiceProvider);
}

// Configuración del pipeline
startup.Configure(app, app.Environment);

app.Run();
