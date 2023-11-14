using System;
using Volo.Abp.Application.Dtos;

namespace AAA.CauseManagement.Dto
{
    public class GetCauseInputDto : PagedAndSortedResultRequestDto
    {
        public Guid a3Id { get; set; }

        public string Filter { get; set; }
    }
}