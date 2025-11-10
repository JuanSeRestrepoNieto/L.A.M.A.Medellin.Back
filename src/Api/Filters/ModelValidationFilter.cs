using System.Text.Json;
using Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Api.Filters;

/// <summary>
/// Filtro para manejar errores de validación del modelo y convertirlos en respuestas estandarizadas
/// </summary>
public class ModelValidationFilter : IActionFilter
{
    private readonly ILogger<ModelValidationFilter> _logger;

    public ModelValidationFilter(ILogger<ModelValidationFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = new Dictionary<string, string[]>();
            foreach (var key in context.ModelState.Keys)
            {
                var modelErrors = context.ModelState[key]?.Errors
                    .Select(e => e.ErrorMessage)
                    .ToArray() ?? Array.Empty<string>();
                
                if (modelErrors.Length > 0)
                {
                    errors[key] = modelErrors;
                }
            }

            var errorResponse = new ApiErrorResponse
            {
                Success = false,
                Message = "Error de validación en los datos proporcionados",
                Errors = errors,
                Type = "ValidationError",
                TraceId = context.HttpContext.TraceIdentifier,
                Timestamp = DateTime.UtcNow
            };

            _logger.LogWarning(
                "Error de validación del modelo: {Errors} | TraceId: {TraceId}",
                string.Join(", ", errors.SelectMany(e => e.Value)),
                context.HttpContext.TraceIdentifier
            );

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            };

            context.Result = new BadRequestObjectResult(errorResponse);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // No action needed
    }
}

