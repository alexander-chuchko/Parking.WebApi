using CoolParking.Common.DTO;

namespace CoolParking.Common.Interfaces
{
    public interface IVehicleService
    {
        VehicleDTO GetVehicleById(string id);
        void AddVehicle(VehicleDTO vehicleDTO);
        IEnumerable<VehicleDTO> GetVehicles();
    }
}
