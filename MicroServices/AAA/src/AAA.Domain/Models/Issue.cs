using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using System.ComponentModel.DataAnnotations;

namespace AAA.Models
{
    /// <summary>
    /// 问题
    /// </summary>
    public class Issue: AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public int Type { get; set; }
        
        /// <summary>
        /// 时间选择
        /// </summary>
        public DateTime OccurrenceDate { get; set; }
        
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// 百分比
        /// </summary>
        public float Rate { get; set; }


        public Part ReferencePart { get; set; }
              
		
		public bool IsDeleted { get; set; }
    }
}