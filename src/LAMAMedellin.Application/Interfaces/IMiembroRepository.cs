using LAMAMedellin.Domain.Entities;

namespace LAMAMedellin.Application.Interfaces;

public interface IMiembroRepository
{
    Task<IEnumerable<Miembro>> GetAllAsync();
    Task<Miembro?> GetByIdAsync(int id);
    Task<Miembro> CreateAsync(Miembro miembro);
    Task<Miembro> UpdateAsync(Miembro miembro);
    Task<bool> DeleteAsync(int id);
}
