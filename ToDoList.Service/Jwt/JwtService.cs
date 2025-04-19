using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoList.Common.Models;
using ToDoList.Data.Entites;

namespace ToDoList.Service.Jwt;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly JwtModel? _jwtSetting;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
        _jwtSetting = _configuration.GetSection("JwtSettings").Get<JwtModel>();
    }

    public string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        var token = new JwtSecurityToken(
            issuer: _jwtSetting.Issuer,
            audience: _jwtSetting.Audience,
            signingCredentials: cred,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

