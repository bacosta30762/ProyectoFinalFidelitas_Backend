using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Ordenes
{
    public class ComentarioDto
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public int Puntuacion { get; set; }
        public string UsuarioId { get; set; }
        public string NombreUsuario { get; set; }  // Mostrar nombre de usuario
        public DateTime FechaCreacion { get; set; }
    }
}
