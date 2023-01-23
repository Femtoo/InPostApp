using InPostApp.Shared.ModelsDto;
using InPostApp.Shared;

namespace InPostApp.Server.Services.ShipmentService
{
    public interface IShipmentService
    {
        Task<ServiceResponse<List<ShipmentDto>>> GetShipments();
        Task<ServiceResponse<ShipmentDto>> GetShipment(int Id);
        Task<ServiceResponse<ShipmentDto>> AddShipment(ShipmentDto shipmentDto);
        Task<ServiceResponse<ShipmentDto>> UpdateShipment(ShipmentDto shipment);
        Task<ServiceResponse<string>> DeleteShipment(int Id);
    }
}
