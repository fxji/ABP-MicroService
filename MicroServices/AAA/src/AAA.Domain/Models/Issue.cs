using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AAA.Models
{
    /// <summary>
    /// 问题
    /// </summary>
    public class Issue : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        public Guid? TenantId { get; set; }


        [Required]
        public string Name { get; set; }

        /// <summary>
        /// A3
        /// </summary>
        public Guid? A3Id { get; set; }

        /// <summary>
        /// Problem Type
        /// </summary>
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// Customer
        /// </summary>
        public int? CustomerId { get; set; }

        /// <summary>
        /// Symptom Description
        /// </summary>
        public string SymptomDescription { get; set; }

        /// <summary>
        /// Goal Statement
        /// </summary>
        public string GoalStatement { get; set; }

        /// <summary>
        /// Failure Qty/Rate
        /// </summary>
        public float Rate { get; set; }

        /// <summary>
        /// Product
        /// </summary>
        public int? ProductId { get; set; }

        /// <summary>
        /// 时间选择
        /// </summary>
        public DateTime? OccurrenceDate { get; set; }

        /// <summary>
        /// Symptom Description
        /// </summary>
        public string Description { get; set; }


        public bool IsDeleted { get; set; }
    }
}