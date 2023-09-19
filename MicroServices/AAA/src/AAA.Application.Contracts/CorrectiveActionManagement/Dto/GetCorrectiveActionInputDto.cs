using System;
using Volo.Abp.Application.Dtos;

namespace AAA.CorrectiveActionManagement.Dto
{
    public class GetCorrectiveActionInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}