using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using CoolParking.Common.DTO;
using CoolParking.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoolParking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IParkingService _parkingService;
        private readonly ITransactionInfoService _transactionInfoService;
        public TransactionsController(IParkingService parkingService, ITransactionInfoService transactionInfoService)
        {
            _parkingService = parkingService;
            _transactionInfoService = transactionInfoService;   
        }

        //api/transactions/last
        [HttpGet("last")]
        public ActionResult<TransactionInfoDTO[]> GetLastTransaction() //Tested
        {
            var lastTransactions = _transactionInfoService.GetLastParkingTransactions();
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

            if (transactions != null && transactions.Length <= 0) 
            {
                return NotFound();
            }

            return Ok(transactions);
        }

        //api/transactions/topUpVehicle
        [HttpPut("topUpVehicle")]
        public ActionResult<VehicleDTO> GetTopVehicle([FromBody] VehicleDTO vehicle) 
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
