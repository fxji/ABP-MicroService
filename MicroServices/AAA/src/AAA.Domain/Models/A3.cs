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
    public class A3: AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        
        /// <summary>
        /// 开关
        /// </summary>
        public bool ReOccurrence { get; set; }
        
        /// <summary>
        /// 单行文本
        /// </summary>
        [Required]
        public string Name { get; set; }
        
        /// <summary>
        /// 日期选择
        /// </summary>
        [Required]
        public DateTime StartDate { get; set; }
        
		
		public bool IsDeleted { get; set; }
    }
}