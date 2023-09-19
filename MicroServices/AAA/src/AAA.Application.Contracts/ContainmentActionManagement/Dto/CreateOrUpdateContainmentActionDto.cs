using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace AAA.ContainmentActionManagement.Dto
{
    public class CreateOrUpdateContainmentActionDto: EntityDto<Guid?>
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