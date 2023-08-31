using System;
using Volo.Abp.Application.Dtos;

namespace AAA.A3Management.Dto
{
    public class GetA3InputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}