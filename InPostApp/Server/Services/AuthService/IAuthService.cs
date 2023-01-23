using InPostApp.Shared.Models;
using InPostApp.Shared.ModelsDto;
using InPostApp.Shared;

namespace InPostApp.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<User>> Register(UserDto request);
        Task<ServiceResponse<string>> Login(UserDto request);
    }
}
