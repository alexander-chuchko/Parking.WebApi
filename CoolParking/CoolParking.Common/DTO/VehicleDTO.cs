using Newtonsoft.Json;


namespace CoolParking.Common.DTO
{
    public class VehicleDTO
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("vehicleType")]
        public VehicleTypeDTO VehicleType { get; set; }
        [JsonProperty("balance")]
        public decimal Balance { get; set; }
    }
}
