using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MetroClimate.Data.Configurations;
using MetroClimate.Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MetroClimate.Services.Services;

public interface IJwtService
{
    Task<string?> GenerateJwtToken(int userId);
    
}

public class JwtService : IJwtService
{
    private readonly IOptionsMonitor<JwtSettings> _jwtSettings;
    
    public JwtService(IOptionsMonitor<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }
    

    public async Task<string?> GenerateJwtToken(int userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.CurrentValue.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", userId.ToString()) }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return await Task.FromResult(tokenHandler.WriteToken(token));
    }
    
}