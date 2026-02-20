using PatientApi.Logic.Models.Exceptions;
using System.Net;
using System.Text.Json;

namespace PatientApi.Web.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = exception switch
            {
                AppValidationException => (int)HttpStatusCode.BadRequest,
                AppNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };

            IDictionary<string, string[]> errors = null;

            switch (exception)
            {
                case AppValidationException e:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errors = e.Errors;
                    break;
                case AppNotFoundException e:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message,
                Errors = errors,
                //#if DEBUG
                //        StackTrace = exception.StackTrace
                //#endif
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
