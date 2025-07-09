using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Feedback.LineInfoManagement.Dto
{
    public class LineInfoDto : EntityDto<Guid?>
    {
        
        /// <summary>
        /// Line
        /// </summary>
        [Required]
        public string PersonInCharge { get; set; }
        
        /// <summary>
        /// PersonInCharge
        /// </summary>
        [Required]
        public string Name { get; set; }
        
    }
}