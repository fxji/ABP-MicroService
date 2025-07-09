using System;
using Volo.Abp.Application.Dtos;

namespace Feedback.LineInfoManagement.Dto
{
    public class GetLineInfoInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}