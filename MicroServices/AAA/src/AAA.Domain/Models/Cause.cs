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
        public string Title { get; set; }
        
        /// <summary>
        /// 下拉选择
        /// </summary>
        public int Status { get; set; }
        
        /// <summary>
        /// 下拉选择
        /// </summary>
        public int Type { get; set; }
        
		
		public bool IsDeleted { get; set; }
    }
}