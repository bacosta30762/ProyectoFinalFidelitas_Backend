using Aplicacion.Usuarios.Dtos;
using Dominio.Comun;

namespace Aplicacion.Interfaces
{
    public interface IUsuariosService
    {
        Task<Resultado> AgregarUsuarioAsync(AgregarUsuarioDto dto);
        Task<Resultado<RespuestaLoginDto>> LoginAsync(LoginDto loginDto);
    }
}