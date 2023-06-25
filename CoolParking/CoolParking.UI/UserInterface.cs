using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;

namespace CoolParking.BL
{
    public class UserInterface
    {
        private readonly IParkingService _parkingService;
        private readonly int numberMenuItems = 9;
        private string key;

        public UserInterface(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        #region ---Helpers---

        //Display the current Parking balance on the screen
        private void DisplayCurrentBalance()
        {
            Console.WriteLine($"\tParking balance: {_parkingService.GetBalance()}");
        }

        //Display the list of Tr. vehicles located in the Parking lot
        private void DisplayNumberFreeAndOccupiedSpaces()
        {
            Console.WriteLine($"\tNumber of free - " +
                $"{_parkingService.GetFreePlaces()} / employed -" +
                $" {_parkingService.GetCapacity() - _parkingService.GetFreePlaces()}");
        }

        //Display the amount of earned funds for the current period (before recording in the log)
        private void DisplayEarnings()
        {
            var transactionsLog = _parkingService.GetLastParkingTransactions();

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
        private void DisplayTransactionHistory()
        {
            try
            {
                string arrayTransaction = _parkingService.ReadFromLog();

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
        private void DisplayListTrFundsLocated()
        {
            if (_parkingService.GetFreePlaces() < Settings.parkingCapacity)
            {
                int count = default(int);
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
        private void PutTrAidForParking()
        {
            try
            {
                var vehicle = new Vehicle(Vehicle.GenerateRandomRegistrationPlateNumber(), VehicleType.Truck, 100);
                _parkingService.AddVehicle(vehicle);
                Console.WriteLine($"\tAdded to the parking car - Id:{vehicle.Id} VehicleType:{vehicle.VehicleType} Balance:{vehicle.Balance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\t{ex.Message}");
            }
        }

        //Pick up the Vehicle from the Parking lot
        private void PickUpVehicle()
        {
            if (_parkingService.GetVehicles().Count > 0)
            {
                try
                {
                    Console.WriteLine("\tSpecify the index of the vehicle");
                    DisplayListTrFundsLocated();
                    string? id = Console.ReadLine();

                    var vehicleses = _parkingService.GetVehicles();

                    if (id != null && int.TryParse(id, out int convertId) && convertId > 0 && convertId <= vehicleses.Count)
                    {
                        _parkingService.RemoveVehicle(vehicleses[convertId - 1].Id);
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
        private void TopUpBalanceCar()
        {
            try
            {
                if (_parkingService.GetVehicles().Count > 0)
                {
                    Console.WriteLine("\tSpecify the index of the vehicle");
                    DisplayListTrFundsLocated();
                    string? id = Console.ReadLine();
                    Console.WriteLine("\tEnter replenishment amount");
                    string? topUpAmount = Console.ReadLine();
                    var vehicleses = _parkingService.GetVehicles();

                    if (id != null && int.TryParse(id, out int convertIndex) && int.TryParse(topUpAmount, out int convertTopUpAmount) && convertIndex > 0 && convertIndex <= vehicleses.Count)
                    {
                        _parkingService.TopUpVehicle(vehicleses[convertIndex - 1].Id, convertTopUpAmount);
                    }
                }
                else
                {
                    Console.WriteLine("\tThere are no cars in the parking lot");
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"\t{ex.Message}");
            }
        }

        //Display all Parking Transactions for the current period (before logging)
        private void DisplayAllTransactionsCurrentPeriod()
        {
            var transactionInfo = _parkingService.GetLastParkingTransactions();

            if (transactionInfo!=null && transactionInfo.Length > 0)
            {

                if (transactionInfo != null)
                {
                    foreach (var transaction in transactionInfo)
                    {
                        if (transaction != null)
                        {
                            Console.WriteLine($"\tId:{transaction.VehicleId} Date:{transaction.TransactionTime} Sum:{transaction.Sum}\r");
                        }
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
                            DisplayListTrFundsLocated();
                            break;

                        case 5:
                            ClearConsole();
                            DisplayInfo();
                            PutTrAidForParking();
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
