using Application.Common.Exceptions;
using System.Net;

namespace WebUI.MiddleWares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                var url = context.Request.Query["url"].ToString() ?? context.Request.Body.ToString();

                if (!string.IsNullOrEmpty(url) && UrlGuard.IsUnsafeUrl(url))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsJsonAsync(new { Message = "Unsafe URL detected" });
                    return; 
                }
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(
                    exception.Message,      
                    exception.StackTrace,    
                    exception.InnerException, 
                    exception.InnerException?.Message 
                );
                await HandleException(context, exception);
            }
        }
        private async Task HandleException(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            switch (exception)
            {
                case ValidationException validationException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsJsonAsync(new { validationException.Message, validationException.Failures });
                    break;

                case NotFoundException notFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    await context.Response.WriteAsJsonAsync(new { notFoundException.Message });
                    break;

                case BadRequestException badRequestException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest; 
                    await context.Response.WriteAsJsonAsync(new { badRequestException.Message });
                    break;

                case UnauthorizedException unauthorizedException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized; 
                    await context.Response.WriteAsJsonAsync(new { unauthorizedException.Message });
                    break;

                case ForbiddenException forbiddenException:
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden; 
                    await context.Response.WriteAsJsonAsync(new { forbiddenException.Message });
                    break;

                case UrlGuard.UnsafeUrlException unsafeUrlException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsJsonAsync(new { unsafeUrlException.Message });
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; 
                    await context.Response.WriteAsJsonAsync(new { exception.Message });
                    break;
            }
        }
    }
}