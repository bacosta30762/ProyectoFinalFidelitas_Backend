using Aplicacion.Usuarios.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    public class GeneradorMensajes
    {
        public static string ConfirmacionCita(CrearOrdenDto dto)
        {
            var mensaje = $@"Se ha generado su cita para el día {dto.Dia} a las {dto.Hora},
                           en caso de querer cancelar la cita o reprogramarla debe realizarlo desde la página web";

            return mensaje;
        }


    }
}
