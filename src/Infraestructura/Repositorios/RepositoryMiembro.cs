using Aplicacion.DTOs;
using Aplicacion.Interfaces.Repositorios;
using Dominio.Entities;
using Infraestructura.Contexto;
using Infraestructura.DataEntities;
using Infraestructura.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Repositorios;

public class RepositoryMiembro : IRepositorioMiembro
{
  private readonly MiembrosContexto _context;

  public RepositoryMiembro(MiembrosContexto context)
  {
    _context = context;
  }

  public async Task<Miembro?> ObtenerPorIdAsync(int id)
  {
    var dataModel = await _context.Miembros.FindAsync(id);
    return dataModel == null ? null : MiembroMapping.ToDomain(dataModel);
  }

  public async Task<IEnumerable<Miembro>> ObtenerTodosAsync()
  {
    var dataModels = await _context.Miembros.ToListAsync();
    return dataModels.Select(MiembroMapping.ToDomain);
  }

  public async Task<PaginatedResponseDto<Miembro>> ObtenerConFiltrosAsync(MiembroFiltrosDto filtros)
  {
    var query = _context.Miembros.AsQueryable();

    // Aplicar filtros
    if (!string.IsNullOrWhiteSpace(filtros.Estatus))
    {
      query = query.Where(m => m.Estatus == filtros.Estatus);
    }

    if (!string.IsNullOrWhiteSpace(filtros.Rango))
    {
      query = query.Where(m => m.Rango == filtros.Rango);
    }

    if (!string.IsNullOrWhiteSpace(filtros.Cargo))
    {
      query = query.Where(m => m.Cargo != null && m.Cargo.Contains(filtros.Cargo));
    }

    // Obtener el total antes de paginar
    var totalCount = await query.CountAsync();

    // Aplicar paginación
    var skip = (filtros.Page - 1) * filtros.PageSize;
    var dataModels = await query
      .Skip(skip)
      .Take(filtros.PageSize)
      .ToListAsync();

    var miembros = dataModels.Select(MiembroMapping.ToDomain).ToList();

    return new PaginatedResponseDto<Miembro>
    {
      Data = miembros,
      Page = filtros.Page,
      PageSize = filtros.PageSize,
      TotalCount = totalCount
    };
  }

  public async Task AgregarAsync(Miembro entidad)
  {
    var dataModel = MiembroMapping.ToDataModel(entidad);
    await _context.Miembros.AddAsync(dataModel);
    await _context.SaveChangesAsync();
    // Actualizar el ID de la entidad de dominio con el ID generado
    entidad.Id = dataModel.Id;
  }

  public async Task ActualizarAsync(Miembro entidad)
  {
    var dataModel = await _context.Miembros.FindAsync(entidad.Id);
    if (dataModel == null)
    {
      throw new KeyNotFoundException($"Miembro con ID {entidad.Id} no encontrado");
    }

    // Actualizar propiedades
    dataModel.Nombre = entidad.Nombre;
    dataModel.Apellido = entidad.Apellido;
    dataModel.Celular = entidad.Celular;
    dataModel.CorreoElectronico = entidad.CorreoElectronico;
    dataModel.FechaIngreso = entidad.FechaIngreso;
    dataModel.Direccion = entidad.Direccion;
    dataModel.MemberNumber = entidad.MemberNumber;
    dataModel.Cargo = entidad.Cargo;
    dataModel.Rango = entidad.Rango;
    dataModel.Estatus = entidad.Estatus;
    dataModel.FechaNacimiento = entidad.FechaNacimiento;
    dataModel.Cedula = entidad.Cedula;
    dataModel.RH = entidad.RH;
    dataModel.EPS = entidad.EPS;
    dataModel.Padrino = entidad.Padrino;
    dataModel.Foto = entidad.Foto;
    dataModel.ContactoEmergencia = entidad.ContactoEmergencia;
    dataModel.Ciudad = entidad.Ciudad;
    dataModel.Moto = entidad.Moto;
    dataModel.AnoModelo = entidad.AnoModelo;
    dataModel.Marca = entidad.Marca;
    dataModel.CilindrajeCC = entidad.CilindrajeCC;
    dataModel.PlacaMatricula = entidad.PlacaMatricula;
    dataModel.FechaExpedicionLicenciaConduccion = entidad.FechaExpedicionLicenciaConduccion;
    dataModel.FechaExpedicionSOAT = entidad.FechaExpedicionSOAT;

    _context.Miembros.Update(dataModel);
    await _context.SaveChangesAsync();
  }

  public async Task EliminarAsync(int id)
  {
    var dataModel = await _context.Miembros.FindAsync(id);
    if (dataModel != null)
    {
      _context.Miembros.Remove(dataModel);
      await _context.SaveChangesAsync();
    }
  }

  public async Task<Miembro?> ObtenerPorCorreoAsync(string correoElectronico)
  {
    var dataModel = await _context.Miembros
      .FirstOrDefaultAsync(m => m.CorreoElectronico == correoElectronico);
    return dataModel == null ? null : MiembroMapping.ToDomain(dataModel);
  }

  public async Task<IEnumerable<Miembro>> ObtenerPorRangoAsync(string rango)
  {
    var dataModels = await _context.Miembros
      .Where(m => m.Rango == rango)
      .ToListAsync();
    return dataModels.Select(MiembroMapping.ToDomain);
  }

  public async Task<IEnumerable<Miembro>> ObtenerPorEstatusAsync(string estatus)
  {
    var dataModels = await _context.Miembros
      .Where(m => m.Estatus == estatus)
      .ToListAsync();
    return dataModels.Select(MiembroMapping.ToDomain);
  }
}

