using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoolParking.WebAPI.Controllers
{
    [Route("api/[controller]"), Produces("application/json")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IParkingService _parkingService;
        public VehiclesController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        //api/vehicles
        [HttpGet]
        public ActionResult<IEnumerable<Vehicle>> GetAll() //Tested
        {
            return Ok(_parkingService.GetVehicles());
        }

        //api/vehicles/id 
        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<Vehicle> GetById(string id) //Tested
        {
            if (!Vehicle.IsValidId(id))
            {
                return BadRequest();
            }

            var vehicle = _parkingService.GetVehicles().FirstOrDefault(v => v.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }

        //api/vehicles
        [HttpPost]
        public IActionResult Add([FromBody] Vehicle vehicle) //Tested
        {
            if (!Vehicle.IsValidId(vehicle.Id))
            {
                return BadRequest();
            }
            
            _parkingService.AddVehicle(vehicle);

            return CreatedAtRoute("GetById", new { id = vehicle.Id }, vehicle);
        }

        //api/vehicles/id
        [HttpDelete("{id}")]
        public IActionResult Delete(string id) //Tested
        {
            if (!Vehicle.IsValidId(id))
            {
                return BadRequest();
            }

            var vehicle = _parkingService.GetVehicles().FirstOrDefault(v => v.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            _parkingService.RemoveVehicle(id);

            return NoContent();
        }
    }
}
