using LAMAMedellin.Application.Interfaces;
using LAMAMedellin.Domain.Entities;
using LAMAMedellin.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LAMAMedellin.Infrastructure.Repositories;

public class MiembroRepository : IMiembroRepository
{
    private readonly ApplicationDbContext _context;

    public MiembroRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Miembro>> GetAllAsync()
    {
        return await _context.Miembros.ToListAsync();
    }

    public async Task<Miembro?> GetByIdAsync(int id)
    {
        return await _context.Miembros.FindAsync(id);
    }

    public async Task<Miembro> CreateAsync(Miembro miembro)
    {
        _context.Miembros.Add(miembro);
        await _context.SaveChangesAsync();
        return miembro;
    }

    public async Task<Miembro> UpdateAsync(Miembro miembro)
    {
        _context.Entry(miembro).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return miembro;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var miembro = await _context.Miembros.FindAsync(id);
        if (miembro == null)
        {
            return false;
        }

        _context.Miembros.Remove(miembro);
        await _context.SaveChangesAsync();
        return true;
    }
}
