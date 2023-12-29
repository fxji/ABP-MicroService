using System;
using Volo.Abp.Application.Dtos;

namespace AAA.A3AttachmentManagement.Dto
{
    public class GetA3AttachmentInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string Type { get; set; }

        public Guid A3Id { get; set; }
    }
}