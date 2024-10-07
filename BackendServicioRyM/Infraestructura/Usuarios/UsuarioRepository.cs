using Dominio.Comun;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Usuarios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsuarioRepository(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Resultado> RegistrarUsuarioAsync(Usuario usuario, string password)
        {
            var result = await _userManager.CreateAsync(usuario, password);

            if (result.Succeeded)
            {
               
                if (!await _roleManager.RoleExistsAsync("Usuario"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Usuario"));
                }

                await _userManager.AddToRoleAsync(usuario, "Usuario");

                return Resultado.Exitoso();
            }

            return Resultado.Fallido(result.Errors.Select(e => e.Description));
        }

        public async Task<Resultado<Usuario>> AutenticarUsuarioAsync(string correo, string password)
        {
            var usuario = await _userManager.FindByNameAsync(correo);
            if (usuario == null || !await _userManager.CheckPasswordAsync(usuario, password))
            {
                return Resultado<Usuario>.Fallido(["Correo y contraseña incorrectos"]);
            }

            return Resultado<Usuario>.Exitoso(usuario);
        }

        public async Task<IEnumerable<string>> ObtenerRolesAsync(Usuario usuario)
        {
            var roles = await _userManager.GetRolesAsync(usuario);
          
            return roles;
        }

        public async Task<Usuario?> ObtenerPorCedulaAsync(string cedula)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.Cedula == cedula);
        }

        public async Task<Resultado> ActualizarAsync(Usuario usuario)
        {
            var result = await _userManager.UpdateAsync(usuario);
            return result.Succeeded ? Resultado.Exitoso() : Resultado.Fallido(result.Errors.Select(e => e.Description));
        }

        public async Task<Resultado> EliminarAsync(Usuario usuario)
        {
            var result = await _userManager.DeleteAsync(usuario);
            return result.Succeeded ? Resultado.Exitoso() : Resultado.Fallido(result.Errors.Select(e => e.Description));
        }

        public async Task<Resultado> AsignarRolAsync(string cedula, string roleName)
        {
            var usuario = await ObtenerPorCedulaAsync(cedula);
            if (usuario == null) return Resultado.Fallido(new[] { "Usuario no encontrado" });

            var result = await _userManager.AddToRoleAsync(usuario, roleName);
            return result.Succeeded ? Resultado.Exitoso() : Resultado.Fallido(result.Errors.Select(e => e.Description));
        }


    }
}
