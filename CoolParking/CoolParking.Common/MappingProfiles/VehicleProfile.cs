using AutoMapper;
using CoolParking.Common.DTO;
using CoolParking.BL.Models;

namespace CoolParking.Common.MappingProfiles
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<Vehicle, VehicleDTO>();

            CreateMap<VehicleDTO, Vehicle>();
        }
    }
}
