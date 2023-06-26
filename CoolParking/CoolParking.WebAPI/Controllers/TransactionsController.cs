using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoolParking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IParkingService _parkingService;
        public TransactionsController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        //api/transactions/last
        [HttpGet("last")]
        public ActionResult<TransactionInfo[]> GetLastTransaction() //Tested
        {
            var lastTransactions = _parkingService.GetLastParkingTransactions();

            if (lastTransactions == null)
            {
                return NoContent();
            }

            return Ok(lastTransactions);
        }

        [HttpGet("all")]
        //api/transactions/all 
        public ActionResult<string> GetAllTransaction() //Tested
        {
            var transactions = _parkingService.ReadFromLog();

            if (transactions != null && transactions.Length <= 0) //If log file not found - Status Code: 404 Not Found
            {
                return NotFound();
            }

            return Ok(transactions);
        }

        //api/transactions/topUpVehicle
        [HttpPut("topUpVehicle")]
        public ActionResult<Vehicle> GetTopVehicle([FromBody] Vehicle vehicle) //Tested
        {
            if (!Vehicle.IsValidId(vehicle.Id) || vehicle.Balance <= 0)
            {
                return BadRequest();
            }

            var foundVehicle = _parkingService.GetVehicles().FirstOrDefault(v => v.Id == vehicle.Id);

            if (foundVehicle == null)
            {
                return NotFound();
            }

            _parkingService.TopUpVehicle(vehicle.Id, vehicle.Balance);

            return Ok(foundVehicle);
        }
    }
}
