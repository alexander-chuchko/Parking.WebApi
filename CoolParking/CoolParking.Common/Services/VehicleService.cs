using AutoMapper;
using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using CoolParking.BL.Services;
using CoolParking.Common.DTO;
using CoolParking.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolParking.Common.Services
{
    public  class VehicleService : IVehicleService
    {
        private readonly IParkingService _parkingService;
        private readonly IMapper _mapper;
        public VehicleService(IParkingService parkingService, IMapper mapper)
        {
            _mapper = mapper;
            _parkingService = parkingService;   
        }

        public VehicleDTO GetVehicleById(string id) 
        {
            VehicleDTO vehicleDTO = null;

            var vehicle = _parkingService.GetVehicles().FirstOrDefault(v => v.Id == id);
            if (vehicle != null)
            {
                vehicleDTO = _mapper.Map<VehicleDTO>(vehicle);
            }

            return vehicleDTO;
        }

        public void AddVehicle(VehicleDTO vehicleDTO)
        {
            if (vehicleDTO!=null)
            {
                var vehicle = _mapper.Map<Vehicle>(vehicleDTO);
                _parkingService.AddVehicle(vehicle);
            }
        }

        public IEnumerable<VehicleDTO> GetVehicles()
        {
            IEnumerable<VehicleDTO> vehiclesDTO = null;
            var vehicles = _parkingService.GetVehicles();
            vehiclesDTO = _mapper.Map<IEnumerable<VehicleDTO>>(vehicles);

            return vehiclesDTO; 
        }
    }
}
