using Dominio.Comun;
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

        private const string Plantilla = @"

                <!DOCTYPE html>
                <html lang=""es"">
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <title>@Model.Asunto</title>
                    <style>
                        /* General styles for Material-like look */
                        body {
                            font-family: Arial, sans-serif;
                            background-color: #f4f4f4;
                            margin: 0;
                            padding: 0;
                            color: #333;
                        }

                        .container {
                            width: 100%;
                            max-width: 600px;
                            margin: 20px auto;
                            background-color: #ffffff;
                            border-radius: 8px;
                            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
                            overflow: hidden;
                        }

                        .header {
                            background-color: #1976d2;
                            color: white;
                            padding: 20px;
                            text-align: center;
                        }

                        .header h1 {
                            font-size: 24px;
                            margin: 0;
                        }

                        .content {
                            padding: 20px;
                        }

                        .content h2 {
                            color: #333;
                            font-size: 20px;
                            margin-top: 0;
                        }

                        .content p {
                            font-size: 16px;
                            line-height: 1.6;
                            margin: 0 0 20px;
                        }

                        .footer {
                            padding: 20px;
                            background-color: #eeeeee;
                            text-align: center;
                            font-size: 14px;
                            color: #666;
                        }

                        /* Button hover effect for email clients that support it */
                        .btn:hover {
                            background-color: #1565c0;
                        }
                    </style>
                </head>
                <body>
                    <div class=""container"">
                        <!-- Header Section -->
                        <div class=""header"">
                            <h1>@Model.Asunto</h1>
                        </div>

                        <!-- Content Section -->
                        <div class=""content"">
                            <h2>Hola, @Model.Destinatario</h2>
                            <p>@Model.Mensaje</p>
                        </div>

                        <!-- Footer Section -->
                        <div class=""footer"">
                            <p>Si tienes alguna pregunta, no dudes en contactar a nuestro equipo de soporte.</p>
                            <p>¡Gracias!</p>
                        </div>
                    </div>
                </body>
                </html>
            ";

        public string ObtenerPlantilla(Notificacion notificacion)
        {
            var contenido = Plantilla.Replace("@Model.Destinatario", notificacion.Destinatario)
                .Replace("@Model.Mensaje", notificacion.Mensaje)
                .Replace("@Model.Asunto", notificacion.Asunto);
            
            return contenido;
        }
    }
}
