using CoolParking.WebAPI.Models;
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

        //api/transactions/last
        [HttpGet("last")]
        public ActionResult<TransactionInfo[]> GetLastTransaction()
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
        public ActionResult<string> GetAllTransaction()
        {
            var transactions = _parkingService.ReadFromLog();

            if (transactions != null && transactions.Length <= 0) //If log file not found - Status Code: 404 Not Found
            {
                return NotFound();
            }

            return Ok(transactions);
        }

    }
}
