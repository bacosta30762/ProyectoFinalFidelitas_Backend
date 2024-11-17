using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public class Orden
    {
        [Key]
        public int NumeroOrden { get; set; }
        public int ServicioId { get; set; }
        public string ClienteId { get; set; }
        public string Estado { get; set; } 
        public string PlacaVehiculo { get; set; }
        public int Hora { get; set; }
        public DateOnly Dia { get; set; }

        //Relación con Usuario
        public Usuario Cliente { get; set; }

        //Relación con Servicio
        public Servicio Servicio { get; set; }

        // Relación con el mecánico asignado
        public string MecanicoAsignadoId { get; set; }
        public Mecanico MecanicoAsignado { get; set; }

    }
}
