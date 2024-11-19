using Aplicacion.DataBase;
using Dominio.Comun;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Roles
{
    public class ServicioRoles : IRoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly DatabaseContext _context;

        public ServicioRoles(RoleManager<IdentityRole> roleManager, UserManager<Usuario> userManager, DatabaseContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
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

        // Obtener roles asignados a un usuario
        public async Task<List<string>> ObtenerRolesUsuarioAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new List<string>();
            }

            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        // Eliminar un rol asignado a un usuario
        public async Task<Resultado> EliminarRolAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Resultado.Fallido(new[] { "Usuario no encontrado" });
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            return result.Succeeded ? Resultado.Exitoso() : Resultado.Fallido(result.Errors.Select(e => e.Description));
        }

        // Listar todos los roles existentes
        public async Task<List<string>> ListarRolesAsync()
        {
            return await _context.Roles
                .Select(r => r.Name)
                .ToListAsync();
        }
    }
}

