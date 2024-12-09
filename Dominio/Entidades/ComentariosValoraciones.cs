using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class ComentariosValoraciones
    {
        public DateTime fecha { get; set; }
        public string cliente { get; set; }
        public int valoracion { get; set; }
        public int orden {  get; set; }
    }
}
