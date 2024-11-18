using Dominio.Comun;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface IMarketingService
    {
        Task<Boletin> CrearBoletinAsync(Boletin boletin);
        Task<List<Boletin>> ObtenerBoletinesAsync();
        Task<Boletin> ModificarBoletinAsync(Boletin boletin);
        Task<bool> EliminarBoletinAsync(int id);

        Task<Resena> CrearResenaAsync(Resena resena);
        Task<List<Resena>> ObtenerResenasAsync();
        Task<Resena> ModificarResenaAsync(Resena resena);
        Task<bool> EliminarResenaAsync(int id);

        Task<Suscripcion> CrearSuscripcionAsync(Suscripcion suscripcion);
        Task<List<Suscripcion>> ObtenerSuscripcionesAsync();
        Task<Suscripcion> ModificarSuscripcionAsync(Suscripcion suscripcion);
        Task<bool> EliminarSuscripcionAsync(int id);
    }
}
