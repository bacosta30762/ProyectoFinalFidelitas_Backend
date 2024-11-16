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

    public class SuscripcionRepository : ISuscripcionRepository
    {
        private readonly DatabaseContext _context;

        public SuscripcionRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Suscripcion> CrearAsync(Suscripcion suscripcion)
        {
            _context.Suscripciones.Add(suscripcion);
            await _context.SaveChangesAsync();
            return suscripcion;
        }

        public async Task<List<Suscripcion>> ObtenerTodasAsync() => await _context.Suscripciones.ToListAsync();

        public async Task<Suscripcion> ModificarAsync(Suscripcion suscripcion)
        {
            _context.Suscripciones.Update(suscripcion);
            await _context.SaveChangesAsync();
            return suscripcion;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var suscripcion = await _context.Suscripciones.FindAsync(id);
            if (suscripcion == null) return false;
            _context.Suscripciones.Remove(suscripcion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
