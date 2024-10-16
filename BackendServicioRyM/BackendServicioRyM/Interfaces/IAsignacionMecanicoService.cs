using Dominio.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface IAsignacionMecanicoService
    {
        Task<Resultado> AsignarMecanicoAsync(int numeroOrden, string? especialidadRequerida = null);
    }
}
