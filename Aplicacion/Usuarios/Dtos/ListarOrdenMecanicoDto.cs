namespace Aplicacion.Usuarios.Dtos
{
    public record ListarOrdenMecanicoDto
    (
        int NumeroOrden,
        string Estado,
        string PlacaVehiculo,
        string NombreCliente,
        DateOnly Dia,
        int Hora,
        string NombreServicio
    );
}
