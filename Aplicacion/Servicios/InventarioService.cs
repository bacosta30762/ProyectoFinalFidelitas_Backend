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

      

       
        Task<IEnumerable<Inventario>> IInventarioService.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<Inventario> IInventarioService.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Inventario>> IInventarioService.GetByCategoriaAsync(string categoria)
        {
            throw new NotImplementedException();
        }

        Task IInventarioService.AddAsync(Inventario producto)
        {
            throw new NotImplementedException();
        }

        Task IInventarioService.UpdateAsync(Inventario producto)
        {
            throw new NotImplementedException();
        }

        Task IInventarioService.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
