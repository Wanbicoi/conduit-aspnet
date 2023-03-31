using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace RealWorld.Infrastructure.Extensions;
public static class ErrorHandlingExtension
{
    public static void UseErrorHandling(this WebApplication app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        RestException => StatusCodes.Status422UnprocessableEntity,
                        RealWorld.Infrastructure.Validation.ValidationException => StatusCodes.Status422UnprocessableEntity,
                        Microsoft.IdentityModel.Tokens.SecurityTokenException => StatusCodes.Status401Unauthorized,
                        _ => StatusCodes.Status500InternalServerError,
                    };
                    if (contextFeature.Error is RestException exception)
                    {
                        await context.Response
                            .WriteAsync(JsonSerializer.Serialize(new { exception.Errors }));
                    }
                    if (contextFeature.Error is RealWorld.Infrastructure.Validation.ValidationException exception1)
                    {
                        await context.Response
                            .WriteAsync(JsonSerializer.Serialize(new { exception1.Errors }));
                    }
                    if (contextFeature.Error is Microsoft.IdentityModel.Tokens.SecurityTokenException exception2)
                    {
                        await context.Response
                            .WriteAsync(JsonSerializer.Serialize(new { exception2.Message }));
                    }
                }
            });
        });

    }
}
