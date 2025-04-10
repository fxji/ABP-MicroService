using AAA.Models;
using AutoMapper;
using DataExport.ExportManagement;

namespace AAA.A3Management
{
    public class A3ExportAutoMapperProfile : Profile
    {
        public A3ExportAutoMapperProfile()
        {
            CreateMap<A3, A3ExDto>();
            CreateMap<A3Member, A3MemberExDto>();
            CreateMap<A3Attachment, A3AttachmentExDto>();
            CreateMap<Cause, CauseExDto>();
            CreateMap<ConfirmInfo, ConfirmInfoExDto>();
            CreateMap<ContainmentAction, ContainmentActionExDto>();
            CreateMap<CorrectiveAction, CorrectiveActionExDto>();
            CreateMap<Issue, IssueExDto>();
            CreateMap<RiskAssessment, RiskAssessmentExDto>();
        }
    }
}
