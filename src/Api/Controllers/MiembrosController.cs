using Api.DTOs;
using Api.Mappings;
using Aplicacion.DTOs;
using Aplicacion.Excepciones;
using Aplicacion.Interfaces.Servicios;
using Dominio.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class MiembrosController : InternalBaseController
{
  private readonly IMiembroService _miembroService;

  public MiembrosController(IMiembroService miembroService)
  {
    _miembroService = miembroService;
  }

  [RequiredScope(API_SCOPE_MIEMBROS_LECTURA)]
  [HttpGet]
  public async Task<ActionResult<ApiResponse<PaginatedResponseDto<MiembroDto>>>> GetMembers([FromQuery] MiembroFiltrosDto? filtros = null)
  {
    // Si no se proporcionan filtros, usar valores por defecto
    MiembroFiltrosDto filtrosQuery = filtros ?? new MiembroFiltrosDto();

    // Obtener miembros con filtros y paginación
    PaginatedResponseDto<Miembro> resultado = await _miembroService.ObtenerConFiltrosAsync(filtrosQuery);

    // Mapear los datos de Miembro a MiembroDto
    var respuesta = new PaginatedResponseDto<MiembroDto>
    {
      Data = resultado.Data.Select(MiembroMapping.ToDto),
      Page = resultado.Page,
      PageSize = resultado.PageSize,
      TotalCount = resultado.TotalCount
    };

    var apiResponse = ApiResponse<PaginatedResponseDto<MiembroDto>>.SuccessResponse(
        respuesta,
        "Miembros obtenidos exitosamente"
    );
    apiResponse.TraceId = HttpContext.TraceIdentifier;

    return Ok(apiResponse);
  }

  [RequiredScope(API_SCOPE_MIEMBROS_TODOS)]
  [HttpGet("GetAll")]
  public async Task<ActionResult<ApiResponse<List<MiembroDto>>>> GetAllMembers()
  {

    var resultado = await _miembroService.ObtenerTodosAsync();
    
    var apiResponse = ApiResponse<List<MiembroDto>>.SuccessResponse(
        resultado.Select(MiembroMapping.ToDto).ToList(),
        "Miembros obtenidos exitosamente"
    );
    apiResponse.TraceId = HttpContext.TraceIdentifier;

    return Ok(apiResponse);
  }

  [RequiredScope(API_SCOPE_MIEMBROS_LEER_ID)]
  [HttpGet("{id:int}")]
  public async Task<ActionResult<ApiResponse<MiembroDto>>> GetMember(int id)
  {
    var miembro = await _miembroService.ObtenerPorIdAsync(id);
    if (miembro is null)
    {
      throw new NotFoundException("Miembro", id);
    }

    var apiResponse = ApiResponse<MiembroDto>.SuccessResponse(
        MiembroMapping.ToDto(miembro),
        "Miembro obtenido exitosamente"
    );
    apiResponse.TraceId = HttpContext.TraceIdentifier;

    return Ok(apiResponse);
  }

  [RequiredScope(API_SCOPE_MIEMBROS_CREACION)]
  [HttpPost]
  public async Task<ActionResult<ApiResponse<MiembroDto>>> CreateMember(CreateMiembroDto dto)
  {
    var miembro = MiembroMapping.ToDomain(dto);
    var created = await _miembroService.CrearAsync(miembro);
    var createdDto = MiembroMapping.ToDto(created);

    var apiResponse = ApiResponse<MiembroDto>.SuccessResponse(
        createdDto,
        "Miembro creado exitosamente"
    );
    apiResponse.TraceId = HttpContext.TraceIdentifier;

    return CreatedAtAction(nameof(GetMember), new { id = createdDto.Id }, apiResponse);
  }

  [RequiredScope(API_SCOPE_MIEMBROS_ACTUALIZACION)]
  [HttpPut("{id:int}")]
  public async Task<ActionResult<ApiResponse<object>>> UpdateMember(int id, UpdateMiembroDto dto)
  {
    var miembro = MiembroMapping.ToDomain(dto, id);
    await _miembroService.ActualizarAsync(id, miembro);

    var apiResponse = ApiResponse<object>.SuccessResponse(
        null,
        "Miembro actualizado exitosamente"
    );
    apiResponse.TraceId = HttpContext.TraceIdentifier;

    return Ok(apiResponse);
  }

  [RequiredScope(API_SCOPE_MIEMBROS_ELIMINACION)]
  [HttpDelete("{id:int}")]
  public async Task<ActionResult<ApiResponse<object>>> DeleteMember(int id)
  {
    await _miembroService.EliminarAsync(id);

    var apiResponse = ApiResponse<object>.SuccessResponse(
        null,
        "Miembro eliminado exitosamente"
    );
    apiResponse.TraceId = HttpContext.TraceIdentifier;

    return Ok(apiResponse);
  }
}
