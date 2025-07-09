using AutoMapper;
using Feedback.LineInfoManagement.Dto;

namespace Feedback.LineInfoManagement
{
    public class LineInfoAutoMapperProfile : Profile
    {
        public LineInfoAutoMapperProfile()
        {
            CreateMap<LineInfo, LineInfoDto>();
            CreateMap<CreateOrUpdateLineInfoDto, LineInfo>();
        }
    }
}
