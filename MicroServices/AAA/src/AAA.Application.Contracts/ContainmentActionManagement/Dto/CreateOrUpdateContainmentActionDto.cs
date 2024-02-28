using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace AAA.ContainmentActionManagement.Dto
{
    public class CreateOrUpdateContainmentActionDto: EntityDto<Guid?>
    {

        /// <summary>
        /// Raw Material
        /// Production Line
        /// Warehouse
        /// 3rd Party Warehouse
        /// Customer Stock
        /// </summary>
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// 单行文本
        /// </summary>
        public string Responsibility { get; set; }

        /// <summary>
        /// 单行文本
        /// </summary>
        //[Required]
        public string Name { get; set; }

        /// <summary>
        /// A3
        /// </summary>
        public Guid? A3Id { get; set; }

        /// <summary>
        /// 单行文本
        /// </summary>
        public string Status { get; set; }


        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

    }
}