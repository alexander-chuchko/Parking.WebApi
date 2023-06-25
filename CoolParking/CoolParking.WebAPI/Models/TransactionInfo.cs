namespace CoolParking.WebAPI.Models
{
    public class TransactionInfo
    {
        public decimal Sum { get; set; }
        public string? TransactionTime { get; set; }
        public string? VehicleId { get; set; }
    }
}
