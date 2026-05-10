using ECommerce.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;
using ApplicationValidationException = ECommerce.Application.Common.Exceptions.ValidationException;

namespace ECommerce.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
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
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        object response;
        HttpStatusCode statusCode;

        switch (exception)
        {
            case ApplicationValidationException validationEx:
                statusCode = HttpStatusCode.BadRequest;
                response = new
                {
                    status = (int)statusCode,
                    title = "Validation failed",
                    errors = validationEx.Errors
                };
                break;

            case NotFoundException notFoundEx:
                statusCode = HttpStatusCode.NotFound;
                response = new
                {
                    status = (int)statusCode,
                    title = "Resource not found",
                    detail = notFoundEx.Message
                };
                break;

            case UnauthorizedException unauthorizedEx:
                statusCode = HttpStatusCode.Unauthorized;
                response = new
                {
                    status = (int)statusCode,
                    title = "Unauthorized",
                    detail = unauthorizedEx.Message
                };
                break;

            case InvalidOperationException invalidOpEx:
                statusCode = HttpStatusCode.BadRequest;
                response = new
                {
                    status = (int)statusCode,
                    title = "Invalid operation",
                    detail = invalidOpEx.Message
                };
                break;

            default:
                _logger.LogError(exception, "Unhandled exception");
                statusCode = HttpStatusCode.InternalServerError;
                response = new
                {
                    status = (int)statusCode,
                    title = "An unexpected error occurred",
                    detail = "Please try again later."
                };
                break;
        }

        context.Response.StatusCode = (int)statusCode;
        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        await context.Response.WriteAsync(json);
    }
}