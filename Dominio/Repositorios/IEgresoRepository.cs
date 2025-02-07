using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public interface IEgresoRepository
    {
        Task<bool> Actualizar(Egreso egreso);
        Task<Egreso> Agregar(Egreso egreso);
        Task<bool> Eliminar(int id);
        Task<Egreso> ObtenerPorId(int id);
        Task<List<Egreso>> ObtenerTodos();
    }
}
