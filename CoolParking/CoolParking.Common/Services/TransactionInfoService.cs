using AutoMapper;
using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using CoolParking.Common.DTO;
using CoolParking.Common.Interfaces;


namespace CoolParking.Common.Services
{
    public class TransactionInfoService : ITransactionInfoService
    {
        private readonly IParkingService _parkingService;
        private readonly IMapper _mapper;
        public TransactionInfoService(IParkingService parkingService, IMapper mapper)
        {
            _parkingService = parkingService;
            _mapper = mapper;
        }

        public TransactionInfoDTO[] GetLastParkingTransactions()
        {
            TransactionInfoDTO[] transactionsDTO = null;
            var lastTransactions = _parkingService.GetLastParkingTransactions();

            if (lastTransactions != null) 
            {
                transactionsDTO = _mapper.Map <TransactionInfoDTO[]>(lastTransactions);
            }

            return transactionsDTO;
        }
    }
}
