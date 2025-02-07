using Aplicacion.Interfaces;
using Aplicacion.Usuarios.Dtos;
using Dominio.Entidades;
using Dominio.Repositorios;

namespace Aplicacion.Servicios
{
    public class EgresoService : IEgresoService
    {
        private readonly IEgresoRepository _egresoRepository;

        public EgresoService(IEgresoRepository egresoRepository)
        {
            _egresoRepository = egresoRepository;
        }

        public async Task<List<EgresoDto>> ObtenerTodos()
        {
            var egresos = await _egresoRepository.ObtenerTodos();
            return egresos.Select(e => new EgresoDto
            {
                Id = e.Id,
                Monto = e.Monto,
                Descripcion = e.Descripcion,
                Fecha = e.Fecha,
                MetodoPago = e.MetodoPago,
                NumeroFactura = e.NumeroFactura
            }).ToList();
        }

        public async Task<EgresoDto> ObtenerPorId(int id)
        {
            var egreso = await _egresoRepository.ObtenerPorId(id);
            if (egreso == null) return null;

            return new EgresoDto
            {
                Id = egreso.Id,
                Monto = egreso.Monto,
                Descripcion = egreso.Descripcion,
                Fecha = egreso.Fecha,
                MetodoPago = egreso.MetodoPago,
                NumeroFactura = egreso.NumeroFactura
            };
        }

        public async Task<EgresoDto> Agregar(EgresoDto dto)
        {
            var egreso = new Egreso
            {
                Monto = dto.Monto,
                Descripcion = dto.Descripcion,
                Fecha = dto.Fecha,
                MetodoPago = dto.MetodoPago,
                NumeroFactura = dto.NumeroFactura
            };

            var nuevoEgreso = await _egresoRepository.Agregar(egreso);

            return new EgresoDto
            {
                Id = nuevoEgreso.Id,
                Monto = nuevoEgreso.Monto,
                Descripcion = nuevoEgreso.Descripcion,
                Fecha = nuevoEgreso.Fecha,
                MetodoPago = nuevoEgreso.MetodoPago,
                NumeroFactura = nuevoEgreso.NumeroFactura
            };
        }

        public async Task<bool> Actualizar(EgresoDto dto)
        {
            var egreso = await _egresoRepository.ObtenerPorId(dto.Id);
            if (egreso == null) return false;

            egreso.Monto = dto.Monto;
            egreso.Descripcion = dto.Descripcion;
            egreso.Fecha = dto.Fecha;
            egreso.MetodoPago = dto.MetodoPago;
            egreso.NumeroFactura = dto.NumeroFactura;

            return await _egresoRepository.Actualizar(egreso);
        }

        public async Task<bool> Eliminar(int id) =>
            await _egresoRepository.Eliminar(id);
    }
}
