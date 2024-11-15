using Dominio.Comun;

namespace Dominio.Interfaces
{
    public interface IRoleRepository
    {
        Task CrearRolesIniciales();
        Task<Resultado> AsignarRolAUsuario(string userId, string roleName);
        Task<bool> EsUsuarioEnRol(string userId, string roleName);
    }
}
