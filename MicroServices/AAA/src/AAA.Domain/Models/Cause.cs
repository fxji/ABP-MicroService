using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using System.ComponentModel.DataAnnotations;

namespace AAA.Models
{
    /// <summary>
    /// 原因
    /// </summary>
    public class Cause: AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        
        /// <summary>
        /// 单行文本
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// A3
        /// </summary>
        public Guid? A3Id { get; set; }

        /// <summary>
        /// 下拉选择
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// 5M 
        /// Man
        /// Method
        /// Machine
        /// Material
        /// Measurement
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// parent why
        /// </summary>
        public Guid? ParentId { get; set; }
        
		
		public bool IsDeleted { get; set; }
    }
}