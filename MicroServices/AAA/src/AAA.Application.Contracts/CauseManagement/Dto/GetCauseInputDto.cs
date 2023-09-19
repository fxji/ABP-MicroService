using System;
using Volo.Abp.Application.Dtos;

namespace AAA.CauseManagement.Dto
{
    public class GetCauseInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}