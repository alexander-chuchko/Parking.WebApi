﻿using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using CoolParking.UI;
using System.Data.Common;
using System.Transactions;
using static System.Net.Mime.MediaTypeNames;

namespace CoolParking.BL
{
    public class UserInterface
    {
        private readonly IApiService _apiService;
        private string key;
        private Dictionary<int, Action> methodDictionary;

        public UserInterface(IApiService apiService)
        {
            _apiService = apiService;
            methodDictionary = GetInitializedMenuItems();
        }

        #region ---Helpers---

        private Dictionary<int, Action> GetInitializedMenuItems() 
        {
            return new Dictionary<int, Action>()
            {
                {1, DisplayCurrentBalance},
                {2, DisplayEarnings},
                {3, DisplayNumberFreeAndOccupiedSpaces},
                {4, DisplayListVehiclesFundsLocated},
                {5, PutVehicleAidForParking},
                {6, PickUpVehicle},
                {7, TopUpBalanceCar},
                {8, DisplayTransactionHistory},
                {9, DisplayAllTransactionsCurrentPeriod},
            };
        }

        //Display the current Parking balance on the screen
        private async void DisplayCurrentBalance()
        {
            ClearConsole();
            DisplayInfo();
            Console.WriteLine($"\tParking balance: {await _apiService.GetBalanceParking()}");
        }

        //Display the list of Tr. vehicles located in the Parking lot
        private async void DisplayNumberFreeAndOccupiedSpaces()
        {
            //Refactoring
            ClearConsole();
            DisplayInfo();

            Console.WriteLine($"\tNumber of free - " +
                $"{await _apiService.GetFreePlacesParking()} / employed -" +
                $" {await _apiService.GetCapacityParking() - await _apiService.GetFreePlacesParking()}");
        }

        //Display the amount of earned funds for the current period (before recording in the log)
        private async void DisplayEarnings()
        {
            ClearConsole();
            DisplayInfo();

            var transactionsLog = await _apiService.GetLastTransaction();

            if (transactionsLog != null)
            {
                Console.WriteLine($"\tAmount for the current period: {transactionsLog.Sum(tr => tr.Sum)}");
            }
            else
            {
                Console.WriteLine($"\tAmount for the current period: 0");
            }
        }

        //Display the history of Transactions on the screen (reading data from the Transactions.log file)
        private async void DisplayTransactionHistory()
        {
            ClearConsole();
            DisplayInfo();

            try
            {
                string transactions = await _apiService.GetTransactionAll();
                string[] arrayTransaction = transactions.Split(new string[] { "\\r", "\\n", "\""  }, StringSplitOptions.RemoveEmptyEntries);
                arrayTransaction.ToList().ForEach(i => Console.WriteLine($"\t{i}"));
            }
            catch (Exception)
            {
                Console.WriteLine("\tAt the moment, transactions have not been logged to a file.");
            }
        }

        //Display the list of vehicles located in the Parking lot
        private void DisplayListVehiclesFundsLocated()
        {
            ClearConsole();
            DisplayInfo();

            int count = 0;
            Console.WriteLine($"\tVehicle list:\n");
            var vehicles = _apiService.GetAllVehicleses().GetAwaiter().GetResult();
            if (vehicles != null && vehicles.Count() > 0)
            {
                vehicles.ToList().ForEach(v => Console.WriteLine($"\t{++count} - Id:{v.Id} VehicleType:{v.VehicleType} Balance:{v.Balance}"));
            }
            else 
            {
                Console.WriteLine("\tThere are no vehicles in the parking lot");
            }
        }

        //Put the Vehicle in Parking
        private void PutVehicleAidForParking()
        {
            ClearConsole();
            DisplayInfo();

            try
            {
                //Написать метод, который лучше генерировал транспортное средство
                var vehicle = new Vehicle(Vehicle.GenerateRandomRegistrationPlateNumber(), VehicleType.Truck, 100);
                var addedVehicle = _apiService.AddVehicle(vehicle).GetAwaiter().GetResult();
                Console.WriteLine($"\tAdded to the parking car - Id:{addedVehicle.Id} VehicleType:{addedVehicle.VehicleType} Balance:{addedVehicle.Balance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\t{ex.Message}");
            }
        }

        //Pick up the vehicle from the parking lot
        private async void PickUpVehicle()
        {
            ClearConsole();
            DisplayInfo();

            Console.WriteLine("\tEnter vehicle number");
            //DisplayListVehiclesFundsLocated();
            string? id = Console.ReadLine();

            if (Validation.IsValidId(id))
            {
                await _apiService.DeleteVehicle(id);
            }
            else 
            {
                Console.WriteLine("\tEnter incorrect data");
            }
        }

        //Replenish the balance of a specific financial instrument.
        private void TopUpBalanceCar()
        {
            ClearConsole();
            DisplayInfo();

            Console.WriteLine("\tEnter vehicle number:");
            string? id = Console.ReadLine();
            Console.WriteLine("\tEnter replenishment amount:");
            string? topUpAmount = Console.ReadLine();

            if (Validation.IsValidId(id) && Validation.IsPositive(topUpAmount))
            {
                var vehicle = _apiService.TopUpVehicle(id, Decimal.Parse(topUpAmount)).GetAwaiter().GetResult();
                Console.WriteLine($"\tVehicle balance successfully topped up - Id:{vehicle.Id} VehicleType:{vehicle.VehicleType} Balance:{vehicle.Balance}");
            }
            else
            {
                Console.WriteLine("\tEnter incorrect data");
            }
        }

        //Display all parking transactions for the current period (before logging)
        private void DisplayAllTransactionsCurrentPeriod()
        {
            ClearConsole();
            DisplayInfo();

            var transactionInfo = _apiService.GetLastTransaction().GetAwaiter().GetResult();

            if (transactionInfo!=null && transactionInfo.Length > 0)
            {
                transactionInfo.ToList().ForEach(t => Console.WriteLine($"\tId:{t.VehicleId} Date:{t.TransactionTime} Sum:{t.Sum}\r"));
            }
            else
            {
                Console.WriteLine("\tNo transactions have been recorded at the moment");
            }
        }

        private void ClearConsole()
        {
            Console.Clear();
        }

        private void ChangedColor(ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
        }

        private void DisplayInfo()
        {
            ClearConsole();
            ChangedColor(ConsoleColor.Red);

            Console.WriteLine("\n\t\t\t\tCOOL PARKING");
            ChangedColor(ConsoleColor.Yellow);

            Console.WriteLine("\n\tMENU");

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\n\t" +
                "1 - Display the current balance of the parking lot\n\t" +
                "2 - Display the amount of money earned for the current period (before logging)\n\t" +
                "3 - Display the number of free/occupied parking spaces on the screen\n\t" +
                "4 - Display the list of vehicles in the parking lot\n\t" +
                "5 - Put the vehicle in parking\n\t" +
                "6 - Pick up the vehicle from the parking lot\n\t" +
                "7 - Top up the balance of a specific vehicle\n\t" +
                "8 - Display transaction history\n\t" +
                "9 - Display all parking transactions for the current period");

            ChangedColor(ConsoleColor.Yellow);
            Console.WriteLine("\n\tSelect the desired item:\n");
            ChangedColor(ConsoleColor.White);
        }

        public void RunApplication()
        {
            DisplayInfo();

            do
            {
                key = Console.ReadLine();

                if (Validation.IsValidMenuItem(key, methodDictionary.Count))
                {
                    methodDictionary[int.Parse(key)].Invoke();
                }
                else if (key != "e")
                {
                    Console.WriteLine("Invalid value specified!");
                }

                ChangedColor(ConsoleColor.Red);
                Console.WriteLine("\n\tEXIT THE APPLICATION - 'e'\n");
                ChangedColor(ConsoleColor.White);

            } while (key != "e");
        }

        #endregion
    }
}
