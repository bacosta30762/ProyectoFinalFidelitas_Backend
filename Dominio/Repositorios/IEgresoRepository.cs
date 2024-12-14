using Dominio.Comun;
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
        Task<Egreso> AgregarEgresoAsync(Egreso egreso);
        Task<List<Egreso>> ObtenerEgresosAsync();
        Task<Egreso?> ObtenerEgresoPorIdAsync(int id);
        Task<bool> EliminarEgresoAsync(int id);
    }
}
