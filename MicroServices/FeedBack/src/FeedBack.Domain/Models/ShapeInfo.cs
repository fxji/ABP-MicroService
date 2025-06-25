using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

public class ShapeInfo : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
{
    public string Name { get; set; }

    public Guid ProgramId { get; set; }

    public string Line { get; set; }

    public int GoodWindows { get; set; }

    public int SlipWindows { get; set; }

    public int FailureWindows { get; set; }

    public int Windows => GoodWindows + SlipWindows + FailureWindows;
    
    public bool  HasChanged { get; set; }

    public bool HasError { get; set; }

    public string Cause { get; set; }

    public DateTime Date { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? TenantId { get; set; }
}