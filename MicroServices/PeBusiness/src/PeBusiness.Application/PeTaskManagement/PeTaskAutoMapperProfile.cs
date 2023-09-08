using AutoMapper;
using PeBusiness.PeTaskManagement.Dto;
using PeBusiness.Models;

namespace PeBusiness.PeTaskManagement
{
    public class PeTaskAutoMapperProfile : Profile
    {
        public PeTaskAutoMapperProfile()
        {
            CreateMap<PeTask, PeTaskDto>();
            CreateMap<CreateOrUpdatePeTaskDto, PeTask>();
        }
    }
}
