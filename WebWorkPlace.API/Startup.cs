using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using RabbitRequestModels;
using WebWorkPlace.API.Authorization.Pollecies;

namespace WebWorkPlace.API;

public static class Startup
{
    public static IServiceCollection RegisterServices(this IServiceCollection service)
    {
        return service
            .RegisterAuthentication()
            .RegisterAuthorization();
    }

    private static IServiceCollection RegisterAuthentication(this IServiceCollection service)
    {
        KeyMaster.SetKey();
        service
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer( co =>
            {
                co.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
                co.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = (ctx) =>
                    {
                        if (ctx.Request.Headers.ContainsKey("Authorization"))
                        {
                            ctx.Token = ctx.Request.Headers["Authorization"];
                        }
                        
                        return Task.CompletedTask;
                    }
                };
                co.Configuration = new OpenIdConnectConfiguration()
                {
                    SigningKeys =
                    {
                        new RsaSecurityKey(KeyMaster.GetRSAKey())
                    }
                };
                co.MapInboundClaims = false;
            });
        return service;
    }

    private static IServiceCollection RegisterAuthorization(this IServiceCollection service)
    {
        return service
            .AddAuthorization(options =>
            {
                options.AddPolicy(LoggedIn.Name, p => p.RequireClaim(LoggedIn.Name ,LoggedIn.Value));
                options.AddPolicy(NewsAccess.Name, p => p.RequireClaim(NewsAccess.Name ,NewsAccess.Value));
            });
    }
}