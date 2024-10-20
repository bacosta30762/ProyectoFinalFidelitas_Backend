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
    }
}
