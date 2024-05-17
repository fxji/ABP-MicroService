using System;
using Volo.Abp.Application.Dtos;

namespace A3.ConfirmInfo.ConfirmInfoManagement.Dto
{
    public class GetConfirmInfoInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}