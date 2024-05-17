using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace A3.Confirm.ConfirmManagement.Dto
{
    public class ConfirmDto : EntityDto<Guid?>
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