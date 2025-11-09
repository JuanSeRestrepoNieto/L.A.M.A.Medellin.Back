using Dominio.Entities;

namespace Aplicacion.Interfaces.Repositorios;

public interface IRepositorioMiembro
{
    Task<Miembro?> ObtenerPorIdAsync(int id);
    Task<IEnumerable<Miembro>> ObtenerTodosAsync();
    Task AgregarAsync(Miembro entidad);
    Task ActualizarAsync(Miembro entidad);
    Task EliminarAsync(int id);
    Task<Miembro?> ObtenerPorCorreoAsync(string correoElectronico);
    Task<IEnumerable<Miembro>> ObtenerPorRangoAsync(string rango);
    Task<IEnumerable<Miembro>> ObtenerPorEstatusAsync(string estatus);
}

