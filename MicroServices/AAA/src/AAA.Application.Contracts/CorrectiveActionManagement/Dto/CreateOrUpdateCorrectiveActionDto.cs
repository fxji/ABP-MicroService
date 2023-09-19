using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace AAA.CorrectiveActionManagement.Dto
{
    public class CreateOrUpdateCorrectiveActionDto: EntityDto<Guid?>
    {
        
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
        
    }
}