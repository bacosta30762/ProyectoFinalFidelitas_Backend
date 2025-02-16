﻿using Aplicacion.Usuarios.Dtos;
using Dominio.Comun;
using Dominio.Entidades;

namespace Aplicacion.Interfaces
{
    public interface IOrdenService
    {
        Task<Resultado> ActualizarEstadoOrdenAsync(ActualizarEstadoOrdenDto dto);
        Task<Resultado> AsignarMecanicoAsync(int numeroOrden, string mecanicoId);
        Task BloquearDiaAsync(BloquearDiaDto dto);
        Task<Resultado> CrearOrdenAsync(CrearOrdenDto dto);
        Task<Resultado> CrearOrdenPorAdminAsync(CrearOrdenAdminDto dto);
        Task DesbloquearDiaAsync(BloquearDiaDto dto);
        Task EliminarOrdenPorIdAsync(int id);
        Task<Resultado<List<ListarOrdenMecanicoDto>>> ListarOrdenesPorMecanicoAsync(string mecanicoId);
        Task<Resultado<List<ListarOrdenDto>>> listarOrdenesUsuarioAsync(string usuarioId);
        Task<Resultado<List<ListarOrdenDto>>> ListarTodasLasOrdenesAsync();
        Task<List<string>> ObtenerDiasBloqueadosAsync();
        Task<List<int>> ObtenerHorasDisponiblesAsync(int servicioId, DateOnly dia);
    }
}
