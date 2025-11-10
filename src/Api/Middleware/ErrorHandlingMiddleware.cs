using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Api.DTOs;
using Aplicacion.Excepciones;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api.Middleware;

/// <summary>
/// Middleware para manejo global de errores, logging y respuestas estandarizadas
/// </summary>
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var traceId = context.TraceIdentifier;
        var stopwatch = Stopwatch.StartNew();

        try
        {
            await _next(context);

            stopwatch.Stop();
            var duration = stopwatch.ElapsedMilliseconds;

            // Solo loggear requests lentos o con errores para reducir overhead
            // En producción, considera aumentar este threshold o deshabilitar completamente
            if (duration > 1000 || context.Response.StatusCode >= 400)
            {
                _logger.LogInformation(
                    "Request completado: {Method} {Path} | Status: {StatusCode} | Duration: {Duration}ms | TraceId: {TraceId}",
                    context.Request.Method,
                    context.Request.Path,
                    context.Response.StatusCode,
                    duration,
                    traceId
                );
            }
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            var duration = stopwatch.ElapsedMilliseconds;
            await HandleExceptionAsync(context, ex, traceId, duration);
        }
    }


    private async Task HandleExceptionAsync(
        HttpContext context, 
        Exception exception, 
        string traceId,
        double duration)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        var errorResponse = new ApiErrorResponse
        {
            TraceId = traceId,
            Timestamp = DateTime.UtcNow
        };

        switch (exception)
        {
            case NotFoundException notFoundEx:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse.Message = notFoundEx.Message;
                errorResponse.Type = "NotFoundException";
                _logger.LogWarning(
                    exception,
                    "Recurso no encontrado: {Message} | TraceId: {TraceId} | Duration: {Duration}ms",
                    notFoundEx.Message,
                    traceId,
                    duration
                );
                break;

            case ValidacionException validationEx:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Message = validationEx.Message;
                errorResponse.Errors = validationEx.Errores;
                errorResponse.Type = "ValidacionException";
                _logger.LogWarning(
                    exception,
                    "Error de validación: {Message} | TraceId: {TraceId} | Duration: {Duration}ms",
                    validationEx.Message,
                    traceId,
                    duration
                );
                break;

            case BusinessException businessEx:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Message = businessEx.Message;
                errorResponse.Type = "BusinessException";
                _logger.LogWarning(
                    exception,
                    "Error de negocio: {Message} | TraceId: {TraceId} | Duration: {Duration}ms",
                    businessEx.Message,
                    traceId,
                    duration
                );
                break;

            // DbUpdateConcurrencyException debe ir ANTES de DbUpdateException porque hereda de él
            case DbUpdateConcurrencyException concurrencyEx:
                response.StatusCode = (int)HttpStatusCode.Conflict;
                errorResponse.Message = "El recurso ha sido modificado por otro usuario. Por favor, actualice y vuelva a intentar.";
                errorResponse.Type = "ConcurrencyException";
                _logger.LogWarning(
                    exception,
                    "Error de concurrencia | TraceId: {TraceId} | Duration: {Duration}ms",
                    traceId,
                    duration
                );
                break;

            case DbUpdateException dbEx:
                // Manejar errores de Entity Framework
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                
                // Detectar errores específicos de SQL Server
                var innerException = dbEx.InnerException;
                if (innerException != null)
                {
                    var errorMessage = innerException.Message;
                    
                    // Detectar violación de clave única
                    if (errorMessage.Contains("UNIQUE") || errorMessage.Contains("duplicate key"))
                    {
                        errorResponse.Message = "Ya existe un registro con estos datos. Por favor, verifique la información.";
                        errorResponse.Type = "DuplicateKeyException";
                        _logger.LogWarning(
                            exception,
                            "Intento de crear registro duplicado | TraceId: {TraceId} | Duration: {Duration}ms",
                            traceId,
                            duration
                        );
                    }
                    // Detectar violación de foreign key
                    else if (errorMessage.Contains("FOREIGN KEY") || errorMessage.Contains("reference constraint"))
                    {
                        errorResponse.Message = "No se puede realizar la operación debido a referencias existentes.";
                        errorResponse.Type = "ForeignKeyViolationException";
                        _logger.LogWarning(
                            exception,
                            "Violación de foreign key | TraceId: {TraceId} | Duration: {Duration}ms",
                            traceId,
                            duration
                        );
                    }
                    // Detectar violación de constraint
                    else if (errorMessage.Contains("CHECK constraint") || errorMessage.Contains("constraint"))
                    {
                        errorResponse.Message = "Los datos proporcionados no cumplen con las restricciones de la base de datos.";
                        errorResponse.Type = "ConstraintViolationException";
                        _logger.LogWarning(
                            exception,
                            "Violación de constraint | TraceId: {TraceId} | Duration: {Duration}ms",
                            traceId,
                            duration
                        );
                    }
                    else
                    {
                        errorResponse.Message = "Error al guardar los datos en la base de datos.";
                        errorResponse.Type = "DatabaseException";
                        _logger.LogError(
                            exception,
                            "Error de base de datos: {Message} | TraceId: {TraceId} | Duration: {Duration}ms",
                            errorMessage,
                            traceId,
                            duration
                        );
                    }
                }
                else
                {
                    errorResponse.Message = "Error al guardar los datos en la base de datos.";
                    errorResponse.Type = "DatabaseException";
                    _logger.LogError(
                        exception,
                        "Error de base de datos | TraceId: {TraceId} | Duration: {Duration}ms",
                        traceId,
                        duration
                    );
                }
                break;

            case TimeoutException:
                response.StatusCode = (int)HttpStatusCode.RequestTimeout;
                errorResponse.Message = "La operación ha excedido el tiempo de espera. Por favor, intente nuevamente.";
                errorResponse.Type = "TimeoutException";
                _logger.LogWarning(
                    exception,
                    "Timeout en operación | TraceId: {TraceId} | Duration: {Duration}ms",
                    traceId,
                    duration
                );
                break;

            case TaskCanceledException taskEx when taskEx.InnerException is TimeoutException:
                response.StatusCode = (int)HttpStatusCode.RequestTimeout;
                errorResponse.Message = "La operación ha excedido el tiempo de espera. Por favor, intente nuevamente.";
                errorResponse.Type = "TimeoutException";
                _logger.LogWarning(
                    exception,
                    "Timeout en operación (TaskCanceled) | TraceId: {TraceId} | Duration: {Duration}ms",
                    traceId,
                    duration
                );
                break;

            // ArgumentNullException debe ir ANTES de ArgumentException porque hereda de él
            case ArgumentNullException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Message = exception.Message;
                errorResponse.Type = exception.GetType().Name;
                _logger.LogWarning(
                    exception,
                    "Argumento nulo: {Message} | TraceId: {TraceId} | Duration: {Duration}ms",
                    exception.Message,
                    traceId,
                    duration
                );
                break;

            case ArgumentException argEx:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Message = exception.Message;
                errorResponse.Type = exception.GetType().Name;
                _logger.LogWarning(
                    exception,
                    "Argumento inválido: {Message} | TraceId: {TraceId} | Duration: {Duration}ms",
                    exception.Message,
                    traceId,
                    duration
                );
                break;

            case UnauthorizedAccessException:
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                errorResponse.Message = "No autorizado para realizar esta operación";
                errorResponse.Type = "UnauthorizedAccessException";
                _logger.LogWarning(
                    exception,
                    "Acceso no autorizado | TraceId: {TraceId} | Duration: {Duration}ms",
                    traceId,
                    duration
                );
                break;

            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.Message = "Ha ocurrido un error interno en el servidor";
                errorResponse.Type = "InternalServerError";
                
                // En desarrollo, incluir más detalles
                if (context.RequestServices.GetService<IWebHostEnvironment>()?.IsDevelopment() == true)
                {
                    errorResponse.Detail = exception.ToString();
                }

                _logger.LogError(
                    exception,
                    "Error no manejado: {Message} | TraceId: {TraceId} | Duration: {Duration}ms | StackTrace: {StackTrace}",
                    exception.Message,
                    traceId,
                    duration,
                    exception.StackTrace
                );
                break;
        }

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        var jsonResponse = JsonSerializer.Serialize(errorResponse, jsonOptions);
        await response.WriteAsync(jsonResponse);
    }
}

