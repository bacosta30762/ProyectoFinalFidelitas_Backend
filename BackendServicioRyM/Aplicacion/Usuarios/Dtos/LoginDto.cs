namespace Aplicacion.Usuarios.Dtos
{
    public record LoginDto
    (
        string Email,
        string Password
    );

    public record RespuestaLoginDto
    (
      string Token  
    );
}
