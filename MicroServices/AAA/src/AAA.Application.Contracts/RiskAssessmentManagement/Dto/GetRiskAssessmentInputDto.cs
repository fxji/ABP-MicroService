using System;
using Volo.Abp.Application.Dtos;

namespace AAA.RiskAssessmentManagement.Dto
{
    public class GetRiskAssessmentInputDto : PagedAndSortedResultRequestDto
    {
        public Guid a3Id { get; set; }

        public string Filter { get; set; }
    }
}