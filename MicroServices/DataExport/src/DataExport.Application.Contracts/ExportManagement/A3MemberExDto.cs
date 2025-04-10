using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace DataExport.ExportManagement
{
    public class A3MemberExDto : EntityDto<Guid?>
    {
     
        public Guid? A3Id { get; set; }

        /// <summary>
        /// Member
        /// </summary>
        [Required]
        public string UserId { get; set; }
        
        /// <summary>
        /// Organization
        /// </summary>
        [Required]
        public string OrganizationId { get; set; }
        
    }
}