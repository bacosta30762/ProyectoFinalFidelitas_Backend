using Aplicacion.Interfaces;
using Aplicacion.Usuarios.Dtos;
using Dominio.Repositorios;

namespace Aplicacion.Servicios
{
    public class ReporteFinancieroService : IReporteFinancieroService
    {
        private readonly IIngresoRepository _ingresoRepository;
        private readonly IEgresoRepository _egresoRepository;

        public ReporteFinancieroService(IIngresoRepository ingresoRepository, IEgresoRepository egresoRepository)
        {
            _ingresoRepository = ingresoRepository;
            _egresoRepository = egresoRepository;
        }

        public async Task<ReporteFinancieroDto> ObtenerReporte(DateTime fechaInicio, DateTime fechaFin)
        {
            var ingresos = await _ingresoRepository.ObtenerTodos();
            var egresos = await _egresoRepository.ObtenerTodos();

            var ingresosFiltrados = ingresos.Where(i => i.Fecha >= fechaInicio && i.Fecha <= fechaFin).Sum(i => i.Monto);
            var egresosFiltrados = egresos.Where(e => e.Fecha >= fechaInicio && e.Fecha <= fechaFin).Sum(e => e.Monto);

            var balance = ingresosFiltrados - egresosFiltrados;

            return new ReporteFinancieroDto
            {
                Ingresos = ingresosFiltrados,
                Egresos = egresosFiltrados,
                Balance = balance
            };
        }
    }
}
