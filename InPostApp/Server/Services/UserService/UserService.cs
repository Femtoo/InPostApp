using InPostApp.Server.Data;
using InPostApp.Shared.Models;
using InPostApp.Shared.ModelsDto;
using InPostApp.Shared;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace InPostApp.Server.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(DataContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public string getMyName()
        {
            var result = string.Empty;
            if (_contextAccessor.HttpContext != null)
            {
                result = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }
        public string getMyRole()
        {
            var result = string.Empty;
            if (_contextAccessor.HttpContext != null)
            {
                result = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            }
            return result;
        }
        public async Task<ServiceResponse<List<User>>> GetUsers()
        {
            var response = new ServiceResponse<List<User>>();

            response.Success = true;
            response.Message = "Successfully get users";
            response.Data = await _context.Users
                .Include(x => x.ReceivedShipments)
                .Include(x => x.SentShipments)
                .ToListAsync();

            return response;
        }
        public async Task<ServiceResponse<List<UserName>>> GetUsersName()
        {
            var response = new ServiceResponse<List<UserName>>();


            var users = await _context.Users
                .Include(x => x.ReceivedShipments)
                .Include(x => x.SentShipments)
                .ToListAsync();

            List<UserName> un = new();
            foreach (var user in users)
            {
                UserName u = new UserName
                {
                    Name = user.Name
                };
                un.Add(u);
            }

            response.Success = true;
            response.Message = "Successfully get users";
            response.Data = un;
            return response;
        }
        public async Task<ServiceResponse<User>> GetUser(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<User>>> AddUser(User user)
        {
            var response = new ServiceResponse<List<User>>();

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Successfully get users";
            response.Data = await _context.Users.ToListAsync(); ;
            return response;
        }

        public async Task<ServiceResponse<List<User>>> UpdateUser(User user)
        {
            var response = new ServiceResponse<List<User>>();
            var dbUser = await _context.Users.FindAsync(user.Id);
            if (dbUser == null)
            {
                response.Success = false;
                response.Message = "Update fail, User not found";
            }
            dbUser.Name = user.Name;
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.Place = user.Place;

            await _context.SaveChangesAsync();

            response.Message = "Update successfully";
            response.Data = await _context.Users.ToListAsync();

            return response;
        }

        public async Task<ServiceResponse<List<User>>> DeleteUser(int Id)
        {
            var response = new ServiceResponse<List<User>>();
            var dbUser = await _context.Users.FindAsync(Id);
            if (dbUser == null)
            {
                response.Success = false;
                response.Message = "Remove fail, User not found";
            }

            _context.Users.Remove(dbUser);
            await _context.SaveChangesAsync();

            response.Message = "Remove successfully";
            response.Data = await _context.Users.ToListAsync();

            return response;
        }


    }
}
