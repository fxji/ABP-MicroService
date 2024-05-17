using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using System.ComponentModel.DataAnnotations;

namespace A3.Confirm.Models
{
    /// <summary>
    /// ConfirmInfo
    /// </summary>
    public class Confirm: AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        
        /// <summary>
        /// UserId
        /// </summary>
        [Required]
        public string UserId { get; set; }
        
        /// <summary>
        /// A3Id
        /// </summary>
        [Required]
        public string A3Id { get; set; }
        
        /// <summary>
        /// Date
        /// </summary>
        [Required]
        public DateTime Date { get; set; }
        
        /// <summary>
        /// Comments
        /// </summary>
        [Required]
        public string Comments { get; set; }
        
		
		public bool IsDeleted { get; set; }
    }
}