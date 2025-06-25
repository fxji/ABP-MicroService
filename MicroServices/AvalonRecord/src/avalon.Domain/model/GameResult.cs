using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

public class GameResult : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
{
    public DateTime PlayedAt { get; private set; }

    public GameOutcome Outcome { get; private set; } // Enum: GoodWin, EvilWin, etc.

    public List<GamePlayer> GamePlayers { get; private set; } = new();

    public bool IsDeleted { get; set; }

    public Guid? TenantId { get; set; }

    private GameResult() { }

    public GameResult(Guid id, DateTime playedAt, GameOutcome outcome)
        : base(id)
    {
        PlayedAt = playedAt;
        Outcome = outcome;
    }

    public void AddPlayer(Guid gamePlayerId, Guid playerId, Role role, bool isMistakeKilled)
    {
        GamePlayers.Add(new GamePlayer(gamePlayerId, Id, playerId, role, isMistakeKilled));
    }
}
