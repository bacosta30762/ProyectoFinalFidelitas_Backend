namespace Aplicacion.Usuarios.Dtos
{
    public class ActualizarArticuloDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public int CategoriaId { get; set; }
    }
}
