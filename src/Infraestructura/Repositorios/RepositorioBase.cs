using Aplicacion.Interfaces.Repositorios;

namespace Infraestructura.Repositorios;

public abstract class RepositoryBase<T, TId> : IRepositorioBase<T, TId>
  where T : class
{
  public async Task<T> ObtenerPorIdAsync(TId id)
  {
    // Implementación para obtener una entidad por su ID
  }

  public async Task<IEnumerable<T>> ObtenerTodosAsync()
  {
    // Implementación para obtener todas las entidades
  }

  public async Task AgregarAsync(T entidad)
  {
    // Implementación para agregar una nueva entidad
  }

  public async Task ActualizarAsync(T entidad)
  {
    // Implementación para actualizar una entidad existente
  }

  public async Task EliminarAsync(TId id)
  {
    // Implementación para eliminar una entidad por su ID
  }
}