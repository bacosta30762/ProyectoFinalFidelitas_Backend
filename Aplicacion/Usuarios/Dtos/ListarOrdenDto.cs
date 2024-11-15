namespace Aplicacion.Usuarios.Dtos
{
    public record ListarOrdenDto
    (
        int NumeroOrden,
        string Estado,
        string PlacaVehiculo,
        string NombreMecanico,
        DateOnly Dia,
        int Hora,
        string NombreServicio
    );
    
}
