using CoolParking.WebAPI.Models;
using CoolParking.WebAPI.Services.ParkingService;
using CoolParking.WebAPI.Services.VehicleService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoolParking.WebAPI.Controllers
{
    [Route("api/[controller]"), Produces("application/json")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly IParkingService _parkingService;
        public VehiclesController(IVehicleService vehicleService, IParkingService parkingService)
        {
            _vehicleService = vehicleService;
            _parkingService = parkingService;
        }

        //api/vehicles
        [HttpGet]
        public ActionResult<IEnumerable<Vehicle>> GetAll()
        {
            return Ok(_parkingService.GetVehicles());
        }

        //api/vehicles/id 
        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<Vehicle> GetById(string id)
        {
            if (!_vehicleService.IsValidRegistrationPlateNumber(id))
            {
                return BadRequest();
            }

            if (!_vehicleService.IsExists(id))
            {
                return NotFound();
            }

            return Ok(_parkingService.GetVehicleById(id));
        }


    }
}
