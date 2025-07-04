using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

public class LineInfo : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
{
    public string Name { get; set; }

    public string PersonInCharge { get; set; }
    
    public bool IsDeleted { get; set; }

    public Guid? TenantId { get; set; }
}