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
        public ActionResult<IEnumerable<VehicleDTO>> GetAll() 
        {
            return Ok(_vehicleService.GetVehicles());
        }

        //api/vehicles/id 
        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<VehicleDTO> GetById(string id) 
        {
            if (!Vehicle.IsValidId(id))
            {
                return BadRequest();
            }

            var vehicleDTO = _vehicleService.GetVehicleById(id);

            if (vehicleDTO == null)
            {
                return NotFound();
            }

            return Ok(vehicleDTO);
        }

        //api/vehicles
        [HttpPost]
        public IActionResult Add([FromBody] VehicleDTO vehicleDTO) 
        {
            try
            {
                _vehicleService.AddVehicle(vehicleDTO);

                return CreatedAtRoute("GetById", new { id = vehicleDTO.Id }, vehicleDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //api/vehicles/id
        [HttpDelete("{id}")]
        public IActionResult Delete(string id) 
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
