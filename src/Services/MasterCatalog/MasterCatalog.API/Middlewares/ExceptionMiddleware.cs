using System.Text.Json;
using MasterCatalog.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace MasterCatalog.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception non gérée : {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    public static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, title, errors) = exception switch
        {
            ValidationException validationEx => (
                StatusCodes.Status400BadRequest,
                "Erreur de validation",
                validationEx.Errors.Select(e => e.ErrorMessage).ToList()
            ),

            ProductNotFoundException => (
                StatusCodes.Status404NotFound,
                "Produit introuvable",
                new List<string> { exception.Message }
            ),

            DuplicateSkuException => (
                StatusCodes.Status409Conflict,
                "SKU déjà existant",
                new List<string> { exception.Message }
            ),

            ArgumentException => (
                StatusCodes.Status400BadRequest,
                "Requête invalide",
                new List<string> { exception.Message }
            ),

            _ => (
                StatusCodes.Status500InternalServerError,
                "Erreur interne du serveur",
                new List<string> { "Une erreur inattendue s'est produite." }
            )
        };

        context.Response.StatusCode = statusCode;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Extensions = { ["errors"] = errors }
        };

        var json = JsonSerializer.Serialize(problemDetails, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }
}