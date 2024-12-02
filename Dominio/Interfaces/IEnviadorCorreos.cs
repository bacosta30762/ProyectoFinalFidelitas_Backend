using Dominio.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IEnviadorCorreos
    {
        Task EnviarNotificacionAsync(string to, Notificacion notificacion);
        Task SendEmailAsync(string to, string subject, string body);
    }
}
