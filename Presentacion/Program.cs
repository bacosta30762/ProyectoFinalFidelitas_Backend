var builder = WebApplication.CreateBuilder(args);

// Configurar el host para escuchar"
builder.WebHost.UseUrls("http://0.0.0.0:8080", "http://0.0.0.0:80");

// Agregar configuración del Startup
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await RoleSeeder.SeedRolesAsync(scope.ServiceProvider);
    await ServicioSeeder.SeedServiciosAsync(scope.ServiceProvider);
}

// Configuración del pipeline
startup.Configure(app, app.Environment);

app.Run();
