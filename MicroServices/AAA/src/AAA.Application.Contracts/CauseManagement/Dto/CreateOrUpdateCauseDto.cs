using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace AAA.CauseManagement.Dto
{
    public class CreateOrUpdateCauseDto: EntityDto<Guid?>
    {
        
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
        /// 下拉选择
        /// </summary>
        public int Type { get; set; }
        
    }
}