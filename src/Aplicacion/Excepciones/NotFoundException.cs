namespace Aplicacion.Excepciones;

/// <summary>
/// Excepción lanzada cuando un recurso no es encontrado
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string resourceName, object key) 
        : base($"El recurso '{resourceName}' con el identificador '{key}' no fue encontrado.")
    {
    }
}

