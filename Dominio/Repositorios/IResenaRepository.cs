using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public interface IResenaRepository
    {
        Task<Resena> CrearAsync(Resena resena);
        Task<List<Resena>> ObtenerTodasAsync();
        Task<Resena> ModificarAsync(Resena resena);
        Task<bool> EliminarAsync(int id);
    }
}
