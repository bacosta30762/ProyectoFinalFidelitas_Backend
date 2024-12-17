using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Ordenes
{
    public class CrearComentarioDto
    {
        public string Texto { get; set; }
        public int Puntuacion { get; set; }  // Puntuación de 1 a 5
    }
}
