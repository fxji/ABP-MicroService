using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace AAA.A3Management.Dto
{
    public class CreateOrUpdateA3Dto: EntityDto<Guid?>
    {
        
        /// <summary>
        /// 开关
        /// </summary>
        public bool ReOccurrence { get; set; }
        
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