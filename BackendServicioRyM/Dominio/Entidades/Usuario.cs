using Microsoft.AspNetCore.Identity;

namespace Dominio.Entidades
{
    public class Usuario : IdentityUser
    {
        public string? Cedula { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public bool Activo { get; set; }
    }
}
