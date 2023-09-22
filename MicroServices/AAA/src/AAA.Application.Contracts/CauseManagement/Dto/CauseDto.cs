using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace AAA.CauseManagement.Dto
{
    public class CauseDto : EntityDto<Guid?>
    {
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
        /// 5M 
        /// Man
        /// Method
        /// Machine
        /// Material
        /// Measurement
        /// </summary>
        public Guid Type { get; set; }

        /// <summary>
        /// parent why
        /// </summary>
        public Guid ParentId { get; set; }

    }
}