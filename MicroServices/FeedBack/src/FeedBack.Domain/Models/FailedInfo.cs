using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

public class FailedInfo : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
{
    public string SerialNo { get; set; }

    public string PieceNo { get; set; }

    public string Project { get; set; }

    public string ComId { get; set; }

    public int ComType { get; set; }

    public string MaterialNo { get; set; }

    public string Quantity { get; set; }

    public int Position { get; set; }

    public bool IsTop   {get; set; }

    public int FailureMode { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? TenantId { get; set; }
}