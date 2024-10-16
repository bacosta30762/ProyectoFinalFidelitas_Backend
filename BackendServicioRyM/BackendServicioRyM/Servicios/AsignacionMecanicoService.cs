using Aplicacion.Interfaces;
using Dominio.Comun;
using Dominio.Interfaces;
using Dominio.Repositorios;

namespace Aplicacion.Servicios
{
    public class AsignacionMecanicoService : IAsignacionMecanicoService
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly IUsuarioRepository _userRepository;

        public AsignacionMecanicoService(IOrdenRepository ordenRepository, IUsuarioRepository userRepository)
        {
            _ordenRepository = ordenRepository;
            _userRepository = userRepository;
        }

        public async Task<Resultado> AsignarMecanicoAsync (int numeroOrden, string? especialidadRequerida = null)
        {
            try
            {
                var orden = await _ordenRepository.ObtenerPorNumeroAsync(numeroOrden);
                if (orden == null)
                {
                    return Resultado.Fallido(new[] { "Orden no encontrada." });
                }

                if(orden.MecanicoAsignadoId != null)
                {
                    return Resultado.Fallido(new[] { "La orden ya tiene mecánico asignado" });
                }

                //Obtener mecánicos disponibles
                var mecanicosDisponibles = await _userRepository.ObtenerUsuariosPorRolAsync("Mecanico");
                var mecanisoAsignado = mecanicosDisponibles
                    .Where(m => m.EstaDisponible)
                    .Where(m => especialidadRequerida == null || m.VerificarEspecializacion(especialidadRequerida))
                    .FirstOrDefault();

                if(mecanisoAsignado == null)
                {
                    return Resultado.Fallido(new[] { "Nohay mecánicos disponibles con la especialización requerida." });
                }

                //Asignar mecánico a la orden
                orden.MecanicoAsignadoId = mecanisoAsignado.Id;
                orden.MecanicoAsignado = mecanisoAsignado;

                //Actualizar disponibilidad del mecánico
                mecanisoAsignado.EstaDisponible = false;

                await _ordenRepository.ActualizarAsync(orden);
                await _userRepository.ActualizarAsync(mecanisoAsignado);

                return Resultado.Exitoso();
            }
            catch (Exception ex)
            {
                return Resultado.Fallido(new[] {"Error en la asignación automática: " + ex.Message});
            }
        }
    }
}
