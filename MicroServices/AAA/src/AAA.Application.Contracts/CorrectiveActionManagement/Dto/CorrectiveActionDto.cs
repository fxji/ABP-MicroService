using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace AAA.CorrectiveActionManagement.Dto
{
    public class CorrectiveActionDto : EntityDto<Guid?>
    {
        /// <summary>
        /// 单行文本
        /// </summary>
        public string Activites { get; set; }

        /// <summary>
        /// 单行文本
        /// </summary>
        [Required]
        public string Name { get; set; }


        /// <summary>
        /// A3
        /// </summary>
        public Guid A3Id { get; set; }

        /// <summary>
        /// 下拉选择
        /// </summary>
        public Guid Status { get; set; }

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