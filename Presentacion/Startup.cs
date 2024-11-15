using Aplicacion.Extensiones;

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

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();
    }
}
