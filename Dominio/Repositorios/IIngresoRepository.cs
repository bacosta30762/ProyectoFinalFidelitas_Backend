using Dominio.Comun;
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
        Task<Ingreso> AgregarIngresoAsync(Ingreso ingreso);
        Task<List<Ingreso>> ObtenerIngresosAsync();
        Task<Ingreso?> ObtenerIngresoPorIdAsync(int id);
        Task<bool> EliminarIngresoAsync(int id);
    }
}
