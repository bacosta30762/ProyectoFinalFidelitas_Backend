using Dominio.Interfaces;
using RazorEngine;
using RazorEngine.Templating;

namespace Infraestructura.Servicios
{
    public class MotorDePlantillas : IMotorDePlantillas
    {
        public async Task<string> RenderizarPlantillaAsync<T>(string templateName, T model)
        {
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "PlantillasCorreo", $"{templateName}.cshtml");
            var templateContent = await File.ReadAllTextAsync(templatePath);
            var type = typeof(T);
            return Engine.Razor.RunCompile(templateContent, templateName, type, model);
        }
    }
}
