using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using CoolParking.BL.Services;
using System.Reflection;

namespace CoolParking.WebAPI.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static void RegisterCustomServices(this IServiceCollection services)
        {
            services.AddSingleton<string>(Settings.LogFilePath);//$@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\transactions.log");
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<ITimerService, TimerService>();
            services.AddTransient<IParkingService, ParkingService>();
        }
    }
}
