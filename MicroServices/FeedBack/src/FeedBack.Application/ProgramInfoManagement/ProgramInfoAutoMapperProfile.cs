using AutoMapper;
using FeedBack.ProgramInfoManagement.Dto;

namespace FeedBack.ProgramInfoManagement
{
    public class ProgramInfoAutoMapperProfile : Profile
    {
        public ProgramInfoAutoMapperProfile()
        {
            CreateMap<ProgramInfo, ProgramInfoDto>();
            CreateMap<CreateOrUpdateProgramInfoDto, ProgramInfo>();
        }
    }
}
