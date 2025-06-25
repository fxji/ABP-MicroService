using System;
using Volo.Abp.Application.Dtos;

namespace FeedBack.ProgramInfoManagement.Dto
{
    public class GetProgramInfoInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public string Name { get; set; }
        public string Line { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}