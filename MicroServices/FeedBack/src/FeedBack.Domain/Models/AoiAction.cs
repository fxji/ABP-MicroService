using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

public class AoiAction : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
{
    public Guid FailedInfoId  { get; set; }

    public string Name { get; set; }

    public string Action { get; set; }

    public DateTime ExecuteDate { get; set; }

    public Guid PersonInCharge { get; set; }
    
    
    public bool IsDeleted { get; set; }

    public Guid? TenantId { get; set; }
}