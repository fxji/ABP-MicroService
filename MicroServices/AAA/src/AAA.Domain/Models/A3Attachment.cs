using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using Volo.Abp;

namespace AAA.Models
{
    public class A3Attachment : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {

        public Guid? a3Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Url { get; set; }

        public bool IsDeleted { get; set; }

        public Guid? TenantId { get; set; }
    }
}
