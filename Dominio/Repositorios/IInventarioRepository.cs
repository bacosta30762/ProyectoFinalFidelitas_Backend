using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public interface IInventarioRepository
    {
        Task AddAsync(Inventario producto);
        Task DeleteAsync(int id);
        Task<IEnumerable<Inventario>> GetAllAsync();
        Task<IEnumerable<Inventario>> GetByCategoriaAsync(string categoria);
        Task<Inventario> GetByIdAsync(int id);
        Task UpdateAsync(Inventario producto);
    }

}
