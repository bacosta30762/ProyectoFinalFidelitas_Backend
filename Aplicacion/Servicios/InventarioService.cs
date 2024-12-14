using Aplicacion.Interfaces;
using Dominio.Entidades;
using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    public class InventarioService : IInventarioService
    {
        private readonly IInventarioRepository _inventarioRepository;

        // Constructor para la inyección de dependencias
        public InventarioService(IInventarioRepository inventarioRepository)
        {
            _inventarioRepository = inventarioRepository ?? throw new ArgumentNullException(nameof(inventarioRepository));
        }

        // Implementación del método para obtener todos los productos
        public async Task<IEnumerable<Inventario>> GetAllAsync()
        {
            return await _inventarioRepository.GetAllAsync();
        }

        // Implementación del método para obtener un producto por ID
        public async Task<Inventario> GetByIdAsync(int id)
        {
            return await _inventarioRepository.GetByIdAsync(id);
        }

        // Implementación del método para obtener productos por categoría
        public async Task<IEnumerable<Inventario>> GetByCategoriaAsync(string categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria))
                throw new ArgumentException("La categoría no puede estar vacía.", nameof(categoria));

            return await _inventarioRepository.GetByCategoriaAsync(categoria);
        }

        // Implementación del método para agregar un nuevo producto
        public async Task AddAsync(Inventario producto)
        {
            if (producto == null)
                throw new ArgumentNullException(nameof(producto));

            await _inventarioRepository.AddAsync(producto);
        }

        // Implementación del método para actualizar un producto
        public async Task UpdateAsync(Inventario producto)
        {
            if (producto == null)
                throw new ArgumentNullException(nameof(producto));

            await _inventarioRepository.UpdateAsync(producto);
        }

        // Implementación del método para eliminar un producto por ID
        public async Task DeleteAsync(int id)
        {
            await _inventarioRepository.DeleteAsync(id);
        }
    }
}
