using Microsoft.AspNetCore.Identity;

namespace Dominio.Entidades
{
    public class Usuarios : IdentityUser
    {
        public string? Cedula { get; set; }
        public string? Nombre { get; set; }
        public bool Activo { get; set; }
    }
}
