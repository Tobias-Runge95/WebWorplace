using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RabbitRequestModels;
using RabbitRequestModels.NewsAPI.Awnsers;
using WebWorkPlace.API.Authorization.Pollecies;

namespace WebWorkPlace.API.Controllers;

[ApiController, Route("controller")]
public class AuthenticationController : ControllerBase
{
    [HttpGet("/login")]
    public async Task<IActionResult> Login()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new RsaSecurityKey(KeyMaster.GetRSAKey());
        var claimsIdentity = new ClaimsIdentity(new Claim[]
        {
            new Claim(LoggedIn.Name, LoggedIn.Value),
            new Claim(NewsAccess.Name, NewsAccess.Value)
        });
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = claimsIdentity,
            Expires = DateTime.UtcNow.AddMinutes(20),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha512)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);
        return Ok(jwt);
    }
}