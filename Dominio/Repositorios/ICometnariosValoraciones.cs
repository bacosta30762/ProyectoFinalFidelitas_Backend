using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public interface IComentariosValoracionesRepository
    {
        Task<IEnumerable<ComentariosValoraciones>> ObtenerTodosAsync();
        Task<ComentariosValoraciones> ObtenerporIdAsync(int id);
        Task<IEnumerable<ComentariosValoraciones>> ObtenerporyClienteAsync(string cliente);
        Task<IEnumerable<ComentariosValoraciones>> ObtenerporOrdenAsync(int orden);
        Task AgregarAsync(ComentariosValoraciones comentarioValoracion);
        Task ActualizarAsync(ComentariosValoraciones comentarioValoracion);
        Task EliminarAsync(int id);
    }
}