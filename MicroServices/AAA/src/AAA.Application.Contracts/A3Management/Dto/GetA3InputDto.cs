using System;
using Volo.Abp.Application.Dtos;

namespace AAA.A3Management.Dto
{
    public class GetA3InputDto : PagedAndSortedResultRequestDto
    {
        public Guid? A3Id { get; set; }

        public string Filter { get; set; }

        public string Source { get; set; }

        public Guid? OrganizationId { get; set; }

        public string Process { get; set; }

    }
}