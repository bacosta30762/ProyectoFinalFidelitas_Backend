namespace Dominio.Entidades
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public int Puntuacion { get; set; }  // Puntuación de 1 a 5
        public string UsuarioId { get; set; }  // Relación con el Usuario (IdentityUser)
        public Usuario Usuario { get; set; }  // Relación con la entidad Usuario
        public DateTime FechaCreacion { get; set; }

    }
}
