using System.ComponentModel.DataAnnotations;

namespace Aplicacion.DTOs;

public class MiembroFiltrosDto
{
    [Range(1, int.MaxValue, ErrorMessage = "Page debe ser mayor a 0")]
    public int Page { get; set; } = 1;

    [Range(1, 100, ErrorMessage = "PageSize debe estar entre 1 y 100")]
    public int PageSize { get; set; } = 10;

    public string? Estatus { get; set; }
    public string? Rango { get; set; }
    public string? Cargo { get; set; }

    public bool TieneFiltros()
    {
        return !string.IsNullOrWhiteSpace(Estatus) ||
               !string.IsNullOrWhiteSpace(Rango) ||
               !string.IsNullOrWhiteSpace(Cargo);
    }
}

