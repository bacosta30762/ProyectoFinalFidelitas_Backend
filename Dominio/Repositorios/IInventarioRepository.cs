using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public interface IInventarioRepository
    {
        Task<IEnumerable<Inventario>> ObtenerTodosAsync();
        Task<Inventario> ObtenerIdAsync(int id);
        Task AgregarAsync(Inventario inventario);
        Task ModificarAsync(Inventario inventario);
        Task EliminarAsync(int id);
    }

}
