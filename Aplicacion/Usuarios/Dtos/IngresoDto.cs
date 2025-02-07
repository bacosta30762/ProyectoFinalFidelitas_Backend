namespace Aplicacion.Usuarios.Dtos
{
    public class IngresoDto
    {
        public int Id { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string MetodoPago { get; set; }
        public string NumeroFactura { get; set; }
    }
}
