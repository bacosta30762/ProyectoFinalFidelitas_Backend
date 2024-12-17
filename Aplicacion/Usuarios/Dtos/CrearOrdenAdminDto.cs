using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Usuarios.Dtos
{
    public record CrearOrdenAdminDto
    (
        string ClienteId,
        int ServicioId,
        string PlacaVehiculo,
        int Hora,
        DateOnly Dia
    );
}
