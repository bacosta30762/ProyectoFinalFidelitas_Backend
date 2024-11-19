namespace Aplicacion.Usuarios.Dtos
{
    public record ListaUsuarioDto
    (
         string? Cedula,
         string? Nombre,
         string? Apellidos,
         string Email,
         string Activo
    );

}
