using DiegoAlvarez.Application.DTOs.Common;
using DiegoAlvarez.Application.Exceptions;

namespace DiegoAlvarez.Persistence.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex switch
            {
                ValidationException => StatusCodes.Status400BadRequest,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                NotFoundException => StatusCodes.Status404NotFound,
                ConflictException => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            var message = context.Response.StatusCode == StatusCodes.Status500InternalServerError
                ? "Error interno del servidor."
                : ex.Message;

            await context.Response.WriteAsJsonAsync(new MessageResponseDto(message));
        }
    }
}
