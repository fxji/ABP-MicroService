using System;
using Volo.Abp.Application.Dtos;

namespace A3.Confirm.ConfirmManagement.Dto
{
    public class GetConfirmInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}