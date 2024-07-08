﻿using Microsoft.AspNetCore.Diagnostics;

namespace WebApi.Filters
{
    public class ApiExceptionFilter : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await httpContext.Response
                .WriteAsJsonAsync(new { Errors = new { Detail = "Server error"}}, cancellationToken);

            return true;
        }
    }
}