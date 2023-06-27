using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using CoolParking.BL.Services;
using CoolParking.Common.Interfaces;
using CoolParking.Common.MappingProfiles;
using CoolParking.Common.Services;
using System.Reflection;

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
            services.AddTransient<IVehicleService, VehicleService>();
            services.AddTransient<ITransactionInfoService, TransactionInfoService>();
        }

        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<TransactionInfoProfile>();
                cfg.AddProfile<VehicleProfile>();
            },
            Assembly.GetExecutingAssembly());
        }
    }
}
