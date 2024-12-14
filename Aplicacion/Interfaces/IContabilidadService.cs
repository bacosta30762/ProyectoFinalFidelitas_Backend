using System;
using System.Collections.Generic;
using Dominio.Entidades;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Comun;

namespace Aplicacion.Interfaces
{
    public interface IContabilidadService
    {
        Task<Egreso> AgregarEgresoAsync(Egreso egreso);
        Task<List<Egreso>> ObtenerEgresosAsync();
        Task<Egreso> ObtenerEgresoPorIdAsync(int id);
        Task<bool> EliminarEgresoAsync(int id);

        Task<Ingreso> AgregarIngresoAsync(Ingreso ingreso);
        Task<List<Ingreso>> ObtenerIngresosAsync();
        Task<Ingreso> ObtenerIngresoPorIdAsync(int id);
        Task<bool> EliminarIngresoAsync(int id);
    }
}
