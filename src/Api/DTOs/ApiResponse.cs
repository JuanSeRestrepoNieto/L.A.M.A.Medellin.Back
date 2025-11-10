namespace Api.DTOs;

/// <summary>
/// Respuesta estandarizada de la API para operaciones exitosas
/// </summary>
public class ApiResponse<T>
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string? TraceId { get; set; }

    public static ApiResponse<T> SuccessResponse(T? data, string message = "Operación exitosa")
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data,
            Timestamp = DateTime.UtcNow
        };
    }
}

/// <summary>
/// Respuesta estandarizada de la API para errores
/// </summary>
public class ApiErrorResponse
{
    public bool Success { get; set; } = false;
    public string Message { get; set; } = string.Empty;
    public string? Detail { get; set; }
    public Dictionary<string, string[]>? Errors { get; set; }
    public string? TraceId { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string? Type { get; set; }

    public static ApiErrorResponse FromException(Exception exception, string? traceId = null)
    {
        return new ApiErrorResponse
        {
            Success = false,
            Message = exception.Message,
            Type = exception.GetType().Name,
            TraceId = traceId,
            Timestamp = DateTime.UtcNow
        };
    }
}

