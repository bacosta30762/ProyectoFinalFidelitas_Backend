using Aplicacion.Usuarios.Dtos;
using Dominio.Comun;
using Dominio.Entidades;
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
        Task<List<ListarMecanicoDto>> ObtenerMecanicosAsync();
        Task<List<MecanicoDto>> ObtenerMecanicosDisponiblesAsync();
    }
}
