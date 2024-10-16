using Microsoft.AspNetCore.Identity;

namespace Dominio.Entidades
{
    public class Usuario : IdentityUser
    {
        public string? Cedula { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public bool Activo { get; set; }

        // Propiedades específicas para el rol de mecánico
        public string? Especialidad { get; set; } = string.Empty;
        public bool EstaDisponible { get; set; } = true;

        public bool VerificarDisponibilidad() => EstaDisponible;

        public bool VerificarEspecializacion(string especialidadRequerida)
            => Especialidad != null && Especialidad.Equals(especialidadRequerida, StringComparison.OrdinalIgnoreCase);
    }
}
