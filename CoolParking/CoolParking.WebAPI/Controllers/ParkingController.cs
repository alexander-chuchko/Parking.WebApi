using CoolParking.WebAPI.Services.ParkingService;
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
            this._parkingService = parkingService;
        }


        //api/parking/balance
        [HttpGet("balance")]
        public ActionResult<decimal> GetBalance() //Tested
        {
            return Ok(_parkingService.GetBalance());
        }

        //api/parking/capacity
        [HttpGet("capacity")]
        public ActionResult<int> GetCapacity() //Tested
        {
            return Ok(_parkingService.GetCapacity());
        }

        //api/parking/freePlaces
        [HttpGet("freePlaces")]
        public ActionResult<int> GetFreePlaces() //Tested
        {
            return Ok(_parkingService.GetFreePlaces());
        }

    }
}
