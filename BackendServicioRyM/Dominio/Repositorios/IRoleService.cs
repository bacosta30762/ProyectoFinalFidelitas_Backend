namespace Dominio.Interfaces
{
    public interface IRoleService
    {
        Task CrearRolesIniciales();
        Task AsignarRolAUsuario(string userId, string roleName);
        Task<bool> EsUsuarioEnRol(string userId, string roleName);
    }
}
