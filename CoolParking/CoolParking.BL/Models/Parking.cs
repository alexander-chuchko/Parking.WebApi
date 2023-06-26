// TODO: implement class Parking.
//       Implementation details are up to you, they just have to meet the requirements 
//       of the home task and be consistent with other classes and tests.

namespace CoolParking.BL.Models
{
    public class Parking : IDisposable
    {
        public List<Vehicle> Vehicles { get; set; }
        public decimal Balance { get; set; }
        public DateTime? StartTime { get; set; }

        private static Parking? instance;
        private static readonly object lockObject = new object();

        public Parking()
        {
        }

        public static Parking GetInstance()
        {
            lock (lockObject)
            {
                if (instance == null)
                {
                    instance = new Parking();
                }
            }

            return instance;
        }

        public void Dispose()
        {
            lock (lockObject)
            {
                Vehicles.Clear();
                Balance = 0;
                StartTime = null;
            }
        }

        public void DisposeInstance()
        {
            if (instance != null)
            {
                instance.Dispose();
                instance = null;
            }
        }
    }
}