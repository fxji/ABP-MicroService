using System;
using Volo.Abp.Application.Dtos;

namespace AAA.A3MemberManagement.Dto
{
    public class GetA3MemberInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}