﻿namespace CoolParking.WebAPI.Models
{
    public class Parking
    {
        private static Parking? instance;
        public List<Vehicle>? Vehicles { get; set; }
        public decimal Balance { get; set; }

        public DateTime? StartTime { get; set; }
    }
}