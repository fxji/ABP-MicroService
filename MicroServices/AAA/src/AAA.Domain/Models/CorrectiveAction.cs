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
    public class CorrectiveAction: AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        
        /// <summary>
        /// 单行文本
        /// </summary>
        public string Activites { get; set; }
        
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
        /// 时间选择
        /// </summary>
        public DateTime DueDate { get; set; }
        
        /// <summary>
        /// 单行文本
        /// </summary>
        public string Responsibility { get; set; }
        
		
		public bool IsDeleted { get; set; }
    }
}