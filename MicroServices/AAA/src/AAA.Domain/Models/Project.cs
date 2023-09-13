using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using Volo.Abp;

namespace AAA.Models
{
    public class Project : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public bool IsDeleted { get; set; }

        public string Number { get; set; }

        public string Name { get; set; }
    }
}