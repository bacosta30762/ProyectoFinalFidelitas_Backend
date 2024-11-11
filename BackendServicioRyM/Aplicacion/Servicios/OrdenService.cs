using Aplicacion.Interfaces;
using Aplicacion.Usuarios.Dtos;
using Dominio.Comun;
using Dominio.Entidades;
using Dominio.Repositorios;

namespace Aplicacion.Servicios
{
    public class OrdenService : IOrdenService
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly IUsuariosService _usuariosService;

        public OrdenService(IOrdenRepository ordenRepository, IUsuariosService usuariosService)
        {
            _ordenRepository = ordenRepository;
            _usuariosService = usuariosService;
        }

        public async Task<Resultado> CrearOrdenAsync(CrearOrdenDto dto)
        {
            //Obtener Id del Usuario logueado
            var clienteid = _usuariosService.ObtenerUsuarioIdAutenticado();

            if (string.IsNullOrEmpty(clienteid)) // Verificar si el clienteid es nulo o vacío
            {
                return Resultado.Fallido(new[] { "El usuario no está autenticado." });
            }

            var mecanicodisponible = await _ordenRepository.ObtenerMecanicoDisponibleAsync(dto.ServicioId, dto.Dia, dto.Hora);
            if (mecanicodisponible == null)
            {
                return Resultado.Fallido(new[] { "No hay mécanicos disponibles para la hora elegida" });
            }
            var orden = new Orden
            {
                Hora = dto.Hora,
                Dia = dto.Dia,
                MecanicoAsignadoId = mecanicodisponible.UsuarioId,
                Estado = "Pendiente",
                PlacaVehiculo = dto.PlacaVehiculo,
                ClienteId = clienteid,
                ServicioId = dto.ServicioId,
            };

            await _ordenRepository.CrearAsync(orden);

            return Resultado.Exitoso();


        }

        public async Task<List<int>> ObtenerHorasDisponiblesAsync(int servicioId, DateOnly dia)
        {
            return await _ordenRepository.ObtenerHorasDisponiblesAsync(servicioId, dia);
        }

        public async Task EliminarOrdenPorIdAsync(int id)
        {
            var orden = await _ordenRepository.ObtenerOrdenPorIdAsync(id);
            if (orden != null)
            {
                await _ordenRepository.EliminarAsync(orden);
            }
            else
            {
                throw new Exception("Orden no encontrada");
            }
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
                o.Dia,
                o.Hora,
                o.Servicio?.Descripcion ?? "N/A"
            )).ToList();

            return Resultado<List<ListarOrdenDto>>.Exitoso(ordenesDto);
        }
    }
}
