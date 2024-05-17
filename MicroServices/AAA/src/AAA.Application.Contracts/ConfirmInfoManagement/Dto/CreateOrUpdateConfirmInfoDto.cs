using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace AAA.ConfirmInfoManagement.Dto
{
    public class CreateOrUpdateConfirmInfoDto: EntityDto<Guid?>
    {
        
        
        /// <summary>
        /// A3Id
        /// </summary>
        [Required]
        public Guid? A3Id { get; set; }
        
        /// <summary>
        /// Comments
        /// </summary>
        [Required]
        public string Comments { get; set; }
        
    }
}