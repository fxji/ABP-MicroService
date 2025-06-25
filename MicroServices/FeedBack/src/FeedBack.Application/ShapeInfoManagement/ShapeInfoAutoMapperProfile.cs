using AutoMapper;
using Feedback.ShapeInfoManagement.Dto;

namespace Feedback.ShapeInfoManagement
{
    public class ShapeInfoAutoMapperProfile : Profile
    {
        public ShapeInfoAutoMapperProfile()
        {
            CreateMap<ShapeInfo, ShapeInfoDto>();
            CreateMap<CreateOrUpdateShapeInfoDto, ShapeInfo>();
        }
    }
}
