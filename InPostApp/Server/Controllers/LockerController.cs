using InPostApp.Server.Services.LockerService;
using InPostApp.Shared.ModelsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InPostApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LockerController : ControllerBase
    {
        private readonly ILockerService _lockerService;

        public LockerController(ILockerService lockerService)
        {
            _lockerService = lockerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<LockerDto>>> GetLockers()
        {
            var response = await _lockerService.GetLockers();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response.Data.ToArray());
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<LockerDto>> AddLocker(LockerDto lockerDto)
        {
            var response = await _lockerService.AddLocker(lockerDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response.Data);
        }
    }
}
