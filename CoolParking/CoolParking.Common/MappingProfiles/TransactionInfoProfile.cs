using AutoMapper;
using CoolParking.BL.Models;
using CoolParking.Common.DTO;

namespace CoolParking.Common.MappingProfiles
{
    public class TransactionInfoProfile : Profile
    {
        public TransactionInfoProfile()
        {
            CreateMap<TransactionInfo, TransactionInfoDTO>();

            CreateMap<TransactionInfoDTO, TransactionInfo>();
        }
    }
}
