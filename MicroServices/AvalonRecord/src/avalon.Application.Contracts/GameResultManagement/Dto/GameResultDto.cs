using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

public class GameResultDto: EntityDto<Guid>
{
    public DateTime PlayedAt { get; set; }
    public GameOutcome Outcome { get; set; }
    public List<GamePlayerDto> GamePlayers { get; set; }
}