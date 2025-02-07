using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Usuarios.Dtos
{
    public class CategoriaArticulosDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<string> Articulos { get; set; } = new();
    }
}
