using Dominio.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface IMecanicoService
    {
        Task<Resultado> AsignarServiciosAsync(string usuarioId, List<int> servicioIds);
    }
}
