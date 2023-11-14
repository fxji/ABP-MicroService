using System;
using Volo.Abp.Application.Dtos;

namespace AAA.IssueManagement.Dto
{
    public class GetIssueInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }


        public Guid a3Id { get; set; }
    }
}