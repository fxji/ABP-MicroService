using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

public class AoiInfo : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
{
    public Guid FailedInfoId { get; set; }

    public string AoiName { get; set; }

    public DateTime AoiDate { get; set; } //AoiDate

    public string AoiLine { get; set; }

    public string RootCause { get; set; }

    public string comments { get; set; }

    // public List<string> pictures { get; set; }

    
    public bool IsDeleted { get; set; }

    public Guid? TenantId { get; set; }

    void SetFailureMode(int failureMode)
    {
        //TODO
        // FailureMode = failureMode;
    }
}