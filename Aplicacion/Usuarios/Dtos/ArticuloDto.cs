using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Usuarios.Dtos
{
    public class ArticuloDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public int CategoriaId { get; set; }
        public string CategoriaNombre { get; set; } // Para evitar incluir la entidad entera
    }
}
