
using CoolParking.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoolParking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingService _parkingService;
        public ParkingController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        //api/parking/balance
        [HttpGet("balance")]
        public ActionResult<decimal> GetBalance() 
        {
            return Ok(_parkingService.GetBalance());
        }

        //api/parking/capacity
        [HttpGet("capacity")]
        public ActionResult<int> GetCapacity() 
        {
            return Ok(_parkingService.GetCapacity());
        }

        //api/parking/freePlaces
        [HttpGet("freePlaces")]
        public ActionResult<int> GetFreePlaces()
        {
            return Ok(_parkingService.GetFreePlaces());
        }
    }
}
