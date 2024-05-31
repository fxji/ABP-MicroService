using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace AAA.A3AttachmentManagement.Dto
{
    public class A3AttachmentDto : EntityDto<Guid?>
    {
        
        /// <summary>
        /// 单行文本
        /// </summary>
        public Guid A3Id { get; set; }
        
        /// <summary>
        /// 单行文本
        /// </summary>
        public string Name { get; set; }

        public string Type { get; set; }

        public string Category { get; set; }

        /// <summary>
        /// 单行文本
        /// </summary>
        public string Url { get; set; }
        
    }
}