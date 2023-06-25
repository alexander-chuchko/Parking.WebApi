using CoolParking.BL.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CoolParking.BL.Services
{
    public class ApiService : IDisposable
    {
        private HttpClient _client;

        public const string WEB_API_VECHICLE = "api/vehicles";
        public const string WEB_API_PARKING = "api/parking";
        public const string WEB_API_TRANSACTION = "api/transactions";

        public ApiService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7066/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region ---Methods for vechicleses---
        public async Task<Vehicle> AddVehicle(Vehicle vehicles)
        {
            Vehicle? addedVehicle = null;

            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync(WEB_API_VECHICLE, vehicles);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.Created)
                    {
                        addedVehicle = await response.Content.ReadFromJsonAsync<Vehicle>();
                    }

                    ShowStatusCode(response.StatusCode, nameof(AddVehicle));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
            }

            return addedVehicle;
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehicleses()
        {
            IEnumerable<Vehicle>? vehicles = null;

            try
            {
                HttpResponseMessage response = await _client.GetAsync(WEB_API_VECHICLE);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        vehicles = await response.Content.ReadFromJsonAsync<IEnumerable<Vehicle>>();
                    }

                    ShowStatusCode(response.StatusCode, nameof(GetAllVehicleses));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
            }

            return vehicles;
        }

        public async Task<Vehicle> GetByIdVehicle(string id)
        {
            Vehicle? addedVehicle = null;

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"{WEB_API_VECHICLE}/{id}");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        addedVehicle = await response.Content.ReadFromJsonAsync<Vehicle>();
                    }

                    ShowStatusCode(response.StatusCode, nameof(GetByIdVehicle));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
            }

            return addedVehicle;
        }

        public async Task DeleteVehicle(string id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{WEB_API_VECHICLE}/{id}");

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                ShowStatusCode(response.StatusCode, nameof(DeleteVehicle));
            }
        }

        #endregion

        #region ---Methods for transaction---

        public async Task<TransactionInfo[]> GetLastTransaction()
        {
            TransactionInfo[]? transactionInfos = null;

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"{WEB_API_TRANSACTION}/last");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {

                        transactionInfos = await response.Content.ReadFromJsonAsync<TransactionInfo[]>();
                    }

                    ShowStatusCode(response.StatusCode, nameof(GetLastTransaction));
                }

                return transactionInfos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
            }

            return transactionInfos;
        }
        public async Task<string> GetTransactionAll()
        {
            string? transactions = null;

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"{WEB_API_TRANSACTION}/all");

                response.EnsureSuccessStatusCode();


                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        transactions = await response.Content.ReadAsStringAsync();
                    }

                    ShowStatusCode(response.StatusCode, nameof(GetTransactionAll));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
            }

            return transactions;
        }

        //topUpVehicle
        public async Task<Vehicle> TopUpVehicle(string id, decimal sum)
        {
            Vehicle? vehicle = null;

            try
            {
                var parametrs = new Vehicle(id, VehicleType.PassengerCar, sum);

                HttpResponseMessage response = await _client.PutAsJsonAsync($"{WEB_API_TRANSACTION}/topUpVehicle", parametrs);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        vehicle = await response.Content.ReadFromJsonAsync<Vehicle>();
                    }

                    ShowStatusCode(response.StatusCode, nameof(TopUpVehicle));

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
            }

            return vehicle;
        }


        #endregion

        #region ---Methods for parking---
        public async Task<int> GetCapacityParking()
        {
            int capacity = 0;

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"{WEB_API_PARKING}/capacity");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        capacity = await response.Content.ReadFromJsonAsync<int>();
                    }

                    ShowStatusCode(response.StatusCode, nameof(GetCapacityParking));

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
            }

            return capacity;
        }

        public async Task<int> GetFreePlacesParking()
        {
            int freePlaces = 0;

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"{WEB_API_PARKING}/freePlaces");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        freePlaces = await response.Content.ReadFromJsonAsync<int>();
                    }

                    ShowStatusCode(response.StatusCode, nameof(GetFreePlacesParking));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
            }

            return freePlaces;
        }

        public async Task<decimal> GetBalanceParking()
        {
            decimal balance = 0;

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"{WEB_API_PARKING}/balance");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        balance = await response.Content.ReadFromJsonAsync<int>();
                    }

                    ShowStatusCode(response.StatusCode, nameof(GetBalanceParking));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
            }

            return balance;
        }

        #endregion

        private void ShowStatusCode(HttpStatusCode httpStatusCode, string nameMethod)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\t\tWorked out the method: {nameMethod}\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\t\tStatus code: {httpStatusCode}\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Dispose()
        {

        }
    }
}
