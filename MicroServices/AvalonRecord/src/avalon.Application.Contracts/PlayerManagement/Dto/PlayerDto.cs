using System;
using Volo.Abp.Application.Dtos;

public class PlayerDto: EntityDto<Guid>
{
    public string Name { get; set; }
    public int TotalScore { get; set; }
}