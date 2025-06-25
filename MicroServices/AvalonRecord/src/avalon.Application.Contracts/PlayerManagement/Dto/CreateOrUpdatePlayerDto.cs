using System;
using Volo.Abp.Application.Dtos;

public class CreateOrUpdatePlayerDto : EntityDto<Guid?>
{
    public string Name { get; set; }
}