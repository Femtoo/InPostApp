using InPostApp.Server.Services.UserService;
using InPostApp.Shared.Models;
using InPostApp.Shared.ModelsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InPostApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetMe"), Authorize]
        public ActionResult<string> GetMe()
        {
            return Ok(_userService.getMyName());
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var response = await _userService.GetUsers();
            if (!response.Success)
            {
                return BadRequest(response.Data);
            }
            return Ok(response.Data);
        }

        [HttpGet("users-name")]
        public async Task<ActionResult<List<UserName>>> GetUsersName()
        {
            var response = await _userService.GetUsersName();
            if (!response.Success)
            {
                return BadRequest(response.Data);
            }
            return Ok(response.Data.ToArray());
        }

        [HttpPost]
        public async Task<ActionResult<List<User>>> AddUser(User user)
        {
            var response = await _userService.AddUser(user);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }

        [HttpPut]
        public async Task<ActionResult<List<User>>> Updateuser(User user)
        {
            var response = await _userService.UpdateUser(user);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }

        [HttpDelete]
        public async Task<ActionResult<List<User>>> DeleteUser(int Id)
        {
            var response = await _userService.DeleteUser(Id);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }
    }
}
