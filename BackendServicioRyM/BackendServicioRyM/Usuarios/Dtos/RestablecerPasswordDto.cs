using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Usuarios.Dtos
{
    public record RestablecerPasswordDto
    (
        string Correo,
        string Token,
        string NuevaPassword
    );
}
