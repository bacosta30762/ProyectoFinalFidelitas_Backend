using Aplicacion.Interfaces;
using Aplicacion.Usuarios.Dtos;
using Dominio.Comun;
using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Repositorios;


namespace Aplicacion.Servicios
{
    public class OrdenService : IOrdenService
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly IUsuariosService _usuariosService;
        private readonly IEnviadorCorreos _enviadorCorreos;
        private readonly IUsuarioRepository _usuariosRepository;

        public OrdenService(IOrdenRepository ordenRepository, IUsuariosService usuariosService, IEnviadorCorreos enviadorCorreos, IUsuarioRepository usuariosRepository)
        {
            _ordenRepository = ordenRepository;
            _usuariosService = usuariosService;
            _enviadorCorreos = enviadorCorreos;
            _usuariosRepository = usuariosRepository;
        }

        public async Task<Resultado> CrearOrdenAsync(CrearOrdenDto dto)
        {
            //Obtener Id del Usuario logueado
            var clienteid = _usuariosService.ObtenerUsuarioIdAutenticado();
            var clientecorreo = _usuariosService.ObtenerUsuarioCorreoAutenticado();

            if (string.IsNullOrEmpty(clienteid)) // Verificar si el clienteid es nulo o vacío
            {
                return Resultado.Fallido(new[] { "El usuario no está autenticado." });
            }

            // Verificar la disponibilidad del mecánico
            var mecanicodisponible = await _ordenRepository.ObtenerMecanicoDisponibleAsync(dto.ServicioId, dto.Dia, dto.Hora);
            if (mecanicodisponible == null)
            {
                return Resultado.Fallido(new[] { "No hay mécanicos disponibles para la hora elegida" });
            }
            var orden = new Orden
            {
                Hora = dto.Hora,
                Dia = dto.Dia,
                MecanicoAsignadoId = mecanicodisponible,
                Estado = "Pendiente",
                PlacaVehiculo = dto.PlacaVehiculo,
                ClienteId = clienteid,
                ServicioId = dto.ServicioId,
            };

            var correomecanico = await _usuariosRepository.ObtenerCorreoPorIdAsync(mecanicodisponible);

            await _ordenRepository.CrearAsync(orden);

            var notificacionusuario = new Notificacion
            (
                _usuariosService.ObtenerUsuarioNombreAutenticado()??"Estimado Usuario",
                "Confirmación de la cita",
                GeneradorMensajes.ConfirmacionCitaUsuario(dto)
            );

            var notificacionmecanico = new Notificacion
            (
                await _usuariosRepository.ObtenerNombrePorIdAsync(mecanicodisponible) ?? "Estimado empleado",
                "Confirmación de servicio",
                GeneradorMensajes.ConfirmacionCitaMecanico(dto)
            );

            await _enviadorCorreos.EnviarNotificacionAsync(clientecorreo, notificacionusuario);
            await _enviadorCorreos.EnviarNotificacionAsync(correomecanico, notificacionmecanico);

            return Resultado.Exitoso();


        }

        public async Task<List<int>> ObtenerHorasDisponiblesAsync(int servicioId, DateOnly dia)
        {
            return await _ordenRepository.ObtenerHorasDisponiblesAsync(servicioId, dia);
        }

        public async Task EliminarOrdenPorIdAsync(int id)
        {
            var orden = await _ordenRepository.ObtenerOrdenPorIdAsync(id);
            if (orden == null)
            {
                throw new Exception("Orden no encontrada");
            }

            // Obtener correos del cliente y mecánico
            var clienteCorreo = await _usuariosRepository.ObtenerCorreoPorIdAsync(orden.ClienteId);
            var mecanicoCorreo = await _usuariosRepository.ObtenerCorreoPorIdAsync(orden.MecanicoAsignadoId);

            // Crear notificaciones
            var notificacionUsuario = new Notificacion
            (
                await _usuariosRepository.ObtenerNombrePorIdAsync(orden.ClienteId) ?? "Estimado usuario",
                "Cancelación de la cita",
                GeneradorMensajes.EliminarCitaUsuario(new NotificacionOrdenDto(
                     orden.Hora,
                     orden.Dia
                ))
            );

            var notificacionMecanico = new Notificacion
            (
                await _usuariosRepository.ObtenerNombrePorIdAsync(orden.MecanicoAsignadoId) ?? "Estimado empleado",
                "Cancelación de servicio",
                GeneradorMensajes.EliminarCitaMecanico(new NotificacionOrdenDto(
                    orden.Hora,
                    orden.Dia
                ))
            );

            // Enviar correos
            await _enviadorCorreos.EnviarNotificacionAsync(clienteCorreo, notificacionUsuario);
            await _enviadorCorreos.EnviarNotificacionAsync(mecanicoCorreo, notificacionMecanico);

            // Eliminar la orden
            await _ordenRepository.EliminarAsync(orden);
        }

        public async Task<Resultado> AsignarMecanicoAsync(int numeroOrden, string mecanicoId)
        {
            var orden = await _ordenRepository.ObtenerPorNumeroAsync(numeroOrden);
            if (orden == null)
            {
                return Resultado.Fallido(new[] { "Orden no encontrada." });
            }

            orden.MecanicoAsignadoId = mecanicoId;
            await _ordenRepository.ActualizarAsync(orden);

            return Resultado.Exitoso();
        }

        public async Task<Resultado<List<ListarOrdenDto>>> listarOrdenesUsuarioAsync(string usuarioId)
        {
            var ordenes = await _ordenRepository.ObtenerOrdenesPorClienteId(usuarioId);

            if (ordenes == null || !ordenes.Any())
            {
                return Resultado<List<ListarOrdenDto>>.Fallido(new List<string> { "No se encontraron órdenes para el usuario." });
            }

            var ordenesDto = ordenes.Select(o => new ListarOrdenDto(
                o.NumeroOrden,
                o.Estado,
                o.PlacaVehiculo,
                o.MecanicoAsignado?.Usuario?.Nombre ?? "N/A",
                o.Cliente?.Nombre ?? "N/A",
                o.Dia,
                o.Hora,
                o.Servicio?.Descripcion ?? "N/A"
            )).ToList();

            return Resultado<List<ListarOrdenDto>>.Exitoso(ordenesDto);
        }

        public async Task<Resultado<List<ListarOrdenDto>>> ListarTodasLasOrdenesAsync()
        {
            var ordenes = await _ordenRepository.ObtenerTodasLasOrdenes();

            if (ordenes == null || !ordenes.Any())
            {
                return Resultado<List<ListarOrdenDto>>.Fallido(new List<string> { "No se encontraron órdenes en el sistema." });
            }

            var ordenesDto = ordenes.Select(o => new ListarOrdenDto(
                o.NumeroOrden,
                o.Estado,
                o.PlacaVehiculo,
                o.MecanicoAsignado?.Usuario?.Nombre ?? "N/A",
                o.Cliente?.Nombre ?? "N/A",
                o.Dia,
                o.Hora,
                o.Servicio?.Descripcion ?? "N/A"
            )).ToList();

            return Resultado<List<ListarOrdenDto>>.Exitoso(ordenesDto);
        }

        public async Task<Resultado<List<ListarOrdenMecanicoDto>>> ListarOrdenesPorMecanicoAsync(string mecanicoId)
        {
            var ordenes = await _ordenRepository.ObtenerOrdenesPorMecanicoId(mecanicoId);

            if (ordenes == null || !ordenes.Any())
            {
                return Resultado<List<ListarOrdenMecanicoDto>>.Fallido(new List<string> { "No se encontraron órdenes asignadas al mecánico." });
            }

            var ordenesDto = ordenes.Select(o => new ListarOrdenMecanicoDto(
                o.NumeroOrden,
                o.Estado,
                o.PlacaVehiculo,
                $"{o.Cliente.Nombre} {o.Cliente.Apellidos}",  
                o.Dia,
                o.Hora,
                o.Servicio?.Descripcion ?? "N/A"
            )).ToList();

            return Resultado<List<ListarOrdenMecanicoDto>>.Exitoso(ordenesDto);
        }

        public async Task BloquearDiaAsync(BloquearDiaDto dto)
        {
            if (!DateOnly.TryParse(dto.Dia, out var dia))
            {
                throw new ArgumentException("Formato de día inválido. Use 'yyyy-MM-dd'.");
            }

            await _ordenRepository.BloquearDiaAsync(dia);
        }

        public async Task DesbloquearDiaAsync(BloquearDiaDto dto)
        {
            if (!DateOnly.TryParse(dto.Dia, out var dia))
            {
                throw new ArgumentException("Formato de día inválido. Use 'yyyy-MM-dd'.");
            }

            await _ordenRepository.DesbloquearDiaAsync(dia);
        }

        public async Task<Resultado> CrearOrdenPorAdminAsync(CrearOrdenAdminDto dto)
        {
            // Verificar si el ClienteId proporcionado es válido
            var usuarioExistente = await _usuariosRepository.ObtenerPorIdAsync(dto.ClienteId);
            if (usuarioExistente == null)
            {
                return Resultado.Fallido(new[] { "El usuario seleccionado no existe." });
            }

            // Verificar la disponibilidad del mecánico
            var mecanicoDisponible = await _ordenRepository.ObtenerMecanicoDisponibleAsync(dto.ServicioId, dto.Dia, dto.Hora);
            if (mecanicoDisponible == null)
            {
                return Resultado.Fallido(new[] { "No hay mecánicos disponibles para la hora elegida." });
            }

            // Crear la orden
            var orden = new Orden
            {
                Hora = dto.Hora,
                Dia = dto.Dia,
                MecanicoAsignadoId = mecanicoDisponible,
                Estado = "Pendiente",
                PlacaVehiculo = dto.PlacaVehiculo,
                ClienteId = dto.ClienteId,
                ServicioId = dto.ServicioId,
            };

            var correoMecanico = await _usuariosRepository.ObtenerCorreoPorIdAsync(mecanicoDisponible);

            await _ordenRepository.CrearAsync(orden);

            // Generar notificación para el usuario seleccionado
            var notificacionUsuario = new Notificacion
            (
                usuarioExistente.Nombre ?? "Estimado Usuario",
                "Confirmación de la cita",
                GeneradorMensajes.ConfirmacionCitaUsuario(new CrearOrdenDto(
                    dto.ServicioId,
                    dto.PlacaVehiculo,
                    dto.Hora,
                    dto.Dia
                ))
            );

            // Generar notificación para el mecánico asignado
            var notificacionMecanico = new Notificacion
            (
                await _usuariosRepository.ObtenerNombrePorIdAsync(mecanicoDisponible) ?? "Estimado empleado",
                "Confirmación de servicio",
                GeneradorMensajes.ConfirmacionCitaMecanico(new CrearOrdenDto(
                    dto.ServicioId,
                    dto.PlacaVehiculo,
                    dto.Hora,
                    dto.Dia
                ))
            );

            // Enviar correos
            await _enviadorCorreos.EnviarNotificacionAsync(usuarioExistente.Email, notificacionUsuario);
            await _enviadorCorreos.EnviarNotificacionAsync(correoMecanico, notificacionMecanico);

            return Resultado.Exitoso();
        }

    }
}
