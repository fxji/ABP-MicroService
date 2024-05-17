using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace A3.ConfirmInfo.ConfirmInfoManagement.Dto
{
    public class CreateOrUpdateConfirmInfoDto: EntityDto<Guid?>
    {
        
        /// <summary>
        /// UserId
        /// </summary>
        [Required]
        public string UserId { get; set; }
        
        /// <summary>
        /// A3Id
        /// </summary>
        [Required]
        public string A3Id { get; set; }
        
        /// <summary>
        /// Date
        /// </summary>
        [Required]
        public DateTime Date { get; set; }
        
        /// <summary>
        /// Comments
        /// </summary>
        [Required]
        public string Comments { get; set; }
        
    }
}