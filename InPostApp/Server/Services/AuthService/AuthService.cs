using InPostApp.Server.Data;
using InPostApp.Shared;
using InPostApp.Shared.Models;
using InPostApp.Shared.ModelsDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace InPostApp.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly DataContext _context;

        public AuthService(IConfiguration configuration, DataContext context, IPasswordHasher<User> passwordHasher)
        {
            _configuration = configuration;
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<ServiceResponse<User>> Register(UserDto request)
        {
            var response = new ServiceResponse<User>();
            //var dbUser = await _context.Users.Where(i => i.Name == request.Username);
            var dbUser = await _context.Users.FirstOrDefaultAsync(i => i.Name == request.Username);
            if (dbUser != null)
            {
                response.Success = false;
                response.Message = "User exist";
                return response;
            }

            User user = new User
            {
                Name = request.Username,
            };
            var hashed = _passwordHasher.HashPassword(user, request.Password);
            user.PasswordHash = hashed;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            response.Data = user;
            response.Message = "Successfully register user";
            return response;
        }

        public async Task<ServiceResponse<string>> Login(UserDto request)
        {
            var response = new ServiceResponse<string>();
            var dbUser = await _context.Users.FirstOrDefaultAsync(i => i.Name == request.Username);
            if (dbUser == null)
            {
                response.Success = false;
                response.Message = "Login fail, User not found" + request.Username;
                return response;
            }
            var result = _passwordHasher.VerifyHashedPassword(dbUser, dbUser.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                response.Success = false;
                response.Message = "Wrong credentials";
                return response;
            }
            string token = CreateToken(dbUser);
            response.Success = true;
            response.Data = token;
            response.Message = "Successfull login";
            return response;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>();
            if (user.Name.Equals("Admin"))
            {
                claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Name)

            };
            } else
            {
                claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, "User")

            };
            }
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}
