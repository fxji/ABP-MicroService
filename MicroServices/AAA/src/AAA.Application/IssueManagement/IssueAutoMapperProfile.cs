using AutoMapper;
using AAA.IssueManagement.Dto;
using AAA.Models;

namespace AAA.IssueManagement
{
    public class IssueAutoMapperProfile : Profile
    {
        public IssueAutoMapperProfile()
        {
            CreateMap<Issue, IssueDto>();
            CreateMap<CreateOrUpdateIssueDto, Issue>();
        }
    }
}
