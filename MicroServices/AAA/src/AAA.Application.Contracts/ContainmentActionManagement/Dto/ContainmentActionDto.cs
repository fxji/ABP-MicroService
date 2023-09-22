using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace AAA.ContainmentActionManagement.Dto
{
    public class ContainmentActionDto : EntityDto<Guid?>
    {
        /// <summary>
        /// Raw Material
        /// Production Line
        /// Warehouse
        /// 3rd Party Warehouse
        /// Customer Stock
        /// </summary>
        [Required]
        public Guid Type { get; set; }

        /// <summary>
        /// 单行文本
        /// </summary>
        public string Responsibility { get; set; }

        /// <summary>
        /// 单行文本
        /// </summary>
        [Required]
        public string Activities { get; set; }

        /// <summary>
        /// A3
        /// </summary>
        public Guid A3Id { get; set; }

        /// <summary>
        /// 单行文本
        /// </summary>
        public Guid Status { get; set; }


        
        
    }
}