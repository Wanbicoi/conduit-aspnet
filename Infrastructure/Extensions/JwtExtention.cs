using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RealWorld.Infrastructure.Security;

namespace RealWorld.Infrastructure.Extensions;
public static class ExceptionMiddlewareExtensions
{
    public static void AddJwtAuthentication(this IServiceCollection services)
    {
        services.AddOptions();

        var signingKey = new SymmetricSecurityKey(
                System.Text.Encoding.ASCII.GetBytes("somethinglongerforthisdumbalgorithmisrequired"));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        var issuer = "issuer";
        var audience = "audience";

        services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = issuer;
                options.Audience = audience;
                options.SigningCredentials = signingCredentials;
            });
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // The signing key must match!
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingCredentials.Key,
                    // Validate the JWT Issuer (iss) claim
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    // Validate the JWT Audience (aud) claim
                    ValidateAudience = true,
                    ValidAudience = audience,
                    // Validate the token expiry
                    ValidateLifetime = true,
                    // If you want to allow a certain amount of clock drift, set that here:
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = (context) =>
                    {
                        var token = context.HttpContext.Request.Headers["Authorization"];
                        if (token.Count > 0 && token[0]!.StartsWith("Token ", StringComparison.OrdinalIgnoreCase))
                        {
                            context.Token = token[0]!.Substring("Token ".Length).Trim();
                        }

                        return Task.CompletedTask;
                    }
                };
            });
    }
}
