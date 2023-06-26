// TODO: implement the ParkingService class from the IParkingService interface.
//       For try to add a vehicle on full parking InvalidOperationException should be thrown.
//       For try to remove vehicle with a negative balance (debt) InvalidOperationException should be thrown.
//       Other validation rules and constructor format went from tests.
//       Other implementation details are up to you, they just have to match the interface requirements
//       and tests, for example, in ParkingServiceTests you can find the necessary constructor format and validation rules.

using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using System.Collections.ObjectModel;
using System.Timers;

namespace CoolParking.BL.Services
{
    public class ParkingService : IParkingService
    {
        private readonly ITimerService _withdrawTimer;
        private readonly ITimerService _logTimer;
        private readonly ILogService _logService;
        private TransactionInfo[] transactionInfo;
        private Parking parking;

        public ParkingService(ITimerService withdrawTimer, ITimerService logTimer, ILogService logService)
        {
            parking = Parking.GetInstance();
            parking.Vehicles = new List<Vehicle>(Settings.ParkingCapacity);
            _logService = logService;
            _logTimer = logTimer;
            _withdrawTimer = withdrawTimer;
            this._logTimer.Elapsed += OnLogRecord;
            this._withdrawTimer.Elapsed += OnWithdrawFunds;

        }

        #region  ---  Interface IParkingService implementation   ---

        //Method for adding vichel to the parking
        public void AddVehicle(Vehicle vehicle)
        {
            if (parking.Vehicles.Count == Settings.ParkingCapacity)
            {
                throw new InvalidOperationException("There are no spaces in the parking lot");
            }

            if (parking.Vehicles.Count != 0 && parking.Vehicles.Exists(v => v.Id == vehicle.Id))
            {
                throw new ArgumentException("Invalid identifier entered");
            }

            if (vehicle.Balance >= Settings.Tariffs[(int)vehicle.VehicleType])
            {
                parking.Vehicles.Add(vehicle);
                StartOrStopTimer(parking.Vehicles);
            }
        }

        public void Dispose()
        {
            parking.DisposeInstance();
        }

        //Method for geting balance of parking
        public decimal GetBalance()
        {
            return parking.Balance;
        }

        //Method for geting capacity of parking
        public int GetCapacity()
        {
            return parking.Vehicles.Capacity;
        }

        //Method for geting free places of parking
        public int GetFreePlaces()
        {
            return parking.Vehicles.Capacity - parking.Vehicles.Count;
        }

        public TransactionInfo[] GetLastParkingTransactions()
        {
            return transactionInfo;
        }

        //Method for geting all vehicles of parking
        public ReadOnlyCollection<Vehicle> GetVehicles()
        {
            return parking.Vehicles.AsReadOnly();
        }

        public string ReadFromLog()
        {
            return _logService.Read();
        }

        //Pick up car from parking
        public void RemoveVehicle(string vehicleId)
        {
            var vehicle = parking.Vehicles.Find(tr => tr.Id == vehicleId);

            if (vehicle != null)
            {
                if (vehicle.Balance < Settings.InitialBalanceParking)
                {
                    throw new InvalidOperationException("Your balance is negative");
                }
                else
                {
                    parking.Vehicles.Remove(vehicle);

                    StartOrStopTimer(parking.Vehicles);
                }
            }
            else
            {
                throw new ArgumentException("This number does not exist");
            }
        }

        //Method for top up vehicle
        public void TopUpVehicle(string vehicleId, decimal sum)
        {
            var foundVehicle = parking.Vehicles.Find(tr => tr.Id == vehicleId);

            if (foundVehicle != null && sum >= 0)
            {
                foundVehicle.Balance += sum;
            }
            else
            {
                throw new ArgumentException("Invalid data entered");
            }
        }

        #endregion

        #region ---Helpers---

        private void OnLogRecord(object sender, ElapsedEventArgs e)
        {  
            string transactions = string.Empty;

            if (transactionInfo != null)
            {
                foreach (var transaction in transactionInfo)
                {
                    if (transaction != null)
                    {
                        transactions += $"Id:{transaction.VehicleId} Date:{transaction.TransactionTime} Sum:{transaction.Sum}\r";
                    }
                }
            }

            _logService.Write(transactions);

            transactionInfo = null;
        }

        private void OnWithdrawFunds(object sender, ElapsedEventArgs e)
        {
            if (parking.Vehicles.Count != 0)
            {
                int count = 0;

                //When the first object is added
                if (transactionInfo == null)
                {
                    transactionInfo = new TransactionInfo[parking.Vehicles.Count];
                }
                else
                {
                    ResizeArray(transactionInfo.Length + parking.Vehicles.Count);
                    count = transactionInfo.Length - parking.Vehicles.Count;
                }

                foreach (var vehicles in parking.Vehicles)
                {
                    decimal sumFine = 0;
                    decimal tariff = Settings.Tariffs[(int)vehicles.VehicleType];

                    if (vehicles.Balance < 0)
                    {
                        sumFine = tariff * Settings.PenaltyCoefficient;
                    }
                    else if (vehicles.Balance < tariff)
                    {
                        sumFine = vehicles.Balance + ((tariff - vehicles.Balance) * Settings.PenaltyCoefficient);
                    }
                    else if (vehicles.Balance >= tariff)
                    {
                        sumFine = tariff;
                    }

                    vehicles.Balance -= sumFine;
                    parking.Balance += sumFine;

                    transactionInfo[count] = CreateTransactionInfo(vehicles, sumFine);

                    count++;
                }
            }
        }

        private TransactionInfo CreateTransactionInfo(Vehicle vehicle, decimal sumFine)
        {
            return new TransactionInfo
            {
                VehicleId = vehicle.Id,
                TransactionTime = DateTime.Now.ToString("hh:mm:ss"),
                Sum = sumFine
            };
        }

        private void ResizeArray(int size)
        {
            TransactionInfo[] newArray = new TransactionInfo[size];

            Array.Copy(transactionInfo, newArray, Math.Min(size, transactionInfo.Length));

            transactionInfo = newArray;
        }

        private void StartOrStopTimer(IEnumerable<Vehicle> vehicles)
        {
            if (vehicles.Count() == 1)
            {
                parking.StartTime = DateTime.Now;
                _withdrawTimer.Interval = Settings.PaymentWriteOffPeriod * Settings.Coefficient;
                _withdrawTimer.Start();
                _logTimer.Interval = Settings.LoggingPeriod * Settings.Coefficient;
                _logTimer.Start();
            }
            else if (vehicles.Count() == 0)
            {
                parking.StartTime = null;
                _withdrawTimer.Stop();
                _logTimer.Stop();
            }
        }

        #endregion
    }
}