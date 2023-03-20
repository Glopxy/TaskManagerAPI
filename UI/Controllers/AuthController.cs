using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace UI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    string signingKey = "ThisIsSigningKey";
    [HttpGet]
    public string Get(string userName, string password)
    {
        var claims = new[]{
            new Claim(ClaimTypes.Name,userName),
            new Claim(JwtRegisteredClaimNames.Email,userName)
        };
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
        var credentials =new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
        
        var jwtSecurityToken = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.Now,
            signingCredentials: credentials,    
            expires: DateTime.Now.AddDays(15)

        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return token;
    }
    [HttpGet("ValidateToken")]
    public bool Validatetoken(string token)
    {
        var securityKey =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
        try
        {
            JwtSecurityTokenHandler handler = new();
            handler.ValidateToken(token,new TokenValidationParameters(){
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = false
                
                 
            }, out SecurityToken validetadToken);
            var jwtToken = (JwtSecurityToken)validetadToken;
            var claims = jwtToken.Claims.ToList();
            return true;
        }
        catch (System.Exception)
        {
            
            return false;
        }
    }
}