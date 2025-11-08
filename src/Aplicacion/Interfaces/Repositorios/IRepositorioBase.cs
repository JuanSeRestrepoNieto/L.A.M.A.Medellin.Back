namespace Aplicacion.Interfaces.Repositorios;

public interface IRepositorioBase<T, TId>
  where T : class
{
  Task<T> ObtenerPorIdAsync(TId id);
  Task<IEnumerable<T>> ObtenerTodosAsync();
  Task AgregarAsync(T entidad);
  Task ActualizarAsync(T entidad);
  Task EliminarAsync(TId id);
}