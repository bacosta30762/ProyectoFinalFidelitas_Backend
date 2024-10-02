using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class AsignacionTecnico
    {
        public string Nombre { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;

        // Llave foránea para la relación con Orden
        public int NumeroOrden { get; set; }
        public Orden Orden { get; set; }
    }
}
