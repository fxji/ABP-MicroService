using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using System.ComponentModel.DataAnnotations;

namespace AAA.Models
{
    /// <summary>
    /// 表单
    /// </summary>
    public class ContainmentAction: AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        
        /// <summary>
        /// 单行文本
        /// </summary>
        public string Responsibility { get; set; }

        /// <summary>
        /// 单行文本
        /// </summary>
        [Required]
        public string Activities { get; set; }
        
        /// <summary>
        /// 单行文本
        /// </summary>
        public int Status { get; set; }
        
		
		public bool IsDeleted { get; set; }
    }
}