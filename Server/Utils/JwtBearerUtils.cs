using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace KolibSoft.AuthStore.Server.Utils;

public static class JwtBearerUtils
{

    public static void AddJwtBearer(this IServiceCollection services, byte[] secret, string issuer = "*", string audience = "*")
    {
        services.AddAuthentication(options => options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(secret),
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidIssuer = issuer,
                        ValidateIssuer = true,
                        ValidAudience = audience,
                        ValidateAudience = true
                    };
                }); ;
    }

}