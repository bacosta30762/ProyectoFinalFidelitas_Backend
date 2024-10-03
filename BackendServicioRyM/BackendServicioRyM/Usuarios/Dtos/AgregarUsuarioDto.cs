namespace Aplicacion.Usuarios.Dtos
{
    public record AgregarUsuarioDto
        (
        string Correo, 
        string Contraseña, 
        string Nombre, 
        string Apellidos, 
        string Cedula
        );
}
