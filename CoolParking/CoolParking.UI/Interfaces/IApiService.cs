using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CoolParking.BL.Models;
using CoolParking.Common.DTO;

namespace CoolParking.BL.Interfaces
{
    public interface IApiService
    {
        Task<VehicleDTO> AddVehicleAsync(VehicleDTO vehicles);
        Task<IEnumerable<VehicleDTO>> GetAllVehiclesesAsync();
        Task<VehicleDTO> GetByIdVehicleAsync(string id);
        Task DeleteVehicleAsync(string id);
        Task<TransactionInfoDTO[]> GetLastTransactionAsync();
        Task<string> GetTransactionAllAsync();
        Task<VehicleDTO> TopUpVehicleAsync(string id, decimal sum);
        Task<int> GetCapacityParkingAsync();
        Task<int> GetFreePlacesParkingAsync();
        Task<decimal> GetBalanceParkingAsync();
    }
}
