﻿// TODO: implement struct TransactionInfo.
//       Necessarily implement the Sum property (decimal) - is used in tests.
//       Other implementation details are up to you, they just have to meet the requirements of the homework.

using System.Transactions;

namespace CoolParking.BL.Models
{
    public class TransactionInfo 
    {
        public decimal Sum { get; set; }
        public string VehicleId { get; set; }
        public string TransactionTime { get; set; }

        public override string ToString()
        {
            return $"Id:{VehicleId} Date:{TransactionTime} Sum:{Sum}\r";
        }
    }
}
