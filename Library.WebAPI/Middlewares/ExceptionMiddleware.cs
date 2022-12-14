using Library.WebAPI.Common;
using Library.WebAPI.Exceptions;
using System.Net;

namespace Library.WebAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

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
                _logger.LogError($"Nešto se dogodilo: {ex}");
                await HandleException(context, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            string message = "Internal server error";

            switch (exception)
            {
                case NoBookException ex:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    message = ex.Message;
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            //pretvori objekat iz JSON u string pomocu overrided metode ToString iz Klase ErrorDetail
            return context.Response.WriteAsync(new ErrorDetail { Message = message}.ToString());
        }
    }
}
