using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace AAA.IssueManagement.Dto
{
    public class IssueDto : EntityDto<Guid?>
    {

        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public int Type { get; set; }
        
        /// <summary>
        /// 时间选择
        /// </summary>
        public DateTime OccurrenceDate { get; set; }
        
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// 计数器
        /// </summary>
        public float Rate { get; set; }
        
    }
}