using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    internal class ComentariosValoracionesService
    public class ComentariosValoracionesService : IComentariosValoracionesService
    {
        private readonly IComentariosValoracionesRepository _comentariosValoracionesRepository;

        public ComentariosValoracionesService(IComentariosValoracionesRepository comentariosValoracionesRepository)
        {
            _comentariosValoracionesRepository = comentariosValoracionesRepository;
        }

        public async Task<IEnumerable<ComentariosValoraciones>> GetAllAsync()
        {
            return await _comentariosValoracionesRepository.GetAllAsync();
        }

        public async Task<IEnumerable<ComentariosValoraciones>> GetByClienteAsync(string cliente)
        {
            return await _comentariosValoracionesRepository.GetByClienteAsync(cliente);
        }

        public async Task<IEnumerable<ComentariosValoraciones>> GetByOrdenAsync(int orden)
        {
            return await _comentariosValoracionesRepository.GetByOrdenAsync(orden);
        }

        public async Task AddAsync(ComentariosValoraciones comentarioValoracion)
        {
            await _comentariosValoracionesRepository.AddAsync(comentarioValoracion);
        }

        public async Task UpdateAsync(ComentariosValoraciones comentarioValoracion)
        {
            await _comentariosValoracionesRepository.UpdateAsync(comentarioValoracion);
        }

        public async Task DeleteAsync(int id)
        {
            await _comentariosValoracionesRepository.DeleteAsync(id);
        }
    }

}
