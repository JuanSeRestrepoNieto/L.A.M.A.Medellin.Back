using LAMAMedellin.Application.DTOs;
using LAMAMedellin.Application.Interfaces;
using LAMAMedellin.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LAMAMedellin.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MiembrosController : ControllerBase
{
    private readonly IMiembroRepository _miembroRepository;
    private readonly ILogger<MiembrosController> _logger;

    public MiembrosController(IMiembroRepository miembroRepository, ILogger<MiembrosController> logger)
    {
        _miembroRepository = miembroRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MiembroDto>>> GetAll()
    {
        try
        {
            var miembros = await _miembroRepository.GetAllAsync();
            var miembroDtos = miembros.Select(m => MapToDto(m));
            return Ok(miembroDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all miembros");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MiembroDto>> GetById(int id)
    {
        try
        {
            var miembro = await _miembroRepository.GetByIdAsync(id);
            if (miembro == null)
            {
                return NotFound();
            }
            return Ok(MapToDto(miembro));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting miembro by id {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<MiembroDto>> Create([FromBody] CreateMiembroDto createMiembroDto)
    {
        try
        {
            var miembro = MapToEntity(createMiembroDto);
            var createdMiembro = await _miembroRepository.CreateAsync(miembro);
            return CreatedAtAction(nameof(GetById), new { id = createdMiembro.Id }, MapToDto(createdMiembro));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating miembro");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MiembroDto>> Update(int id, [FromBody] MiembroDto miembroDto)
    {
        try
        {
            if (id != miembroDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            var existingMiembro = await _miembroRepository.GetByIdAsync(id);
            if (existingMiembro == null)
            {
                return NotFound();
            }

            var miembro = MapToEntity(miembroDto);
            var updatedMiembro = await _miembroRepository.UpdateAsync(miembro);
            return Ok(MapToDto(updatedMiembro));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating miembro {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var deleted = await _miembroRepository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting miembro {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    private static MiembroDto MapToDto(Miembro miembro)
    {
        return new MiembroDto
        {
            Id = miembro.Id,
            Nombre = miembro.Nombre,
            Apellido = miembro.Apellido,
            Celular = miembro.Celular,
            CorreoElectronico = miembro.CorreoElectronico,
            FechaIngreso = miembro.FechaIngreso,
            Direccion = miembro.Direccion,
            Member = miembro.Member,
            Cargo = miembro.Cargo,
            Rango = miembro.Rango,
            Estatus = miembro.Estatus,
            FechaNacimiento = miembro.FechaNacimiento,
            Cedula = miembro.Cedula,
            RH = miembro.RH,
            EPS = miembro.EPS,
            Padrino = miembro.Padrino,
            Foto = miembro.Foto,
            ContactoEmergencia = miembro.ContactoEmergencia,
            Ciudad = miembro.Ciudad,
            Moto = miembro.Moto,
            AnoModelo = miembro.AnoModelo,
            Marca = miembro.Marca,
            CilindrajeCC = miembro.CilindrajeCC,
            PlacaMatricula = miembro.PlacaMatricula,
            FechaExpedicionLicenciaConduccion = miembro.FechaExpedicionLicenciaConduccion,
            FechaExpedicionSOAT = miembro.FechaExpedicionSOAT
        };
    }

    private static Miembro MapToEntity(CreateMiembroDto dto)
    {
        return new Miembro
        {
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Celular = dto.Celular,
            CorreoElectronico = dto.CorreoElectronico,
            FechaIngreso = dto.FechaIngreso,
            Direccion = dto.Direccion,
            Member = dto.Member,
            Cargo = dto.Cargo,
            Rango = dto.Rango,
            Estatus = dto.Estatus,
            FechaNacimiento = dto.FechaNacimiento,
            Cedula = dto.Cedula,
            RH = dto.RH,
            EPS = dto.EPS,
            Padrino = dto.Padrino,
            Foto = dto.Foto,
            ContactoEmergencia = dto.ContactoEmergencia,
            Ciudad = dto.Ciudad,
            Moto = dto.Moto,
            AnoModelo = dto.AnoModelo,
            Marca = dto.Marca,
            CilindrajeCC = dto.CilindrajeCC,
            PlacaMatricula = dto.PlacaMatricula,
            FechaExpedicionLicenciaConduccion = dto.FechaExpedicionLicenciaConduccion,
            FechaExpedicionSOAT = dto.FechaExpedicionSOAT
        };
    }

    private static Miembro MapToEntity(MiembroDto dto)
    {
        return new Miembro
        {
            Id = dto.Id,
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Celular = dto.Celular,
            CorreoElectronico = dto.CorreoElectronico,
            FechaIngreso = dto.FechaIngreso,
            Direccion = dto.Direccion,
            Member = dto.Member,
            Cargo = dto.Cargo,
            Rango = dto.Rango,
            Estatus = dto.Estatus,
            FechaNacimiento = dto.FechaNacimiento,
            Cedula = dto.Cedula,
            RH = dto.RH,
            EPS = dto.EPS,
            Padrino = dto.Padrino,
            Foto = dto.Foto,
            ContactoEmergencia = dto.ContactoEmergencia,
            Ciudad = dto.Ciudad,
            Moto = dto.Moto,
            AnoModelo = dto.AnoModelo,
            Marca = dto.Marca,
            CilindrajeCC = dto.CilindrajeCC,
            PlacaMatricula = dto.PlacaMatricula,
            FechaExpedicionLicenciaConduccion = dto.FechaExpedicionLicenciaConduccion,
            FechaExpedicionSOAT = dto.FechaExpedicionSOAT
        };
    }
}
