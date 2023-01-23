using InPostApp.Server.Data;
using InPostApp.Shared.Models;
using InPostApp.Shared.ModelsDto;
using InPostApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace InPostApp.Server.Services.LockerService
{
    public class LockerService : ILockerService
    {
        private readonly DataContext _context;

        public LockerService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<LockerDto>>> GetLockers()
        {
            var response = new ServiceResponse<List<LockerDto>>();
            List<LockerDto> sdList = new();
            var r = await _context.Lockers.ToListAsync();

            foreach (var s in r)
            {
                LockerDto sd = new LockerDto
                {
                    Id = s.LockerId,
                    Name = s.Name,
                    City = s.City,

                };
                sdList.Add(sd);
            }
            response.Success = true;
            response.Message = "Successfully get shipment";
            response.Data = sdList;

            return response;
        }

        public Task<ServiceResponse<LockerDto>> GetLocker(int Id)
        {
            throw new NotImplementedException();
        }
        public async Task<ServiceResponse<LockerDto>> AddLocker(LockerDto lockerDto)
        {
            var response = new ServiceResponse<LockerDto>();

            var newLocker = new Locker
            {
                Name = lockerDto.Name,
                City = lockerDto.City

            };
            _context.Lockers.Add(newLocker);
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Successfully add Locker";
            response.Data = lockerDto;
            return response;
        }
    }
}
