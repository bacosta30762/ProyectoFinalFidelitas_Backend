using Aplicacion.DataBase;
using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.ComentariosyValoraciones
{
     internal class ComentariosValoracionesRepository : IComentariosValoracionesRepository
    {
        private readonly DatabaseContext _context;

        public ComentariosValoracionesRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ComentariosValoraciones>> GetAllAsync()
        {
            return await _context.Set<ComentariosValoraciones>().ToListAsync();
        }

        public async Task<ComentariosValoraciones> GetByIdAsync(int id)
        {
            return await _context.Set<ComentariosValoraciones>().FindAsync(id);
        }

        public async Task<IEnumerable<ComentariosValoraciones>> GetByClienteAsync(string cliente)
        {
            return await _context.Set<ComentariosValoraciones>()
                .Where(c => c.cliente == cliente)
                .ToListAsync();
        }

        public async Task<IEnumerable<ComentariosValoraciones>> GetByOrdenAsync(int orden)
        {
            return await _context.Set<ComentariosValoraciones>()
                .Where(c => c.orden == orden)
                .ToListAsync();
        }

        public async Task AddAsync(ComentariosValoraciones comentarioValoracion)
        {
            await _context.Set<ComentariosValoraciones>().AddAsync(comentarioValoracion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ComentariosValoraciones comentarioValoracion)
        {
            _context.Set<ComentariosValoraciones>().Update(comentarioValoracion);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<ComentariosValoraciones>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<ComentariosValoraciones>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
