using System;
using Volo.Abp.Application.Dtos;

namespace Feedback.ShapeInfoManagement.Dto
{
    public class GetShapeInfoInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public string Name { get; set; }
        public string Line { get; set; }
        public Guid? ProgramId { get; set; }
        public bool? HasError { get; set; }
        public bool? HasChanged { get; set; }
    }
}