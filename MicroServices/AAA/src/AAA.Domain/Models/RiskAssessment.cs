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
    public class RiskAssessment: AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        
        /// <summary>
        /// 因素
        /// </summary>
        [Required]
        public string Factor { get; set; }
        
        /// <summary>
        /// 单行文本
        /// </summary>
        public bool SafetyRelevant { get; set; }
        
        /// <summary>
        /// 单行文本
        /// </summary>
        public int Probability { get; set; }
        
        /// <summary>
        /// 多行文本
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// 单行文本
        /// </summary>
        public int Functionally { get; set; }
        
		
		public bool IsDeleted { get; set; }
    }
}