using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    public class Mecanico
    {
        [Key]
        public string UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        public ICollection<Servicio> Servicios { get; set; }
        public ICollection<Orden> Ordenes { get; set; }

    }
}
