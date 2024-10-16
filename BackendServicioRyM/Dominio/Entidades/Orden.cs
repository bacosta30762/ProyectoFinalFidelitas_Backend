using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public class Orden
    {
        [Key]
        public int NumeroOrden { get; set; }
        public string Servicio { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;
        public string? Acciones { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string PlacaVehiculo { get; set; } = string.Empty;

        // Relación con el mecánico asignado
        public string? MecanicoAsignadoId { get; set; }
        public Usuario? MecanicoAsignado { get; set; }
    }
}
