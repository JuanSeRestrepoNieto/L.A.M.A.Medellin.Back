namespace Aplicacion.Excepciones;

/// <summary>
/// Excepción lanzada cuando hay errores de validación
/// </summary>
public class ValidacionException : Exception
{
    public Dictionary<string, string[]> Errores { get; }

    public ValidacionException(Dictionary<string, string[]> errores) 
        : base("Se encontraron errores de validación")
    {
        Errores = errores;
    }

    public ValidacionException(string campo, string mensaje) 
        : base("Se encontraron errores de validación")
    {
        Errores = new Dictionary<string, string[]>
        {
            { campo, new[] { mensaje } }
        };
    }
}

