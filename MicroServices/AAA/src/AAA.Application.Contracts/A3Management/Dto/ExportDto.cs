using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace AAA.A3Management.Dto
{
    public class ExportDto : EntityDto<Guid?>
    {
        public string Path { get; set; }
    }
}
