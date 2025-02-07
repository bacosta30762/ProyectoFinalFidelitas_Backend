using Aplicacion.Interfaces;
using Aplicacion.Usuarios.Dtos;
using Dominio.Entidades;
using Dominio.Repositorios;

namespace Aplicacion.Servicios
{
    public class IngresoService : IIngresoService
    {
        private readonly IIngresoRepository _ingresoRepository;

        public IngresoService(IIngresoRepository ingresoRepository)
        {
            _ingresoRepository = ingresoRepository;
        }

        public async Task<List<IngresoDto>> ObtenerTodos()
        {
            var ingresos = await _ingresoRepository.ObtenerTodos();
            return ingresos.Select(i => new IngresoDto
            {
                Id = i.Id,
                Monto = i.Monto,
                Descripcion = i.Descripcion,
                Fecha = i.Fecha,
                MetodoPago = i.MetodoPago,
                NumeroFactura = i.NumeroFactura
            }).ToList();
        }

        public async Task<IngresoDto> ObtenerPorId(int id)
        {
            var ingreso = await _ingresoRepository.ObtenerPorId(id);
            if (ingreso == null) return null;

            return new IngresoDto
            {
                Id = ingreso.Id,
                Monto = ingreso.Monto,
                Descripcion = ingreso.Descripcion,
                Fecha = ingreso.Fecha,
                MetodoPago = ingreso.MetodoPago,
                NumeroFactura = ingreso.NumeroFactura
            };
        }

        public async Task<IngresoDto> Agregar(IngresoDto dto)
        {
            var ingreso = new Ingreso
            {
                Monto = dto.Monto,
                Descripcion = dto.Descripcion,
                Fecha = dto.Fecha,
                MetodoPago = dto.MetodoPago,
                NumeroFactura = dto.NumeroFactura
            };

            var nuevoIngreso = await _ingresoRepository.Agregar(ingreso);

            return new IngresoDto
            {
                Id = nuevoIngreso.Id,
                Monto = nuevoIngreso.Monto,
                Descripcion = nuevoIngreso.Descripcion,
                Fecha = nuevoIngreso.Fecha,
                MetodoPago = nuevoIngreso.MetodoPago,
                NumeroFactura = nuevoIngreso.NumeroFactura
            };
        }

        public async Task<bool> Actualizar(IngresoDto dto)
        {
            var ingreso = await _ingresoRepository.ObtenerPorId(dto.Id);
            if (ingreso == null) return false;

            ingreso.Monto = dto.Monto;
            ingreso.Descripcion = dto.Descripcion;
            ingreso.Fecha = dto.Fecha;
            ingreso.MetodoPago = dto.MetodoPago;
            ingreso.NumeroFactura = dto.NumeroFactura;

            return await _ingresoRepository.Actualizar(ingreso);
        }

        public async Task<bool> Eliminar(int id) =>
            await _ingresoRepository.Eliminar(id);
    }
}
