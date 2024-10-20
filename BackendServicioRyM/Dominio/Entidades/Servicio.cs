using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Servicio
    {
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public ICollection<Mecanico> Mecanicos { get; set; }
    }
}
