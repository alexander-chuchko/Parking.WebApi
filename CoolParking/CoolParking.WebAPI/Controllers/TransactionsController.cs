using CoolParking.WebAPI.Services.ParkingService;
using CoolParking.WebAPI.Services.VehicleService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoolParking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly IParkingService _parkingService;
        public TransactionsController(IVehicleService vehicleService, IParkingService parkingService)
        {
            this._vehicleService = vehicleService;
            this._parkingService = parkingService;
        }

    }
}
