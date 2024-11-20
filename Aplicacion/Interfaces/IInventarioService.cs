using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    
    public interface IInventarioService
    {
        Task<IEnumerable<Inventario>> GetAllAsync();
        Task<Inventario> GetByIdAsync(int id);
        Task<IEnumerable<Inventario>> GetByCategoriaAsync(string categoria);
        Task AddAsync(Inventario producto);
        Task UpdateAsync(Inventario producto);
        Task DeleteAsync(int id);
    }

}
