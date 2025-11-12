using Api.DTOs;
using Api.Mappings;
using Aplicacion.DTOs;
using Aplicacion.Excepciones;
using Aplicacion.Interfaces.Servicios;
using Dominio.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MiembrosController : ControllerBase
{
  private readonly IMiembroService _miembroService;

  public MiembrosController(IMiembroService miembroService)
  {
    _miembroService = miembroService;
  }

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
