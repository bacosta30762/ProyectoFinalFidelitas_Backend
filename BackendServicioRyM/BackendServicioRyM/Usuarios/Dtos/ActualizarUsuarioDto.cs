namespace Aplicacion.Usuarios.Dtos
{
    public record ActualizarUsuarioDto
    (
        string Cedula,
        string Nombre,
        string Apellidos,
        string Correo
    );
}
