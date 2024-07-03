using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DoList.Common.Models.User;
using DoList.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DoList.Service.ModelServices
{
    public class UserService
    {
        public UserService(AppDbContext appDbContext, IConfiguration configuration)
        {
            _dbContext = appDbContext;
            _configuration = configuration;
        }
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public async Task<string> Login(LoginUserModel model)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, model.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

    }
}
