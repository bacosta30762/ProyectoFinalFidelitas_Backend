using Dominio.Comun;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Roles
{
    public class ServicioRoles : IRoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Usuario> _userManager;

        public ServicioRoles(RoleManager<IdentityRole> roleManager, UserManager<Usuario> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task CrearRolesIniciales()
        {
            string[] roles = { "Admin", "Mecanico", "Contador", "Usuario" };
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public async Task<Resultado> AsignarRolAUsuario(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && !await _userManager.IsInRoleAsync(user, roleName))
            {
                var result = await _userManager.AddToRoleAsync(user, roleName);
                if (result.Succeeded)
                {
                    return Resultado.Exitoso();
                }
                else
                {
                    return Resultado.Fallido(result.Errors.Select(e => e.Description));
                }
            }

            return Resultado.Fallido(new[] { "Usuario no encontrado o ya tiene asignado el rol." });
        }

        public async Task<bool> EsUsuarioEnRol(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user != null && await _userManager.IsInRoleAsync(user, roleName);
        }
    }
}

