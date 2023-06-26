// See https://aka.ms/new-console-template for more information



using CoolParking.BL;
using CoolParking.BL.Services;

UserInterface userInterface = new UserInterface(new ApiService());
userInterface.RunApplication();

//ParkingService parkingService = new ParkingService(new TimerService(), new TimerService(), new LogService(Settings.logFilePath));
//UserInterface navigation = new UserInterface(parkingService);
//navigation.RunApplication();