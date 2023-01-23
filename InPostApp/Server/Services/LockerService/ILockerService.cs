using InPostApp.Shared.ModelsDto;
using InPostApp.Shared;

namespace InPostApp.Server.Services.LockerService
{
    public interface ILockerService
    {
        Task<ServiceResponse<List<LockerDto>>> GetLockers();
        Task<ServiceResponse<LockerDto>> GetLocker(int Id);
        Task<ServiceResponse<LockerDto>> AddLocker(LockerDto lockerDto);
    }
}
