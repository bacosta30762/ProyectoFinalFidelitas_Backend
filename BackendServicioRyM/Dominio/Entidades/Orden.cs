using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Orden
    {
        public int NumeroOrden { get; set; }
        public string Servicio { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;
        public string? Acciones { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string PlacaVehiculo { get; set; } = string.Empty;

        // Relación con mecánico asignado
        public string? MecanicoAsignadoId { get; set; }
        public Usuarios? MecanicoAsignado { get; set; }

        // Relación con asignación de técnicos
        public ICollection<AsignacionTecnico> AsignacionTecnicos { get; set; }
    }
}
