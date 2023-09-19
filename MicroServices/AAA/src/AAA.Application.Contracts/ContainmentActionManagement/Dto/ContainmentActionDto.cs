using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace AAA.ContainmentActionManagement.Dto
{
    public class ContainmentActionDto : EntityDto<Guid?>
    {
        
        /// <summary>
        /// 单行文本
        /// </summary>
        public string Responsibility { get; set; }
        
        /// <summary>
        /// 单行文本
        /// </summary>
        public string Activities { get; set; }
        
        /// <summary>
        /// 单行文本
        /// </summary>
        public int Status { get; set; }
        
    }
}