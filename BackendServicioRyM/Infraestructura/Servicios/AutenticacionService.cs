using Dominio.Entidades;
using Dominio.Repositorios;
using Microsoft.AspNetCore.Identity;

namespace Infraestructura.Usuarios
{
    public class AutenticacionRepository : IAutenticacionRepository
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IJwtRepository _jwtRepository;

        public AutenticacionRepository(UserManager<Usuario> userManager, IJwtRepository jwtRepository)
        {
            _userManager = userManager;
            _jwtRepository = jwtRepository;
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var usuario = await _userManager.FindByNameAsync(username);
            if (usuario == null || !await _userManager.CheckPasswordAsync(usuario, password))
            {
                return null; 
            }

            return _jwtRepository.GenerarToken(usuario); 
        }
    }
}