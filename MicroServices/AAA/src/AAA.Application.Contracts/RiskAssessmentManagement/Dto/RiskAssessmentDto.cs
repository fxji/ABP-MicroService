using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace AAA.RiskAssessmentManagement.Dto
{
    public class RiskAssessmentDto : EntityDto<Guid?>
    {

        /// <summary>
        /// 因素
        /// </summary>
        [Required]
        public string Factor { get; set; }

        /// <summary>
        /// A3
        /// </summary>
        public Guid A3Id { get; set; }

        /// <summary>
        /// 单行文本
        /// </summary>
        public bool SafetyRelevant { get; set; }

        /// <summary>
        /// Low
        /// Medium
        /// High
        /// </summary>
        public Guid Probability { get; set; }

        /// <summary>
        /// 多行文本
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Low
        /// Medium
        /// High
        /// </summary>
        public Guid Functionally { get; set; }

    }
}