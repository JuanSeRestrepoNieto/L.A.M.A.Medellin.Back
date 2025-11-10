namespace Aplicacion.Excepciones;

/// <summary>
/// Excepción lanzada cuando hay errores de lógica de negocio
/// </summary>
public class BusinessException : Exception
{
    public BusinessException(string message) : base(message)
    {
    }

    public BusinessException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}

