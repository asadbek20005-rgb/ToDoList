using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Blazored.LocalStorage;
using DoList.Common.Dtos;
using DoList.Common.Models.User;
using DoList.Common.Statics;
using DoList.Data.Entities;
using DoList.Data.Repositories;
using DoList.Service.ConvertToExtension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DoList.Service.ModelServices
{
    public class UserService
    {
        public UserService(IUserRepository userRepository, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
         
        }
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public async Task<string> Login(LoginUserModel model)
        {
            var user = await _userRepository.GetUserByUsername(model.Username);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password or you should register first");
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

             _httpContextAccessor.HttpContext.Session.SetString("UserId", user.Id.ToString());
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
                Role = Role.User
            };

            var passwordHash = new PasswordHasher<Users>().HashPassword(user, model.Password);
            user.Password = passwordHash;
            user.ConfirmPassword = model.ConfirmPassword;
            await _userRepository.AddUser(user);
            return user.ParseToModel();
        }




        public async Task<UserDto> UpdateUser(Guid userId, UpdateUserModel model)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user is not null)
            {
                user.Firstname = model.Firstname;
                user.Lastname = model.Lastname;
                user.Username = model.Username;

                await _userRepository.UpdateUser(user);

                return user.ParseToModel();
            }
            throw new KeyNotFoundException($"User with ID {userId} not found");
        }



        public async Task<string> DeleteUser(Guid userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user is not null)
            {
                await _userRepository.DeleteUser(user);
                return $"{userId} is deleted successfully";
            }
            throw new Exception($"User with id {userId} not found.");

        }


        public async Task<UserDto> GetUser(Guid userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if(user is null)
                throw new Exception($"User with id {userId} not found.");
            return user.ParseToModel();
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers()??
                null; 
            return users.ParseToModels();
        }







        private async Task Check(string username)
        {
            var user = await _userRepository.GetUserByUsername(username);
            if (user is not null) throw new Exception($"Username with {username} is exist");
        }
    }
}
