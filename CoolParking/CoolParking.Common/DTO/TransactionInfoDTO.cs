using Newtonsoft.Json;


namespace CoolParking.Common.DTO
{
    public class TransactionInfoDTO
    {
        [JsonProperty("sum")]
        public decimal Sum { get; set; }
        [JsonProperty("vehicleId")]
        public string VehicleId { get; set; }
        [JsonProperty("transactionTime")]
        public string TransactionTime { get; set; }
    }
}
