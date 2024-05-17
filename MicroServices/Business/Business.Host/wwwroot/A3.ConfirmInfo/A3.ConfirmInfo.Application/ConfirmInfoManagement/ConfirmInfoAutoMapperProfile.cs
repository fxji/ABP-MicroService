using AutoMapper;
using A3.ConfirmInfo.ConfirmInfoManagement.Dto;
using A3.ConfirmInfo.Models;

namespace A3.ConfirmInfo.ConfirmInfoManagement
{
    public class ConfirmInfoAutoMapperProfile : Profile
    {
        public ConfirmInfoAutoMapperProfile()
        {
            CreateMap<ConfirmInfo, ConfirmInfoDto>();
            CreateMap<CreateOrUpdateConfirmInfoDto, ConfirmInfo>();
        }
    }
}
