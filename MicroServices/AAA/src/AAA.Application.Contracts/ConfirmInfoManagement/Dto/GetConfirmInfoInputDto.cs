using System;
using Volo.Abp.Application.Dtos;

namespace AAA.ConfirmInfoManagement.Dto
{
    public class GetConfirmInfoInputDto : PagedAndSortedResultRequestDto
    {
        public Guid a3Id { get; set; }

        public string Filter { get; set; }
    }
}