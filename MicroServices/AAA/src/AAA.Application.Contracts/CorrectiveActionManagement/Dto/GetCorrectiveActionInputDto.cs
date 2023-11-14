using System;
using Volo.Abp.Application.Dtos;

namespace AAA.CorrectiveActionManagement.Dto
{
    public class GetCorrectiveActionInputDto : PagedAndSortedResultRequestDto
    {
        public Guid a3Id { get; set; }

        public string Filter { get; set; }
    }
}