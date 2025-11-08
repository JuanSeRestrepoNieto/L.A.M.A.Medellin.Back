using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infraestructura.DataEntities;

[Table("TB_MIEMBROS")]
public class DataModelMiembro
{
  [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  [Column("Id", TypeName = "INT")]
  public int Id { get; set; }
  [Column("Nombre", TypeName = "VARCHAR(100)")]
  public string? Nombre { get; set; }
  [Column("Apellido", TypeName = "VARCHAR(100)")]
  public string? Apellido { get; set; }
  [Column("Celular", TypeName = "VARCHAR(20)")]
  public string? Celular { get; set; }
  [Column("CorreoElectronico", TypeName = "VARCHAR(100)")]
  public string? CorreoElectronico { get; set; }
  [Column("FechaIngreso", TypeName = "DATE")]
  public DateTime? FechaIngreso { get; set; }
  [Column("Direccion", TypeName = "VARCHAR(255)")]
  public string? Direccion { get; set; }
  [Column("Member", TypeName = "INT")]
  public int? MemberNumber { get; set; }
  [Column("Cargo", TypeName = "VARCHAR(100)")]
  public string? Cargo { get; set; }
  [Column("Rango", TypeName = "VARCHAR(100)")]
  public string? Rango { get; set; }
  [Column("Estatus", TypeName = "VARCHAR(50)")]
  public string? Estatus { get; set; }
  [Column("FechaNacimiento", TypeName = "DATE")]
  public DateTime? FechaNacimiento { get; set; }
  [Column("Cedula", TypeName = "VARCHAR(20)")]
  public string? Cedula { get; set; }
  [Column("RH", TypeName = "VARCHAR(5)")]
  public string? RH { get; set; }
  [Column("EPS", TypeName = "VARCHAR(100)")]
  public string? EPS { get; set; }
  [Column("Padrino", TypeName = "VARCHAR(100)")]
  public string? Padrino { get; set; }
  [Column("Foto", TypeName = "VARCHAR(255)")]
  public string? Foto { get; set; }
  [Column("ContactoEmergencia", TypeName = "VARCHAR(100)")]
  public string? ContactoEmergencia { get; set; }
  [Column("Ciudad", TypeName = "VARCHAR(100)")]
  public string? Ciudad { get; set; }
  [Column("Moto", TypeName = "VARCHAR(100)")]
  public string? Moto { get; set; }
  [Column("AnoModelo", TypeName = "INT")]
  public int? AnoModelo { get; set; }
  [Column("Marca", TypeName = "VARCHAR(100)")]
  public string? Marca { get; set; }
  [Column("CilindrajeCC", TypeName = "INT")]
  public int? CilindrajeCC { get; set; }
  [Column("PlacaMatricula", TypeName = "VARCHAR(20)")]
  public string? PlacaMatricula { get; set; }
  [Column("FechaExpedicionLicenciaConduccion", TypeName = "DATE")]
  public DateTime? FechaExpedicionLicenciaConduccion { get; set; }
  [Column("FechaExpedicionSOAT", TypeName = "DATE")]
  public DateTime? FechaExpedicionSOAT { get; set; }
}