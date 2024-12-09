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
    public class InventarioService : IInventarioService
    {
        private readonly IInventarioRepository _inventarioRepository;

        public InventarioService(IInventarioRepository inventarioRepository)
        {
            _inventarioRepository = inventarioRepository;
        }

        public async Task<IEnumerable<Inventario>> GetAllAsync()
        {
            return await _inventarioRepository.GetAllAsync();
        }

        public async Task AddAsync(Inventario inventario)
        {
            await _inventarioRepository.AddAsync(inventario);
        }
    }
}
