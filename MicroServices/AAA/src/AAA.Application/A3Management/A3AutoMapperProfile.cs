using AutoMapper;
using AAA.A3Management.Dto;
using AAA.Models;
using AAA.A3MemberManagement.Dto;
using AAA.A3AttachmentManagement.Dto;
using AAA.CauseManagement.Dto;
using AAA.ConfirmInfoManagement.Dto;
using AAA.ContainmentActionManagement.Dto;
using AAA.CorrectiveActionManagement.Dto;
using AAA.IssueManagement.Dto;
using AAA.RiskAssessmentManagement.Dto;

namespace AAA.A3Management
{
    public class A3AutoMapperProfile : Profile
    {
        public A3AutoMapperProfile()
        {
            CreateMap<A3, A3Dto>();
            CreateMap<A3Member, A3MemberDto>();
            CreateMap<A3Attachment, A3AttachmentDto>();
            CreateMap<Cause, CauseDto>();
            CreateMap<ConfirmInfo, ConfirmInfoDto>();
            CreateMap<ContainmentAction, ContainmentActionDto>();
            CreateMap<CorrectiveAction, CorrectiveActionDto>();
            CreateMap<Issue, IssueDto>();
            CreateMap<RiskAssessment, RiskAssessmentDto>();
            CreateMap<CreateOrUpdateA3Dto, A3>();
            CreateMap<CreateOrUpdateA3MemberDto, A3Member>();
            CreateMap<CreateOrUpdateA3AttachmentDto, A3Attachment>();
            CreateMap<CreateOrUpdateCauseDto, Cause>();
            CreateMap<CreateOrUpdateConfirmInfoDto, ConfirmInfo>();
            CreateMap<CreateOrUpdateContainmentActionDto, ContainmentAction>();
            CreateMap<CreateOrUpdateCorrectiveActionDto, CorrectiveAction>();
            CreateMap<CreateOrUpdateIssueDto, Issue>();
            CreateMap<CreateOrUpdateRiskAssessmentDto, RiskAssessment>();
        }
    }
}
