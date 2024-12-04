using Aplicacion.Usuarios.Dtos;

namespace Aplicacion.Servicios
{
    public class GeneradorMensajes
    {
        public static string ConfirmacionCitaUsuario(CrearOrdenDto dto)
        {
            var mensaje = $@"Se ha generado su cita para el día {dto.Dia} a las {dto.Hora},
                           en caso de querer cancelar la cita o reprogramarla debe realizarlo desde la página web";

            return mensaje;
        }

        public static string ConfirmacionCitaMecanico(CrearOrdenDto dto)
        {
            var mensaje = $@"Se ha generado un servicio para el día {dto.Dia} a las {dto.Hora}";

            return mensaje;
        }

        public static string EliminarCitaUsuario(NotificacionOrdenDto dto)
        {
            var mensaje = $@"Se ha cancelado su cita para el día {dto.Dia} a las {dto.Hora},
                           en caso de querer agendar una nueva cita debe realizarlo desde la página web";

            return mensaje;
        }

        public static string EliminarCitaMecanico(NotificacionOrdenDto dto)
        {
            var mensaje = $@"Se ha cancelado el servicio programado para el día {dto.Dia} a las {dto.Hora}";

            return mensaje;
        }

        public static string RecuperarPassword(string token)
        {
            var mensaje = $@"Su token de recuperación es : 
                            {string.Join(Environment.NewLine, token.ToCharArray().Select(c => c.ToString()))}";

            return mensaje;
        }


    }
}
