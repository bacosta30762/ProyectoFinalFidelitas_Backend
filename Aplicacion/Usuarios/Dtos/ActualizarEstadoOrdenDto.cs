using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Usuarios.Dtos
{
    public class ActualizarEstadoOrdenDto
    {
        public int NumeroOrden { get; set; }
        public string Estado { get; set; } // "En Ejecución" o "Finalizada"
    }
}
