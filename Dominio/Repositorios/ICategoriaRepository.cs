using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public interface ICategoriaRepository
    {
        Task<List<Categoria>> ObtenerTodas();
        Task<Categoria> ObtenerPorId(int id);
    }
}
