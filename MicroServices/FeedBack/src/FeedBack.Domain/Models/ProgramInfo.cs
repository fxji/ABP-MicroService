using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

public class ProgramInfo: AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
{
    public string Name { get; set; }

    public string Line { get; set; }

    // public ICollection<ShapeInfo> ShapeInfos { get; set; }= new List<ShapeInfo>();

    public int Good { get; set; }

    public int Failure { get; set; }

    public int Slip { get; set; }

    public int Windows => Good + Failure + Slip;

    public DateTime Date { get; set; }
    
    public bool IsDeleted { get; set; }

    public Guid? TenantId { get; set; }
}