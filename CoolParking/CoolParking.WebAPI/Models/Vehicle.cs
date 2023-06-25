using Newtonsoft.Json;

namespace CoolParking.WebAPI.Models
{
    public class Vehicle
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        public VehicleType VehicleType { get; set; }
        [JsonProperty("balance")]
        public decimal Balance { get; set; }
    }
}
