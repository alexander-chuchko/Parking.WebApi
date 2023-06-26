// TODO: implement class Vehicle.
//       Properties: Id (string), VehicleType (VehicleType), Balance (decimal).
//       The format of the identifier is explained in the description of the home task.
//       Id and VehicleType should not be able for changing.
//       The Balance should be able to change only in the CoolParking.BL project.
//       The type of constructor is shown in the tests and the constructor should have a validation, which also is clear from the tests.
//       Static method GenerateRandomRegistrationPlateNumber should return a randomly generated unique identifier.

using System.Text;
using System.Text.RegularExpressions;

namespace CoolParking.BL.Models
{
    public class Vehicle 
    {
        public string Id { get; }
        public VehicleType VehicleType { get; }
        public decimal Balance { get; internal set; }

        public Vehicle(string id, VehicleType vehicleType, decimal balance)
        {
            if (IsValidId(id) && balance >= Settings.Tariffs[(int)vehicleType])
            {
                this.Id = id;
                this.VehicleType = vehicleType; 
                this.Balance = balance;  
            }
            else
            {
                throw new ArgumentException("Invalid identifier entered");
            }
        }

        #region ---helpers---

        public static string GenerateRandomRegistrationPlateNumber()
        {
            Random random = new Random();
            StringBuilder stringBuilder = new StringBuilder();
            GetTwoLetters(stringBuilder, random);
            stringBuilder.Append("-").Append(random.Next(1000, 10000)).Append('-');
            GetTwoLetters(stringBuilder, random);

            return stringBuilder.ToString();
        }

        private static void GetTwoLetters(StringBuilder stringBuilder, Random random)
        {
            for (int i = 0; i < 2; i++)
            {
                stringBuilder.Append((char)random.Next('A', 'Z' + 1));
            }
        }

        public static bool IsValidId(string id)
        {
            return new Regex(@"^[A-Z]{2}-[0-9]{4}-[A-Z]{2}$").IsMatch(id);
        }

        #endregion
    }
}