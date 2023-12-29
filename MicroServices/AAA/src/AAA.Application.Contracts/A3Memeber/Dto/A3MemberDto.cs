using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace AAA.A3MemberManagement.Dto
{
    public class A3MemberDto : EntityDto<Guid?>
    {
        
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