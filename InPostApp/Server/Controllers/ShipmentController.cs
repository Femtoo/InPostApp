using InPostApp.Server.Services.ShipmentService;
using InPostApp.Shared.ModelsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InPostApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User,Admin")]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;

        public ShipmentController(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<ShipmentDto>>> GetShipments()
        {
            var response = await _shipmentService.GetShipments();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response.Data.ToArray());
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ShipmentDto>> GetShipment(int Id)
        {
            var response = await _shipmentService.GetShipment(Id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response.Data);
        }
        [HttpPost]
        public async Task<ActionResult<ShipmentDto>> AddShipment(ShipmentDto shipmentDto)
        {
            var response = await _shipmentService.AddShipment(shipmentDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response.Data);
        }
        //[HttpPost]
        //public async Task<ActionResult<string>> AddShipment(AddShipmentDto shipmentDto)
        //{
        //    var response = await _shipmentService.AddShipment(shipmentDto);
        //    if (!response.Success)
        //    {
        //        return BadRequest(response);
        //    }
        //    return Ok(response.Data);
        //}
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ShipmentDto>> UpdateShipment(ShipmentDto shipmentDto)
        {
            var response = await _shipmentService.UpdateShipment(shipmentDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response.Data);
        }
    }
}
