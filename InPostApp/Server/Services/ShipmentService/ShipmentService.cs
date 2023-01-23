using InPostApp.Server.Data;
using InPostApp.Shared.Models;
using InPostApp.Shared.ModelsDto;
using InPostApp.Shared;
using Microsoft.EntityFrameworkCore;
using InPostApp.Server.Services.UserService;

namespace InPostApp.Server.Services.ShipmentService
{
    public class ShipmentService : IShipmentService
    {

        private readonly DataContext _context;
        private readonly IUserService _userService;

        public ShipmentService(DataContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<ServiceResponse<List<ShipmentDto>>> GetShipments()
        {
            var response = new ServiceResponse<List<ShipmentDto>>();
            List<ShipmentDto> sdList = new();
            var name = _userService.getMyName();
            var role = _userService.getMyRole();
            var r = await _context.Shipments.Include(x => x.FromLocker).Include(x => x.ToLocker).Include(x => x.Receiver).Where(u => u.Sender.Name == name).ToListAsync();
            if (role == "Admin")
            {

                r = await _context.Shipments.Include(x => x.FromLocker).Include(x => x.ToLocker).Include(x => x.Receiver).ToListAsync();

            }
            //var notes = await _context.Notes.Where(p => p.Public == true && p.Secure == false).ToListAsync();
            //.Where(u => u.Sender.Name == name)

            foreach (var s in r)
            {
                string receiver = "";

                if (s.Receiver != null)
                {
                    receiver = s.Receiver.Name;

                }
                ShipmentDto sd = new ShipmentDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    State = s.State,
                    Receiver = receiver,
                    FromLockerName = s.FromLocker.Name,
                    ToLockerName = s.ToLocker.Name
                };
                sdList.Add(sd);
            }
            response.Success = true;
            response.Message = "Successfully get shipment";
            response.Data = sdList;

            return response;
        }
        //_context.Shipments.Select(s => _mapper.Map<GetShipmentDto>(s))
        public async Task<ServiceResponse<ShipmentDto>> GetShipment(int Id)
        {
            var response = new ServiceResponse<ShipmentDto>();

            var s = await _context.Shipments.Include(x => x.FromLocker).Include(x => x.ToLocker).Include(x => x.Receiver).FirstOrDefaultAsync(i => i.Id == Id);
            if (s == null)
            {
                response.Success = false;
                response.Message = "Shipment not found";
                return response;

            }
            string receiver = "";
            if (s.Receiver != null)
            {
                receiver = s.Receiver.Name;

            }
            ShipmentDto sd = new ShipmentDto
            {
                Id = s.Id,
                Name = s.Name,
                State = s.State,
                Receiver = receiver,
                FromLockerName = s.FromLocker.Name,
                ToLockerName = s.ToLocker.Name
            };
            response.Message = "Shipment successfully get";
            response.Data = sd;
            return response;
        }

        public async Task<ServiceResponse<ShipmentDto>> AddShipment(ShipmentDto shipmentDto)
        {
            var response = new ServiceResponse<ShipmentDto>();
            var user = _context.Users.Where(u => u.Name == _userService.getMyName()).FirstOrDefault();
            //var sender = await _context.Users.FindAsync(2);
            var receiver = _context.Users.Where(u => u.Name == shipmentDto.Receiver).FirstOrDefault();
            var lockerFrom = _context.Lockers.Where(u => u.Name == shipmentDto.FromLockerName).FirstOrDefault();
            var lockerTo = _context.Lockers.Where(u => u.Name == shipmentDto.ToLockerName).FirstOrDefault();

            _userService.getMyName();

            var newShipment = new Shipment
            {
                Name = shipmentDto.Name,
                State = shipmentDto.State,
                Sender = user,
                Receiver = receiver,
                FromLocker = lockerFrom,
                ToLocker = lockerTo
            };
            _context.Shipments.Add(newShipment);
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Successfully add shipment";
            response.Data = shipmentDto;
            return response;
        }
        public async Task<ServiceResponse<string>> DeleteShipment(int Id)
        {
            throw new NotImplementedException();
        }
        public async Task<ServiceResponse<ShipmentDto>> UpdateShipment(ShipmentDto shipment)
        {
            var response = new ServiceResponse<ShipmentDto>();

            var ship = await _context.Shipments.FirstOrDefaultAsync(i => i.Id == shipment.Id);
            if (ship == null)
            {
                response.Success = false;
                response.Message = "Shipment not found";
                return response;

            }

            ship.State = shipment.State;
            _context.Shipments.Update(ship);
            await _context.SaveChangesAsync();

            response.Data = shipment;
            return response;
        }
    }
}
