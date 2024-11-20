using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    internal class IComentariosValoracionesService

    public interface IComentariosValoracionesService
    {
        Task<IEnumerable<ComentariosValoraciones>> GetAllAsync();
        Task<IEnumerable<ComentariosValoraciones>> GetByClienteAsync(string cliente);
        Task<IEnumerable<ComentariosValoraciones>> GetByOrdenAsync(int orden);
        Task AddAsync(ComentariosValoraciones comentarioValoracion);
        Task UpdateAsync(ComentariosValoraciones comentarioValoracion);
        Task DeleteAsync(int id);
    }

}
}
