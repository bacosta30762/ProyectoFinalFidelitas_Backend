using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public class Mecanico
    {
        [Key]
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public ICollection<Servicio> Servicios { get; set; }
        public ICollection<Orden> Ordenes { get; set; }

    }
}
