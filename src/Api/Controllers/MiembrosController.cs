using Api.DTOs;
using Api.Mappings;
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
  public async Task<ActionResult<IEnumerable<MiembroDto>>> GetMembers()
  {
    var miembros = await _miembroService.ObtenerTodosAsync();
    var dtos = miembros.Select(MiembroMapping.ToDto);
    return Ok(dtos);
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
