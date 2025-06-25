using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

public class Player : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
{
    public string Name { get; private set; }

    public int TotalScore { get; private set; }

    public string Avatar { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? TenantId { get; set; }

    private Player() { }

    public Player(Guid id, string name)
        : base(id)
    {
        Name = name;
        TotalScore = 0;
    }

    public void AddScore(int score)
    {
        TotalScore += score;
    }


}