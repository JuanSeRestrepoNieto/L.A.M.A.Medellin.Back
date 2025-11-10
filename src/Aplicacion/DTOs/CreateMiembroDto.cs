using System.ComponentModel.DataAnnotations;

namespace Aplicacion.DTOs;

public class CreateMiembroDto
{
    [Required]
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string? Celular { get; set; }
    [EmailAddress]
    public string? CorreoElectronico { get; set; }
    public DateTime? FechaIngreso { get; set; }
    public string? Direccion { get; set; }
    public int? MemberNumber { get; set; }
    public string? Cargo { get; set; }
    public string? Rango { get; set; }
    public string? Estatus { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public string? Cedula { get; set; }
    public string? RH { get; set; }
    public string? EPS { get; set; }
    public string? Padrino { get; set; }
    public string? Foto { get; set; }
    public string? ContactoEmergencia { get; set; }
    public string? Ciudad { get; set; }
    public string? Moto { get; set; }
    public int? AnoModelo { get; set; }
    public string? Marca { get; set; }
    public int? CilindrajeCC { get; set; }
    public string? PlacaMatricula { get; set; }
    public DateTime? FechaExpedicionLicenciaConduccion { get; set; }
    public DateTime? FechaExpedicionSOAT { get; set; }
}



