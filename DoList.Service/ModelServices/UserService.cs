using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DoList.Common.Dtos;
using DoList.Common.Models.User;
using DoList.Data.Entities;
using DoList.Data.Repositories;
using DoList.Service.ConvertToExtension;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DoList.Service.ModelServices
{
    public class UserService
    {
        public UserService(IUserRepository userRepository, IConfiguration configuration, UserManager<UserDto> userManager)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _userManager = userManager;
        }
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly UserManager<UserDto> _userManager;
        public async Task<string> Login(LoginUserModel model)
        {
            var user = await _userRepository.GetUserByUsername(model.Username);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            var passwordHasher = new PasswordHasher<Users>();

            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);
            if (passwordVerificationResult != PasswordVerificationResult.Success)
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

        public async Task<UserDto> AddUser(AddUserModel model)
        {
            await Check(model.Username);

            var user = new Users
            {
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Username = model.Username,
            };
            var passwordHash = new PasswordHasher<Users>().HashPassword(user, model.Password);
            user.Password = passwordHash;
            await _userRepository.AddUser(user);

           return user.ParseToModel();
        }




        private async Task Check(string username)
        {
            var user = await _userRepository.GetUserByUsername(username);
            if (user is not null) throw new Exception($"Username with {username} is exist");
        }
    }
}
