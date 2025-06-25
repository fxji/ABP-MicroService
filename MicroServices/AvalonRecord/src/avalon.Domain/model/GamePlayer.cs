using System;
using Volo.Abp.Domain.Entities;

public class GamePlayer : Entity<Guid>
{
    public Guid GameResultId { get; set; }

    public Guid PlayerId { get; set; }

    public Role Role { get; set; }

    public bool IsWinner { get; set; }

    public bool MistakeKilled { get; set; }


    public GamePlayer(Guid id, Guid gameResultId, Guid playerId, Role role, bool isWinner): base(id)
    {
        GameResultId = gameResultId;
        PlayerId = playerId;
        Role = role;
        IsWinner = isWinner;
    }

    public override object[] GetKeys()
    {
        return new object[] { GameResultId, PlayerId }; 
    }
}