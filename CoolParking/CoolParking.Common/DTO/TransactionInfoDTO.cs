using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolParking.Common.DTO
{
    public class TransactionInfoDTO
    {
        public decimal Sum { get; set; }
        public string VehicleId { get; set; }
        public string TransactionTime { get; set; }
    }
}
