
using System.ComponentModel.DataAnnotations;


namespace Dominio.Entidades
{
    public class Suscripcion
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaSuscripcion { get; set; }
        public bool Activo { get; set; }
    }
}