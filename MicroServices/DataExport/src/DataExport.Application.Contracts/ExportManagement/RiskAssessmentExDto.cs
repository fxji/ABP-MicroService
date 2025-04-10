using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace DataExport.ExportManagement
{
    public class RiskAssessmentExDto : EntityDto<Guid?>
    {

        /// <summary>
        /// 因素
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// A3
        /// </summary>
        public Guid? A3Id { get; set; }

        /// <summary>
        /// 单行文本
        /// </summary>
        public bool SafetyRelevant { get; set; }

        /// <summary>
        /// Low
        /// Medium
        /// High
        /// </summary>
        public string Probability { get; set; }

        /// <summary>
        /// 多行文本
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Low
        /// Medium
        /// High
        /// </summary>
        public string Functionally { get; set; }

    }
}