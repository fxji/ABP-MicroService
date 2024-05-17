using AutoMapper;
using A3.Confirm.ConfirmManagement.Dto;
using A3.Confirm.Models;

namespace A3.Confirm.ConfirmManagement
{
    public class ConfirmAutoMapperProfile : Profile
    {
        public ConfirmAutoMapperProfile()
        {
            CreateMap<Confirm, ConfirmDto>();
            CreateMap<CreateOrUpdateConfirmDto, Confirm>();
        }
    }
}
