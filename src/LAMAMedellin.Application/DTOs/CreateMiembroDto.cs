namespace LAMAMedellin.Application.DTOs;

public class CreateMiembroDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Celular { get; set; } = string.Empty;
    public string CorreoElectronico { get; set; } = string.Empty;
    public DateTime? FechaIngreso { get; set; }
    public string Direccion { get; set; } = string.Empty;
    public int? Member { get; set; }
    public string Cargo { get; set; } = string.Empty;
    public string Rango { get; set; } = string.Empty;
    public string Estatus { get; set; } = string.Empty;
    public DateTime? FechaNacimiento { get; set; }
    public string Cedula { get; set; } = string.Empty;
    public string RH { get; set; } = string.Empty;
    public string EPS { get; set; } = string.Empty;
    public string Padrino { get; set; } = string.Empty;
    public string Foto { get; set; } = string.Empty;
    public string ContactoEmergencia { get; set; } = string.Empty;
    public string Ciudad { get; set; } = string.Empty;
    public string Moto { get; set; } = string.Empty;
    public int? AnoModelo { get; set; }
    public string Marca { get; set; } = string.Empty;
    public int? CilindrajeCC { get; set; }
    public string PlacaMatricula { get; set; } = string.Empty;
    public DateTime? FechaExpedicionLicenciaConduccion { get; set; }
    public DateTime? FechaExpedicionSOAT { get; set; }
}
