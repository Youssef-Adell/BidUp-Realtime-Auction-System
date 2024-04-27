using System.Net;
using BidUp.BusinessLogic.DTOs.CommonDTOs;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;

namespace BidUp.Presentation.Infrastructure;

internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        Log.Error(exception.ToString());

        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(new ErrorResponse(ErrorCode.SERVER_INTENRAL_ERROR, "We're experiencing technical issues. Try again later."), cancellationToken);

        return true;
    }

}