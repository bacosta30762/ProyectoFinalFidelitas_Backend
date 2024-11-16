using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public interface ISuscripcionRepository
    {
        Task<Suscripcion> CrearAsync(Suscripcion suscripcion);
        Task<List<Suscripcion>> ObtenerTodasAsync();
        Task<Suscripcion> ModificarAsync(Suscripcion suscripcion);
        Task<bool> EliminarAsync(int id);
    }
}
