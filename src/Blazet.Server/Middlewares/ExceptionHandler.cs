using System.Diagnostics;
using Blazet.Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Blazet.Server.Middlewares
{
    internal sealed class ExceptionHandler(ILogger<ExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
            CancellationToken cancellationToken)
        {
            var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

            logger.LogError(exception,
                $"Could not process a request on machine {Environment.MachineName}. TraceId: {traceId}");

            await GenerateProblemDetails(httpContext, traceId, exception);

            return true;
        }

        private static async Task GenerateProblemDetails(HttpContext httpContext,
            string traceId,
            Exception exception)
        {
            var (statusCode, title) = MapExceptionWithStatusCode(exception);

            await Results.Problem(title: title,
                statusCode: statusCode,
                extensions: new Dictionary<string, object?>
                {
                    {
                        "traceId", traceId
                    }
                }).ExecuteAsync(httpContext);

        }
        private static (int statusCode, string title) MapExceptionWithStatusCode(Exception exception)
        {
            if (exception is not ServerException && exception.InnerException != null)
                while (exception.InnerException != null)
                    exception = exception.InnerException;
            return exception switch
            {
                ArgumentOutOfRangeException => (StatusCodes.Status400BadRequest, exception.Message),
                ServerException => (StatusCodes.Status500InternalServerError, exception.Message),
                KeyNotFoundException => (StatusCodes.Status404NotFound, exception.Message),
                _ => (StatusCodes.Status500InternalServerError, "An error occured. Contact your system administrator.")
            };
        }

    }
}
