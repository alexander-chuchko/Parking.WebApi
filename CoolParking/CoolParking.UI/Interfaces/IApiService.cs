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
        Task<VehicleDTO> AddVehicle(VehicleDTO vehicles);
        Task<IEnumerable<VehicleDTO>> GetAllVehicleses();
        Task<VehicleDTO> GetByIdVehicle(string id);
        Task DeleteVehicle(string id);
        Task<TransactionInfoDTO[]> GetLastTransaction();
        Task<string> GetTransactionAll();
        Task<VehicleDTO> TopUpVehicle(string id, decimal sum);
        Task<int> GetCapacityParking();
        Task<int> GetFreePlacesParking();
        Task<decimal> GetBalanceParking();
    }
}
