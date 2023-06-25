using CoolParking.WebAPI.Services.LogService;
using CoolParking.WebAPI.Services.ParkingService;
using CoolParking.WebAPI.Services.TimerService;
using CoolParking.WebAPI.Services.VehicleService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IVehicleService, VehicleService>();
builder.Services.AddTransient<ITimerService, TimerService>();
builder.Services.AddTransient<ILogService, LogService>();
builder.Services.AddSingleton<IParkingService, ParkingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
