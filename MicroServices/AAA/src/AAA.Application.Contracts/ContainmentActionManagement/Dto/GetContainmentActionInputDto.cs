using System;
using Volo.Abp.Application.Dtos;

namespace AAA.ContainmentActionManagement.Dto
{
    public class GetContainmentActionInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public Guid a3Id { get; set; }
    }
}