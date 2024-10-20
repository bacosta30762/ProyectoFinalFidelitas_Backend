namespace Aplicacion.Usuarios.Dtos
{
    public record CrearOrdenDto
    (
        int ServicioId,
        string PlacaVehiculo,
        int Hora,
        DateOnly Dia
    );
    
}
