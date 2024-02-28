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
        /// Raw Material
        /// Production Line
        /// Warehouse
        /// 3rd Party Warehouse
        /// Customer Stock
        /// </summary>
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// 单行文本
        /// </summary>
        public string Responsibility { get; set; }

        /// <summary>
        /// 单行文本
        /// </summary>
        //[Required]
        public string Name { get; set; }

        /// <summary>
        /// A3
        /// </summary>
        public Guid? A3Id { get; set; }

        /// <summary>
        /// 单行文本
        /// </summary>
        public string Status { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}