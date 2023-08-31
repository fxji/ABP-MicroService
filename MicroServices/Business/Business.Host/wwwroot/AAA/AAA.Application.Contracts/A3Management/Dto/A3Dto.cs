using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace AAA.A3Management.Dto
{
    public class A3Dto : EntityDto<Guid?>
    {
        
        /// <summary>
        /// 开关
        /// </summary>
        public bool Re-Occurrence { get; set; }
        
        /// <summary>
        /// 单行文本
        /// </summary>
        [Required]
        public string Name { get; set; }
        
        /// <summary>
        /// 日期选择
        /// </summary>
        [Required]
        public DateTime StartDate { get; set; }
        
    }
}