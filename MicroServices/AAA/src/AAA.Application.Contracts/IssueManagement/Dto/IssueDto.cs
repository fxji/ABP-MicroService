using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace AAA.IssueManagement.Dto
{
    public class IssueDto : EntityDto<Guid?>
    {
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// A3
        /// </summary>
        public Guid A3Id { get; set; }

        /// <summary>
        /// Problem Type
        /// </summary>
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// Customer
        /// </summary>
        public int CustomerId { get; set; }

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
        public int ProductId { get; set; }

        /// <summary>
        /// 时间选择
        /// </summary>
        public DateTime OccurrenceDate { get; set; }

        /// <summary>
        /// Symptom Description
        /// </summary>
        public string Description { get; set; }
        



    }
}