using Dominio.Comun;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infraestructura.Usuarios
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
            usuario.Activo = true;

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

        
    }
}
