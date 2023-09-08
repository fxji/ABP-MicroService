using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace PeBusiness.PeTaskManagement.Dto
{
    public class PeTaskDto : EntityDto<Guid?>
    {
        
        /// <summary>
        /// 时间选择
        /// </summary>
        [Required]
        public DateTime DueDate { get; set; }
        
        /// <summary>
        /// 单行文本
        /// </summary>
        [Required]
        public string Name { get; set; }
        
    }
}