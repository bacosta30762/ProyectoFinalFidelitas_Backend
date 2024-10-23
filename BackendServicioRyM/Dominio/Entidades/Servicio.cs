using System.ComponentModel.DataAnnotations;

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
