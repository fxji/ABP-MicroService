using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace AAA.A3AttachmentManagement.Dto
{
    public class CreateOrUpdateA3AttachmentDto: EntityDto<Guid?>
    {
        
        /// <summary>
        /// 单行文本
        /// </summary>
        public Guid a3Id { get; set; }
        
        /// <summary>
        /// 单行文本
        /// </summary>
        public string Name { get; set; }

        public string Category { get; set; }

        public string Type { get; set; }

        /// <summary>
        /// 单行文本
        /// </summary>
        public string Url { get; set; }
        
    }
}