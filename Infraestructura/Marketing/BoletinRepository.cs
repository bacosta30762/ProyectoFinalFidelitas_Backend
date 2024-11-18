
using Microsoft.EntityFrameworkCore;
using Aplicacion.DataBase;
using Dominio.Entidades;
using Dominio.Repositorios;

public class BoletinRepository : IBoletinRepository
{
    private readonly DatabaseContext _context;

    public BoletinRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Boletin> CrearAsync(Boletin boletin)
    {
        _context.Boletines.Add(boletin);
        await _context.SaveChangesAsync();
        return boletin;
    }

    public async Task<List<Boletin>> ObtenerTodosAsync() => await _context.Boletines.ToListAsync();

    public async Task<Boletin> ModificarAsync(Boletin boletin)
    {
        _context.Boletines.Update(boletin);
        await _context.SaveChangesAsync();
        return boletin;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var boletin = await _context.Boletines.FindAsync(id);
        if (boletin == null) return false;
        _context.Boletines.Remove(boletin);
        await _context.SaveChangesAsync();
        return true;
    }
}
