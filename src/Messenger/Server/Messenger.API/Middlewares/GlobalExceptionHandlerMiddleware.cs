using Messenger.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using ValidationException = Messenger.Domain.Exceptions.ValidationException;

namespace Messenger.API.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
            => _next = next;

        public async Task InvokeAsync(
            HttpContext httpContext,
            [FromServices] ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException validationException)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                logger.LogError(validationException, validationException.Message);

                await HandleExceptionAsync(httpContext, validationException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

                var serializedObject = JsonSerializer.Serialize(new
                {
                    notFoundException.Message
                });

                logger.LogError(notFoundException, notFoundException.Message);

                await HandleExceptionAsync(httpContext, serializedObject);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, exception.Message);

                await HandleExceptionAsync(httpContext, exception.Message);
            }
        }

        private async Task HandleExceptionAsync(
            HttpContext context,
            string message)
        {
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(message);
        }
    }
}
