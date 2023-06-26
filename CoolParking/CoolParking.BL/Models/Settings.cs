﻿// TODO: implement class Settings.
//       Implementation details are up to you, they just have to meet the requirements of the home task.

using System.Reflection;

namespace CoolParking.BL.Models
{
    public class Settings
    {
        public string LogFilePath = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\transactions.log";
        public decimal InitialBalanceParking = 0;
        public int ParkingCapacity = 10;
        public int PaymentWriteOffPeriod = 5;
        public int LoggingPeriod = 60;
        public int Coefficient = 1000;
        public decimal PenaltyCoefficient = 2.5m;

        //A dictionary with tariff coefficients has been created
        public Dictionary<int, decimal> Tariffs = new ()
        {
            [(int)VehicleType.PassengerCar] = 2m,
            [(int)VehicleType.Truck] = 5m,
            [(int)VehicleType.Bus] = 3.5m,
            [(int)VehicleType.Motorcycle] = 1m
        };
    }
}