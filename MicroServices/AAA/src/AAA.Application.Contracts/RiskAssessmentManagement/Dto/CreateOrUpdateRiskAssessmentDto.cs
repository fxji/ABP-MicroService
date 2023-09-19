using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace AAA.RiskAssessmentManagement.Dto
{
    public class CreateOrUpdateRiskAssessmentDto: EntityDto<Guid?>
    {
        
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
        
    }
}