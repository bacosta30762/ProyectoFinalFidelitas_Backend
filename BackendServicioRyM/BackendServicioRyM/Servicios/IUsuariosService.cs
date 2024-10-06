using Aplicacion.Usuarios.Dtos;
using Dominio.Comun;

namespace Aplicacion.Servicios
{
    public interface IUsuariosService
    {
        Task<Resultado> AgregarUsuarioAsync(AgregarUsuarioDto dto);
        Task<string> LoginAsync(LoginDto loginDto);
    }
}