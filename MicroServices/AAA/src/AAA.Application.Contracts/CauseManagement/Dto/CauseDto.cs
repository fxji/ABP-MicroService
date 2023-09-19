using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace AAA.CauseManagement.Dto
{
    public class CauseDto : EntityDto<Guid?>
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