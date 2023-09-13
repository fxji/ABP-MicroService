using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using Volo.Abp;

namespace AAA.Models
{
    public class Part : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
		public bool IsDeleted { get; set; }

        public Guid? TenantId { get; set; }


        public Product ParentProduct { get; set; }

        public Customer Customer { get; set; }

        public string Number { get; set; }

        public string Name { get; set; }
    }
}