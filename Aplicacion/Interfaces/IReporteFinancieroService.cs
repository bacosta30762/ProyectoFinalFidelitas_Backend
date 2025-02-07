using Aplicacion.Usuarios.Dtos;

namespace Aplicacion.Interfaces
{
    public interface IReporteFinancieroService
    {
        Task<ReporteFinancieroDto> ObtenerReporte(DateTime fechaInicio, DateTime fechaFin);
    }
}
