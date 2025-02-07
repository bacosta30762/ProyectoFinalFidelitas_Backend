using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public interface IIngresoRepository
    {
        Task<bool> Actualizar(Ingreso ingreso);
        Task<Ingreso> Agregar(Ingreso ingreso);
        Task<bool> Eliminar(int id);
        Task<Ingreso> ObtenerPorId(int id);
        Task<List<Ingreso>> ObtenerTodos();
    }
}
