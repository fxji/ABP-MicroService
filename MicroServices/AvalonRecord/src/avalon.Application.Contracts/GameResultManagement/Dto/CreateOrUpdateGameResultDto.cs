using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

public class CreateOrUpdateGameResultDto : EntityDto<Guid?>
{
    public GameOutcome Outcome { get; set; }
    
    public List<GamePlayerDto> GamePlayers { get; set; }
}