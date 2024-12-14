using Aplicacion.Interfaces;
using Dominio.Entidades;
using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    public class ContabilidadService : IContabilidadService
    {
        private readonly IEgresoRepository _egresoRepository;
        private readonly IIngresoRepository _ingresoRepository;

        public ContabilidadService(IEgresoRepository egresoRepository, IIngresoRepository ingresoRepository)
        {
            _egresoRepository = egresoRepository;
            _ingresoRepository = ingresoRepository;
        }

        // Métodos para **Egresos**

        public async Task<Egreso> AgregarEgresoAsync(Egreso egreso)
        {
            return await _egresoRepository.AgregarEgresoAsync(egreso);
        }

        public async Task<List<Egreso>> ObtenerEgresosAsync()
        {
            return await _egresoRepository.ObtenerEgresosAsync();
        }

        public async Task<Egreso> ObtenerEgresoPorIdAsync(int id)
        {
            return await _egresoRepository.ObtenerEgresoPorIdAsync(id); 
        }

        public async Task<bool> EliminarEgresoAsync(int id)
        {
            var resultado = await _egresoRepository.EliminarEgresoAsync(id);
            return resultado; 
        }

        

        public async Task<Ingreso> AgregarIngresoAsync(Ingreso ingreso)
        {
            return await _ingresoRepository.AgregarIngresoAsync(ingreso);
        }

        public async Task<List<Ingreso>> ObtenerIngresosAsync()
        {
            return await _ingresoRepository.ObtenerIngresosAsync();
        }

        public async Task<Ingreso> ObtenerIngresoPorIdAsync(int id)
        {
            return await _ingresoRepository.ObtenerIngresoPorIdAsync(id);
        }

        public async Task<bool> EliminarIngresoAsync(int id)
        {
            var resultado = await _ingresoRepository.EliminarIngresoAsync(id);
            return resultado; 
        }



        
    }
}
