using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using CoolParking.Common.DTO;
using CoolParking.UI;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CoolParking.BL.Services
{
    public class ApiService : IApiService, IDisposable
    {
        private HttpClient _client;

        public ApiService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7066/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region ---Methods for vechicleses---
        public async Task<VehicleDTO> AddVehicleAsync(VehicleDTO vehicles)
        {
            VehicleDTO? addedVehicle = null;

            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync(Constants.WEB_API_VECHICLE, vehicles);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.Created)
                {
                    addedVehicle = await response.Content.ReadFromJsonAsync<VehicleDTO>();
                }

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"\tHTTP Request Error: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"\tJSON Deserialization Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\tError:{ex.Message}");
            }

            return addedVehicle;
        }

        public async Task<IEnumerable<VehicleDTO>> GetAllVehiclesesAsync()
        {
            IEnumerable<VehicleDTO>? vehicles = null;

            try
            {
                HttpResponseMessage response = await _client.GetAsync(Constants.WEB_API_VECHICLE);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    vehicles = await response.Content.ReadFromJsonAsync<IEnumerable<VehicleDTO>>();
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"\tHTTP Request Error: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"\tJSON Deserialization Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
            }

            return vehicles;
        }

        public async Task<VehicleDTO> GetByIdVehicleAsync(string id)
        {
            VehicleDTO? addedVehicle = null;

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"{Constants.WEB_API_VECHICLE}/{id}");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    addedVehicle = await response.Content.ReadFromJsonAsync<VehicleDTO>();
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"\tHTTP Request Error: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"\tJSON Deserialization Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\tError:{ex.Message}");
            }

            return addedVehicle;
        }

        public async Task DeleteVehicleAsync(string id)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync($"{Constants.WEB_API_VECHICLE}/{id}");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode && response.IsSuccessStatusCode)
                {
                    ShowStatusCode(response.StatusCode, nameof(DeleteVehicleAsync));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\tError:{ex.Message}");
            }
        }

        #endregion

        #region ---Methods for transaction---

        public async Task<TransactionInfoDTO[]> GetLastTransactionAsync()
        {
            TransactionInfoDTO[]? transactionInfos = null;

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"{Constants.WEB_API_TRANSACTION}/last");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    transactionInfos = await response.Content.ReadFromJsonAsync<TransactionInfoDTO[]>();
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"\tHTTP Request Error: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"\tJSON Deserialization Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
            }

            return transactionInfos;
        }
        public async Task<string> GetTransactionAllAsync()
        {
            string? transactions = null;

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"{Constants.WEB_API_TRANSACTION}/all");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    transactions = await response.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"\tHTTP Request Error: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"\tJSON Deserialization Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
            }

            return transactions;
        }

        public async Task<VehicleDTO> TopUpVehicleAsync(string id, decimal sum)
        {
            VehicleDTO? vehicle = null;

            try
            {
                var parametrs = new Vehicle(id, VehicleType.PassengerCar, sum);

                HttpResponseMessage response = await _client.PutAsJsonAsync($"{Constants.WEB_API_TRANSACTION}/topUpVehicle", parametrs);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    vehicle = await response.Content.ReadFromJsonAsync<VehicleDTO>();
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"\tHTTP Request Error: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"\tJSON Deserialization Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
            }

            return vehicle;
        }

        #endregion

        #region ---Methods for parking---
        public async Task<int> GetCapacityParkingAsync()
        {
            int capacity = 0;

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"{Constants.WEB_API_PARKING}/capacity");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    capacity = await response.Content.ReadFromJsonAsync<int>();
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"\tHTTP Request Error: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"\tJSON Deserialization Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
            }

            return capacity;
        }

        public async Task<int> GetFreePlacesParkingAsync()
        {
            int freePlaces = 0;

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"{Constants.WEB_API_PARKING}/freePlaces");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    freePlaces = await response.Content.ReadFromJsonAsync<int>();
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"\tHTTP Request Error: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"\tJSON Deserialization Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
            }

            return freePlaces;
        }

        public async Task<decimal> GetBalanceParkingAsync()
        {
            decimal balance = 0;

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"{Constants.WEB_API_PARKING}/balance");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    balance = await response.Content.ReadFromJsonAsync<int>();
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"\tHTTP Request Error: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"\tJSON Deserialization Error: {ex.Message}");
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
