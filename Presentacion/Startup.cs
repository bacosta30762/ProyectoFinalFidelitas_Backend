using Aplicacion.DataBase;
using Aplicacion.Extensiones;
using Microsoft.EntityFrameworkCore;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // Configuración de servicios
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            

        // Agregar servicios al contenedor
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddInfraestructura(Configuration);
        services.AddAplicacion();
        services.AddHttpContextAccessor();

        // Configurar CORS
        services.AddCors(options =>
        {
            options.AddPolicy("PermitirFrontend", builder =>
            {
                builder.WithOrigins("http://localhost:3000", //local
                    "http://localhost:3001", //local
                    "https://bacosta30762.github.io") //GitHub Origen del frontend
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials(); // Si estás manejando cookies o autenticación
            });
        });

        // Configuración de JSON
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
            });
    }

    // Configuración del pipeline HTTP
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("PermitirFrontend"); // Aplicar política de CORS

        // Asegúrate de poner UseRouting antes de UseEndpoints
        app.UseRouting();  

        app.UseAuthentication();  // Manejo de autenticación
        app.UseAuthorization();   // Manejo de autorización

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); 
        });

    }
}
