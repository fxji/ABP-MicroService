using System;
using Volo.Abp.Application.Dtos;

namespace PeBusiness.PeTaskManagement.Dto
{
    public class GetPeTaskInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}