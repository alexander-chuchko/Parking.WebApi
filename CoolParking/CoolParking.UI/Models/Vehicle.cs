using CoolParking.BL.Models;
using Newtonsoft.Json;

namespace CoolParking.UI.Models
{
    public class Vehicle
    {
        [JsonProperty("id")]
        public string Id { get; }
        [JsonProperty("vehicleType")]
        public VehicleType VehicleType { get; }
        [JsonProperty("balance")]
        public decimal Balance { get; }
    }
}
