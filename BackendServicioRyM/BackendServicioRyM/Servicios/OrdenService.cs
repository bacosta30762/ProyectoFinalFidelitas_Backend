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

        public OrdenService(IOrdenRepository ordenRepository)
        {
            _ordenRepository = ordenRepository;
        }

        public async Task<Resultado<Orden>> CrearOrdenAsync(CrearOrdenDto dto)
        {
            try
            {
                var orden = new Orden
                {
                    Servicio = dto.Servicio,
                    Cliente = dto.Cliente,
                    Acciones = dto.Acciones,
                    Estado = "Pendiente",
                    PlacaVehiculo = dto.PlacaVehiculo
                };

                // Aquí se podría agregar lógica adicional si es necesario

                await _ordenRepository.CrearAsync(orden);

                return Resultado<Orden>.Exitoso(orden);
            }
            catch (Exception ex)
            {
                return Resultado<Orden>.Fallido(new[] { "Error al crear la orden: " + ex.Message });
            }
        }
    }
}
