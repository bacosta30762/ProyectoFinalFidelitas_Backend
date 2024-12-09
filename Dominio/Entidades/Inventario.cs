using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Inventario

    {
        public int Id { get; set; }
        public string tipo { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
       
    }
}
