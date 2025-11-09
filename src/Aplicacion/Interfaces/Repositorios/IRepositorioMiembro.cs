using Dominio.Entities;

namespace Aplicacion.Interfaces.Repositorios;

public interface IRepositorioMiembro : IRepositorioBase<Miembro, int>
{
    Task<Miembro?> ObtenerPorCorreoAsync(string correoElectronico);
    Task<IEnumerable<Miembro>> ObtenerPorRangoAsync(string rango);
    Task<IEnumerable<Miembro>> ObtenerPorEstatusAsync(string estatus);
}

