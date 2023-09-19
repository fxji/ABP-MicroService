using System;
using Volo.Abp.Application.Dtos;

namespace AAA.RiskAssessmentManagement.Dto
{
    public class GetRiskAssessmentInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}