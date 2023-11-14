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
    public class A3 : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Location/Plant/Site
        /// </summary>
        public Guid? OrganizationId { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Sponsor / Champion
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// 开关
        /// </summary>
        public bool ReOccurrence { get; set; }

        /// <summary>
        /// 日期选择
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Process of Production issue
        /// </summary>
        public string Process { get; set; }

        /// <summary>
        /// Reference A3 
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Source Of Defect
        /// </summary>
        public string Source { get; set; }


        public bool IsDeleted { get; set; }
    }
}