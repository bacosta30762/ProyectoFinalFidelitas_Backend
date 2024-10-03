using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infraestructura.Roles
{
    public class ServicioRoles : IRoleService
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

        public async Task AsignarRolAUsuario(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && !await _userManager.IsInRoleAsync(user, roleName))
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
        }

        public async Task<bool> EsUsuarioEnRol(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user != null && await _userManager.IsInRoleAsync(user, roleName);
        }
    }
}

