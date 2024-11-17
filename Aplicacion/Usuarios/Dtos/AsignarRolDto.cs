namespace Aplicacion.Usuarios.Dtos
{
    public record AsignarRolDto
    {
        public string Cedula { get; init; }
        public List<string> RoleNames { get; init; } = new List<string>();
    }
}
