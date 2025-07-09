using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Feedback.LineInfoManagement.Dto
{
    public class CreateOrUpdateLineInfoDto: EntityDto<Guid?>
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