using Application.Common.Exceptions;
using System.Net;

namespace WebUI.MiddleWares
{
    // Middleware class for handling exceptions and unsafe URLs
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        // Constructor to initialize the middleware with the next delegate and logger
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // Invoke method to process HTTP requests
        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Check for unsafe URL first
                var url = context.Request.Query["url"].ToString() ?? context.Request.Body.ToString();

                // If the URL contains unsafe patterns, return a 400 Bad Request response
                if (!string.IsNullOrEmpty(url) && UrlGuard.IsUnsafeUrl(url))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsJsonAsync(new { Message = "Unsafe URL detected" });
                    return; // Exit the middleware to prevent further processing
                }

                // Continue processing the request
                await _next(context);
            }
            catch (Exception exception)
            {
                // Log the exception details for debugging purposes
                _logger.LogError(
                    exception.Message,      // Logs the error message
                    exception.StackTrace,    // Logs the stack trace for debugging
                    exception.InnerException, // Logs the inner exception if it exists
                    exception.InnerException?.Message // Logs the message of the inner exception if it exists
                );

                // Handle the exception and generate a proper response for the client
                await HandleException(context, exception);
            }
        }

        // Method to handle exceptions and return appropriate HTTP responses
        private async Task HandleException(HttpContext context, Exception exception)
        {
            // Set the response content type to JSON
            context.Response.ContentType = "application/json";

            // Determine the type of exception and set the appropriate HTTP status code and message
            switch (exception)
            {
                case ValidationException validationException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest; // 400 Bad Request
                    await context.Response.WriteAsJsonAsync(new { validationException.Message, validationException.Failures });
                    break;

                case NotFoundException notFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound; // 404 Not Found
                    await context.Response.WriteAsJsonAsync(new { notFoundException.Message });
                    break;

                case BadRequestException badRequestException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest; // 400 Bad Request
                    await context.Response.WriteAsJsonAsync(new { badRequestException.Message });
                    break;

                case UnauthorizedException unauthorizedException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized; // 401 Unauthorized
                    await context.Response.WriteAsJsonAsync(new { unauthorizedException.Message });
                    break;

                case ForbiddenException forbiddenException:
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden; // 403 Forbidden
                    await context.Response.WriteAsJsonAsync(new { forbiddenException.Message });
                    break;

                case UrlGuard.UnsafeUrlException unsafeUrlException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest; // 400 Bad Request for unsafe URLs
                    await context.Response.WriteAsJsonAsync(new { unsafeUrlException.Message });
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // 500 Internal Server Error
                    await context.Response.WriteAsJsonAsync(new { exception.Message });
                    break;
            }
        }
    }
}
