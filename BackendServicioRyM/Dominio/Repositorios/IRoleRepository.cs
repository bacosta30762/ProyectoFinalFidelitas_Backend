namespace Dominio.Interfaces
{
    public interface IRoleRepository
    {
        Task CrearRolesIniciales();
        Task AsignarRolAUsuario(string userId, string roleName);
        Task<bool> EsUsuarioEnRol(string userId, string roleName);
    }
}
