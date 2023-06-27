using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using CoolParking.Common.DTO;
using CoolParking.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoolParking.WebAPI.Controllers
{
    [Route("api/[controller]"), Produces("application/json")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IParkingService _parkingService;
        private readonly IVehicleService _vehicleService;
        public VehiclesController(IParkingService parkingService, IVehicleService vehicleService)
        {
            _parkingService = parkingService;
            _vehicleService = vehicleService;   
        }

        //api/vehicles
        [HttpGet]
        public ActionResult<IEnumerable<VehicleDTO>> GetAll() //Tested
        {
            //return Ok(_parkingService.GetVehicles());
            return Ok(_vehicleService.GetVehicles());
        }

        //api/vehicles/id 
        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<VehicleDTO> GetById(string id) //Tested
        {
            if (!Vehicle.IsValidId(id))
            {
                return BadRequest();
            }

            var vehicleDTO = _vehicleService.GetVehicleById(id);

            //var vehicle = _parkingService.GetVehicles().FirstOrDefault(v => v.Id == id);

            if (vehicleDTO == null)
            {
                return NotFound();
            }

            return Ok(vehicleDTO);
        }

        //api/vehicles
        [HttpPost]
        public IActionResult Add([FromBody] VehicleDTO vehicleDTO) //Tested
        {
            if (!Vehicle.IsValidId(vehicleDTO.Id) && vehicleDTO.Balance >= Settings.Tariffs[(int)vehicleDTO.VehicleType])
            {
                return BadRequest();
            }
            _vehicleService.AddVehicle(vehicleDTO);
            //_parkingService.AddVehicle(vehicle);

            return CreatedAtRoute("GetById", new { id = vehicleDTO.Id }, vehicleDTO);
        }

        //api/vehicles/id
        [HttpDelete("{id}")]
        public IActionResult Delete(string id) //Tested
        {

            var vehicle = _parkingService.GetVehicles().FirstOrDefault(v => v.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            if (!Vehicle.IsValidId(id) || vehicle.Balance < 0)
            {
                return BadRequest();
            }

            _parkingService.RemoveVehicle(id);

            return NoContent();
        }
    }
}
