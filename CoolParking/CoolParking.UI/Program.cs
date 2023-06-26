// See https://aka.ms/new-console-template for more information



using CoolParking.BL;
using CoolParking.BL.Models;
using CoolParking.BL.Services;

UserInterface userInterface = new UserInterface(new ApiService());
//ParkingService parkingService = new ParkingService(new TimerService(), new TimerService(), new LogService(Settings.logFilePath));
//UserInterface navigation = new UserInterface(parkingService);
//navigation.RunApplication();