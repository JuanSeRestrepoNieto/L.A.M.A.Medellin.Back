using Aplicacion.DTOs;
using Aplicacion.Excepciones;
using Aplicacion.Interfaces.Repositorios;
using Aplicacion.Interfaces.Servicios;
using Dominio.Entities;

namespace Aplicacion.Servicios;

public class MiembroService : IMiembroService
{
    private readonly IRepositorioMiembro _repositorio;

    public MiembroService(IRepositorioMiembro repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<IEnumerable<Miembro>> ObtenerTodosAsync()
    {
        return await _repositorio.ObtenerTodosAsync();
    }

    public async Task<PaginatedResponseDto<Miembro>> ObtenerConFiltrosAsync(MiembroFiltrosDto filtros)
    {
        return await _repositorio.ObtenerConFiltrosAsync(filtros);
    }

    public async Task<Miembro?> ObtenerPorIdAsync(int id)
    {
        return await _repositorio.ObtenerPorIdAsync(id);
    }

    public async Task<Miembro> CrearAsync(Miembro miembro)
    {
        // Las excepciones de base de datos (DbUpdateException, etc.) se propagan
        // al middleware que las maneja apropiadamente
        await _repositorio.AgregarAsync(miembro);
        return miembro;
    }

    public async Task<bool> ActualizarAsync(int id, Miembro miembro)
    {
        var miembroExistente = await _repositorio.ObtenerPorIdAsync(id);
        if (miembroExistente == null)
        {
            throw new NotFoundException("Miembro", id);
        }

        miembro.Id = id;
        await _repositorio.ActualizarAsync(miembro);
        return true;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var miembro = await _repositorio.ObtenerPorIdAsync(id);
        if (miembro == null)
        {
            throw new NotFoundException("Miembro", id);
        }

        await _repositorio.EliminarAsync(id);
        return true;
    }
}

