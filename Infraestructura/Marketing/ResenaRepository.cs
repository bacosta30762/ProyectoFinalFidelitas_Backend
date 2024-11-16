using Aplicacion.DataBase;
using Dominio.Entidades;
using Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Marketing
{
    public class ResenaRepository : IResenaRepository
    {
        private readonly DatabaseContext _context;

        public ResenaRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Resena> CrearAsync(Resena resena)
        {
            _context.Resenas.Add(resena);
            await _context.SaveChangesAsync();
            return resena;
        }

        public async Task<List<Resena>> ObtenerTodasAsync() => await _context.Resenas.ToListAsync();

        public async Task<Resena> ModificarAsync(Resena resena)
        {
            _context.Resenas.Update(resena);
            await _context.SaveChangesAsync();
            return resena;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var resena = await _context.Resenas.FindAsync(id);
            if (resena == null) return false;
            _context.Resenas.Remove(resena);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
