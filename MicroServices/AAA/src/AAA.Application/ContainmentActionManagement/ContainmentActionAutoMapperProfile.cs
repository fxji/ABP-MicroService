using AutoMapper;
using AAA.ContainmentActionManagement.Dto;
using AAA.Models;

namespace AAA.ContainmentActionManagement
{
    public class ContainmentActionAutoMapperProfile : Profile
    {
        public ContainmentActionAutoMapperProfile()
        {
            CreateMap<ContainmentAction, ContainmentActionDto>();
            CreateMap<CreateOrUpdateContainmentActionDto, ContainmentAction>();
        }
    }
}
