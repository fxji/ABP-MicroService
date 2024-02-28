using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace AAA.CauseManagement.Dto
{
    public class CreateOrUpdateCauseDto: EntityDto<Guid?>
    {
        /// <summary>
        /// 单行文本
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// A3
        /// </summary>
        public Guid? A3Id { get; set; }

        /// <summary>
        /// 下拉选择
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 5M 
        /// Man
        /// Method
        /// Machine
        /// Material
        /// Measurement
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// parent why
        /// </summary>
        public Guid? ParentId { get; set; }

        public bool IsRelevant { get; set; }

    }
}