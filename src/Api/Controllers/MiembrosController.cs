using Api.Mappings;
using Aplicacion.DTOs;
using Aplicacion.Interfaces.Servicios;
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
  public async Task<ActionResult<PaginatedResponseDto<MiembroDto>>> GetMembers([FromQuery] MiembroFiltrosDto? filtros = null)
  {
    // Si no se proporcionan filtros, usar valores por defecto
    var filtrosQuery = filtros ?? new MiembroFiltrosDto();
    
    // Validar el modelo
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }
    
    // Obtener miembros con filtros y paginación
    var resultado = await _miembroService.ObtenerConFiltrosAsync(filtrosQuery);
    
    // Mapear los datos de Miembro a MiembroDto
    var respuesta = new PaginatedResponseDto<MiembroDto>
    {
        Data = resultado.Data.Select(MiembroMapping.ToDto),
        Page = resultado.Page,
        PageSize = resultado.PageSize,
        TotalCount = resultado.TotalCount
    };
    
    return Ok(respuesta);
  }

  [HttpGet("{id:int}")]
  public async Task<ActionResult<MiembroDto>> GetMember(int id)
  {
    var miembro = await _miembroService.ObtenerPorIdAsync(id);
    if (miembro is null)
    {
      return NotFound();
    }
    return Ok(MiembroMapping.ToDto(miembro));
  }

  [HttpPost]
  public async Task<ActionResult<MiembroDto>> CreateMember(CreateMiembroDto dto)
  {
    var miembro = MiembroMapping.ToDomain(dto);
    var created = await _miembroService.CrearAsync(miembro);
    var createdDto = MiembroMapping.ToDto(created);
    return CreatedAtAction(nameof(GetMember), new { id = createdDto.Id }, createdDto);
  }

  [HttpPut("{id:int}")]
  public async Task<IActionResult> UpdateMember(int id, UpdateMiembroDto dto)
  {
    var miembro = MiembroMapping.ToDomain(dto, id);
    var updated = await _miembroService.ActualizarAsync(id, miembro);
    return updated ? NoContent() : NotFound();
  }

  [HttpDelete("{id:int}")]
  public async Task<IActionResult> DeleteMember(int id)
  {
    var deleted = await _miembroService.EliminarAsync(id);
    return deleted ? NoContent() : NotFound();
  }
}
