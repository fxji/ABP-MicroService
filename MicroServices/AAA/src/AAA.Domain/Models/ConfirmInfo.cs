using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using System.ComponentModel.DataAnnotations;

namespace AAA.Models
{
    /// <summary>
    /// ConfirmInfo
    /// </summary>
    public class ConfirmInfo: AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        
       
        
        /// <summary>
        /// A3Id
        /// </summary>
        [Required]
        public Guid A3Id { get; set; }
        
        
        
        /// <summary>
        /// Comments
        /// </summary>
        [Required]
        public string Comments { get; set; }
        
		
		public bool IsDeleted { get; set; }
    }
}