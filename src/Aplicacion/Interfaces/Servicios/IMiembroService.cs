using Aplicacion.DTOs;
using Dominio.Entities;

namespace Aplicacion.Interfaces.Servicios;

public interface IMiembroService
{
    Task<IEnumerable<Miembro>> ObtenerTodosAsync();
    Task<PaginatedResponseDto<Miembro>> ObtenerConFiltrosAsync(MiembroFiltrosDto filtros);
    Task<Miembro?> ObtenerPorIdAsync(int id);
    Task<Miembro> CrearAsync(Miembro miembro);
    Task<bool> ActualizarAsync(int id, Miembro miembro);
    Task<bool> EliminarAsync(int id);
}

