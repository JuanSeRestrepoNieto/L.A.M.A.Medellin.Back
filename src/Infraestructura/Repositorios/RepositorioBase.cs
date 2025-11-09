using Aplicacion.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Repositorios;

public abstract class RepositorioBase<T, TId> : IRepositorioBase<T, TId>
  where T : class
{
  protected readonly DbContext _context;
  protected readonly DbSet<T> _dbSet;

  protected RepositorioBase(DbContext context)
  {
    _context = context;
    _dbSet = context.Set<T>();
  }

  public virtual async Task<T?> ObtenerPorIdAsync(TId id)
  {
    return await _dbSet.FindAsync(id);
  }

  public virtual async Task<IEnumerable<T>> ObtenerTodosAsync()
  {
    return await _dbSet.ToListAsync();
  }

  public virtual async Task AgregarAsync(T entidad)
  {
    await _dbSet.AddAsync(entidad);
    await _context.SaveChangesAsync();
  }

  public virtual async Task ActualizarAsync(T entidad)
  {
    _dbSet.Update(entidad);
    await _context.SaveChangesAsync();
  }

  public virtual async Task EliminarAsync(TId id)
  {
    var entidad = await ObtenerPorIdAsync(id);
    if (entidad != null)
    {
      _dbSet.Remove(entidad);
      await _context.SaveChangesAsync();
    }
  }
}