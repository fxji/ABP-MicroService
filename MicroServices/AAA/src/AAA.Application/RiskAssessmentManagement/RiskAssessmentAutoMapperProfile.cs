using AutoMapper;
using AAA.RiskAssessmentManagement.Dto;
using AAA.Models;

namespace AAA.RiskAssessmentManagement
{
    public class RiskAssessmentAutoMapperProfile : Profile
    {
        public RiskAssessmentAutoMapperProfile()
        {
            CreateMap<RiskAssessment, RiskAssessmentDto>();
            CreateMap<CreateOrUpdateRiskAssessmentDto, RiskAssessment>();
        }
    }
}
