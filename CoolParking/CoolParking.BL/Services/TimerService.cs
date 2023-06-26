// TODO: implement class TimerService from the ITimerService interface.
//       Service have to be just wrapper on System Timers.


using CoolParking.BL.Interfaces;
using System.Timers;

namespace CoolParking.BL.Services
{
    public class TimerService : ITimerService
    {
        private System.Timers.Timer timer;
        public TimerService()
        {
            timer = new System.Timers.Timer();
        }

        #region  ---  Interface ITimerService implementation   ---
        public double Interval 
        {
            get { return timer.Interval; }
            set { timer.Interval = value; } 
        }

        public event ElapsedEventHandler Elapsed;

        public void Start()
        {
            timer.Elapsed += FireElapsedEvent;
            timer.AutoReset = true;
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        public void Dispose()
        {
        }

        #endregion

        #region ---Helpers---

        public void FireElapsedEvent(object sender, ElapsedEventArgs e)
        {
            if (Elapsed != null)
            {
                Elapsed?.Invoke(this, null);
            }
        }

        #endregion
    }
}
