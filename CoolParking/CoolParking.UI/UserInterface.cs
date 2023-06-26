using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using CoolParking.UI;

namespace CoolParking.BL
{
    public class UserInterface
    {
        private readonly IParkingService _parkingService;
        private readonly IApiService _apiService;
        private readonly int numberMenuItems = 9;
        private string key;

        public UserInterface(IApiService apiService)
        {
            //_parkingService = parkingService;
            _apiService = apiService;   
        }

        #region ---Helpers---

        //Display the current Parking balance on the screen
        private async void DisplayCurrentBalance()
        {
            Console.WriteLine($"\tParking balance: {await _apiService.GetBalanceParking()}");
        }

        //Display the list of Tr. vehicles located in the Parking lot
        private async void DisplayNumberFreeAndOccupiedSpaces()
        {
            Console.WriteLine($"\tNumber of free - " +
                $"{await _apiService.GetFreePlacesParking()} / employed -" +
                $" {await _apiService.GetCapacityParking() - await _apiService.GetFreePlacesParking()}");
        }

        //Display the amount of earned funds for the current period (before recording in the log)
        private async void DisplayEarnings()
        {
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
            try
            {
                string arrayTransaction = await _apiService.GetTransactionAll();

                var transactions = arrayTransaction.Split(new string[] { "\r" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in transactions)
                {
                    Console.WriteLine($"\t{item}");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("\tAt the moment, transactions have not been logged to a file.");
            }
        }

        //Display the list of Tr. vehicles located in the Parking lot
        private async void DisplayListVehiclesFundsLocated()
        {
            if (await _apiService.GetFreePlacesParking() < Settings.ParkingCapacity)
            {
                int count = 0;
                Console.WriteLine($"\tVehicle list:\n");

                foreach (var item in _parkingService.GetVehicles())
                {
                    Console.WriteLine($"\t{++count} - Id:{item.Id} VehicleType:{item.VehicleType} Balance:{item.Balance}");
                }
            }
            else
            {
                Console.WriteLine("\tThere are no cars in the parking lot");
            }
        }

        //Put the Vehicle in Parking
        private async void PutVehicleAidForParking()
        {
            try
            {
                //Написать метод, который лучше генерировал транспортное средство
                var vehicle = new Vehicle(Vehicle.GenerateRandomRegistrationPlateNumber(), VehicleType.Truck, 100);
                await _apiService.AddVehicle(vehicle);
                Console.WriteLine($"\tAdded to the parking car - Id:{vehicle.Id} VehicleType:{vehicle.VehicleType} Balance:{vehicle.Balance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\t{ex.Message}");
            }
        }

        //Pick up the Vehicle from the Parking lot
        private async void PickUpVehicle()
        {
            var res = await _apiService.GetAllVehicleses();

            if (res.Count() > 0)
            {
                try
                {
                    Console.WriteLine("\tSpecify the index of the vehicle");
                    DisplayListVehiclesFundsLocated();
                    string? id = Console.ReadLine();

                    var vehicleses = _parkingService.GetVehicles();
                    //Заменить
                    if (id != null && int.TryParse(id, out int convertId) && convertId > 0 && convertId <= vehicleses.Count)
                    {
                        await _apiService.DeleteVehicle(vehicleses[convertId - 1].Id);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\t{ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("\tThere are no cars in the parking lot");
            }
        }

        //Replenish the balance of a specific financial instrument.
        private async void TopUpBalanceCar()
        {
            Console.WriteLine("\tEnter vehicle number:");
            string? id = Console.ReadLine();
            Console.WriteLine("\tEnter replenishment amount:");
            string? topUpAmount = Console.ReadLine();

            if (Validation.IsValidId(id) && Validation.IsPositive(topUpAmount))
            {
                await _apiService.TopUpVehicle(id, Decimal.Parse(topUpAmount));
            }
            else
            {
                Console.WriteLine("\tEnter incorrect data");
            }
        }

        //Display all Parking Transactions for the current period (before logging)
        private async void DisplayAllTransactionsCurrentPeriod()
        {
            var transactionInfo = await _apiService.GetLastTransaction();

            if (transactionInfo!=null && transactionInfo.Length > 0)
            {
                foreach (var transaction in transactionInfo)
                {
                    if (transaction != null)
                    {
                        Console.WriteLine($"\tId:{transaction.VehicleId} Date:{transaction.TransactionTime} Sum:{transaction.Sum}\r");
                    }
                }
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
            Console.Clear();
            ChangedColor(ConsoleColor.Red);

            Console.WriteLine("\n\t\t\t\tCOOL PARKING");
            ChangedColor(ConsoleColor.Yellow);

            Console.WriteLine("\n\tMENU");

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\n\t" +
                "1 - Display the current balance of the Parking Lot\n\t" +
                "2 - Display the amount of money earned for the current period (before logging)\n\t" +
                "3 - Display the number of free/occupied parking spaces on the screen\n\t" +
                "4 - Display the list of Tr. funds located in the Parking lot\n\t" +
                "5 - Put Tr. aid for parking\n\t" +
                "6 - Pick up the vehicle from the Parking lot\n\t" +
                "7 - Top up the balance of a specific Tr. funds\n\t" +
                "8 - Display transaction history\n\t" +
                "9 - Display all parking transactions for the current period");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\tSelect the desired item:\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void RunApplication()
        {
            DisplayInfo();

            do
            {

                key = Console.ReadLine();

                if (int.TryParse(key, out int number) && number > 0 && number <= numberMenuItems)
                {
                    switch (number)
                    {
                        case 1:
                            ClearConsole();
                            DisplayInfo();
                            DisplayCurrentBalance();
                            break;
                        case 2:
                            ClearConsole();
                            DisplayInfo();
                            DisplayEarnings();
                            break;

                        case 3:
                            ClearConsole();
                            DisplayInfo();
                            DisplayNumberFreeAndOccupiedSpaces();
                            break;

                        case 4:
                            ClearConsole();
                            DisplayInfo();
                            DisplayListVehiclesFundsLocated();
                            break;

                        case 5:
                            ClearConsole();
                            DisplayInfo();
                            PutVehicleAidForParking();
                            break;

                        case 6:
                            ClearConsole();
                            DisplayInfo();
                            PickUpVehicle();
                            break;

                        case 7:
                            ClearConsole();
                            DisplayInfo();
                            TopUpBalanceCar();
                            break;

                        case 8:
                            ClearConsole();
                            DisplayInfo();
                            DisplayTransactionHistory();
                            break;

                        case 9:
                            ClearConsole();
                            DisplayInfo();
                            DisplayAllTransactionsCurrentPeriod();
                            break;

                        default:
                            Console.WriteLine("\tInvalid value specified!");
                            break;
                    }
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
