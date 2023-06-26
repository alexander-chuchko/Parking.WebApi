using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CoolParking.BL.Models;

namespace CoolParking.BL.Interfaces
{
    public interface IApiService
    {
        Task<Vehicle> AddVehicle(Vehicle vehicles);
        Task<IEnumerable<Vehicle>> GetAllVehicleses();
        Task<Vehicle> GetByIdVehicle(string id);
        Task DeleteVehicle(string id);
        Task<TransactionInfo[]> GetLastTransaction();
        Task<string> GetTransactionAll();
        Task<Vehicle> TopUpVehicle(string id, decimal sum);
        Task<int> GetCapacityParking();
        Task<int> GetFreePlacesParking();
        Task<decimal> GetBalanceParking();
    }
}
