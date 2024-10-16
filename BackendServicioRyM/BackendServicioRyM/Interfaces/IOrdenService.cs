using Aplicacion.Usuarios.Dtos;
using Dominio.Comun;
using Dominio.Entidades;

namespace Aplicacion.Interfaces
{
    public interface IOrdenService
    {
        Task<Resultado<Orden>> CrearOrdenAsync(CrearOrdenDto dto);
    }
}
