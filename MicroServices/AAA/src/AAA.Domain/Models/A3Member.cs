using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using System.ComponentModel.DataAnnotations;

namespace AAA.Models
{
    /// <summary>
    /// A3Member
    /// </summary>
    public class A3Member: AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        
        /// <summary>
        /// Member
        /// </summary>
        [Required]
        public string UserId { get; set; }
        
        /// <summary>
        /// Organization
        /// </summary>
        [Required]
        public string OrganizationId { get; set; }
        
		
		public bool IsDeleted { get; set; }
    }
}