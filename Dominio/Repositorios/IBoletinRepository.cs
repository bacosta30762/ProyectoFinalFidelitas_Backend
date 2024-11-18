using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public interface IBoletinRepository
    {
        Task<Boletin> CrearAsync(Boletin boletin);
        Task<List<Boletin>> ObtenerTodosAsync();
        Task<Boletin> ModificarAsync(Boletin boletin);
        Task<bool> EliminarAsync(int id);
    }
}
