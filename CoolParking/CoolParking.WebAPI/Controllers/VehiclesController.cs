using CoolParking.WebAPI.Services.ParkingService;
using CoolParking.WebAPI.Services.VehicleService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoolParking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly IParkingService _parkingService;
        public VehiclesController(IVehicleService vehicleService, IParkingService parkingService)
        {
            this._vehicleService = vehicleService;
            this._parkingService = parkingService;
        }

        //api/parking/balance
        [HttpGet("balance")]
        public ActionResult<decimal> GetBalance() //Tested
        {
            return Ok(_parkingService.GetBalance());
        }
    }
}
