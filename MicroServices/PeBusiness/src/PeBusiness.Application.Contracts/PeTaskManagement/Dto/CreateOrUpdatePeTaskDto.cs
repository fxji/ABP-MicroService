using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace PeBusiness.PeTaskManagement.Dto
{
    public class CreateOrUpdatePeTaskDto: EntityDto<Guid?>
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