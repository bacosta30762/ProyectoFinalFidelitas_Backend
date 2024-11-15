using Dominio.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Infraestructura.Servicios
{
    public class EnviadorCorreos : IEnviadorCorreos

    {
        private readonly IConfiguration _config;

        public EnviadorCorreos(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(to));
            message.From = new MailAddress(_config["Smtp:Direcion"]);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var client = new SmtpClient(_config["Smtp:Servidor"], int.Parse(_config["Smtp:Puerto"])))
            {
                client.Credentials = new NetworkCredential(_config["Smtp:Usuario"], _config["Smtp:Password"]);
                client.EnableSsl = true; 
                await client.SendMailAsync(message);
            }
        }
    }
}



