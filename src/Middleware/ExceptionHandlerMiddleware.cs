using Microsoft.AspNetCore.Http;
using MVC.Boilerplate.Exceptions;
using System.Net;

namespace MVC.Boilerplate.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next; _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "", null);
                
                context.Response.Redirect("ErrorHandler/" + ConvertException(context, ex));
            }
        }

        private int ConvertException(HttpContext context, Exception exception)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            switch (exception)
            {
                //case ValidationException validationException:
                //    httpStatusCode = HttpStatusCode.BadRequest;
                //    result = GetErrorMessages(validationException.ValdationErrors);
                //    break;
                case BadRequestException badRequestException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;
                case NotFoundException notFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    break;
                case ApplicationException appexception:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;
                case Exception ex:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;
            }
            return (int)httpStatusCode; 
        }
    }
}
