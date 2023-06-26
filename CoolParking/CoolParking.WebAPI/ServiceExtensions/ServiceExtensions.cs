using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using CoolParking.BL.Services;

namespace CoolParking.WebAPI.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static void RegisterCustomServices(this IServiceCollection services)
        {
            services.AddSingleton<string>(Settings.LogFilePath);
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<ITimerService, TimerService>();
            services.AddSingleton<IParkingService, ParkingService>();
        }
    }
}
