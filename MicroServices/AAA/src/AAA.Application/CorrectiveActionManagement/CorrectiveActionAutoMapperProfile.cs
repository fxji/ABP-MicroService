using AutoMapper;
using AAA.CorrectiveActionManagement.Dto;
using AAA.Models;

namespace AAA.CorrectiveActionManagement
{
    public class CorrectiveActionAutoMapperProfile : Profile
    {
        public CorrectiveActionAutoMapperProfile()
        {
            CreateMap<CorrectiveAction, CorrectiveActionDto>();
            CreateMap<CreateOrUpdateCorrectiveActionDto, CorrectiveAction>();
        }
    }
}
