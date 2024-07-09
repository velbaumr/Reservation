using Microsoft.AspNetCore.Diagnostics;

namespace WebApi.Handlers;

public class ApiExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        await httpContext.Response
            .WriteAsJsonAsync(new { Errors = new { Detail = exception.Message } }, cancellationToken);

        return true;
    }
}