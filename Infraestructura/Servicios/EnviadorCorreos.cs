using Dominio.Comun;
using Dominio.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MimeKit;

namespace Infraestructura.Servicios
{
    public class EnviadorCorreos : IEnviadorCorreos

    {
        private readonly IConfiguration _config;
        private readonly IMotorDePlantillas _motorDePlantillas;

        public EnviadorCorreos(IConfiguration config, IMotorDePlantillas motorDePlantillas)
        {
            _config = config;
            _motorDePlantillas = motorDePlantillas;
        }


        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Servicentro RyM", _config["Smtp:Direcion"]));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;
            message.Body = new TextPart("html")
            {
                Text = body
            };
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_config["Smtp:Servidor"], int.Parse(_config["Smtp:Puerto"]), MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_config["Smtp:Usuario"], _config["Smtp:Password"]);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        public async Task EnviarNotificacionAsync(string to, Notificacion notificacion)
        {
            //var body = await _motorDePlantillas.RenderizarPlantillaAsync("Notificacion", notificacion);

            await SendEmailAsync(to, notificacion.Asunto, notificacion.Mensaje);
             
        }
    }

}




