using Aplicacion.Extensiones;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfraestructura(builder.Configuration);
builder.Services.AddAplicacion();
builder.Services.AddHttpContextAccessor();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", builder =>
    {
        builder.WithOrigins("http://localhost:3000", //local
            "http://localhost:3001", //local
            "https://bacosta30762.github.io") //Github Origen del frontend
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials(); // Si estás manejando cookies o autenticación
    });
});


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await RoleSeeder.SeedRolesAsync(scope.ServiceProvider);
    await ServicioSeeder.SeedServiciosAsync(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PermitirFrontend"); // Aplicar política de CORS

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
