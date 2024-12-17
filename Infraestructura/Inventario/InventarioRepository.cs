using Aplicacion.Interfaces;
using Dominio.Entidades;
using Dominio.Repositorios;

namespace Aplicacion.Servicios
{
    public class InventarioService : IInventarioService
    {
        private readonly IInventarioRepository _inventarioRepository;

        // Constructor que recibe el repositorio mediante inyección de dependencias
        public InventarioService(IInventarioRepository inventarioRepository)
        {
            _inventarioRepository = inventarioRepository;
        }

        // Método para obtener todos los elementos del inventario
        public async Task<IEnumerable<Inventario>> GetAllAsync()
        {
            return await _inventarioRepository.GetAllAsync();
        }

        // Método para obtener un elemento del inventario por ID
        public async Task<Inventario> GetByIdAsync(int id)
        {
            return await _inventarioRepository.GetByIdAsync(id);
        }

        // Método para obtener elementos del inventario por categoría
        public async Task<IEnumerable<Inventario>> GetByCategoriaAsync(string categoria)
        {
            return await _inventarioRepository.GetByCategoriaAsync(categoria);
        }

        // Método para agregar un nuevo elemento al inventario
        public async Task AddAsync(Inventario producto)
        {
            await _inventarioRepository.AddAsync(producto);
        }

        // Método para actualizar un elemento del inventario
        public async Task UpdateAsync(Inventario producto)
        {
            await _inventarioRepository.UpdateAsync(producto);
        }

        // Método para eliminar un elemento del inventario por ID
        public async Task DeleteAsync(int id)
        {
            await _inventarioRepository.DeleteAsync(id);
        }
    }
}

