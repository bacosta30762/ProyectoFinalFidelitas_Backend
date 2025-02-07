using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Usuarios.Dtos
{
    public class ReporteFinancieroDto
    {
        public decimal Ingresos { get; set; }
        public decimal Egresos { get; set; }
        public decimal Balance { get; set; }
    }
}
