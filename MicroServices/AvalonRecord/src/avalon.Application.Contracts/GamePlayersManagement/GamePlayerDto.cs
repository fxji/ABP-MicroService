using System;
using Volo.Abp.Application.Dtos;

public class GamePlayerDto : EntityDto<Guid?>
{
    public Guid PlayerId { get; set; }
    
    public Role Role { get; set; }

    public bool MistakeKilled { get; set; }
}