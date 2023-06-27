using CoolParking.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolParking.Common.Interfaces
{
    public interface ITransactionInfoService
    {
        TransactionInfoDTO[] GetLastParkingTransactions();
    }
}
